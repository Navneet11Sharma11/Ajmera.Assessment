using System;
using Ajmera.Assessment.API.ApiUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics;

namespace Ajmera.Assessment.API.Logging
{
    using static IdEncoder;

    internal class ErrorLog : IErrorLog
    {
        private readonly Settings _Settings;
        private readonly IHttpContextAccessor _ContextAccessor;
        private readonly ILogger<ErrorLog> _Logger;

        public ErrorLog(IOptionsSnapshot<Settings> options, IHttpContextAccessor contextAccessor, ILogger<ErrorLog> logger)
        {
            _Settings = options.Value;
            _ContextAccessor = contextAccessor;
            _Logger = logger;
        }

        public async Task<string> Write(string message)
        {
            _Logger.LogError(message);

            var s = new StackTrace();
            var logId = await WriteLog(message, s.ToString());

            return EncodeId(logId);
        }

        public async Task<string> Write(Exception exception)
        {
            _Logger.LogError(exception, exception?.Message);

            var logId = await WriteLog(exception.Message, BuildExceptionStackTrace(exception));

            return EncodeId(logId);
        }

        private async Task<int> WriteLog(string message, string? stackTrace)
        {
            try
            {
                var insert =
                    "insert into xaud.ServiceErrorLog (OccurredOn, ErrorMessage, StackTrace, UserId, VendorId, Server, Path, QueryString, PostData, SessionId) " +
                    "values (getdate(), @ErrorMessage, @StackTrace, @UserId, @VendorId, @Server, @Path, @QueryString, @PostData, @SessionId);" +
                    "select @xid = scope_identity();";

                await using var dbConn = new SqlConnection(_Settings.DBConnectionString);
                await dbConn.OpenAsync();
                await using var cmd = dbConn.CreateCommand();

                cmd.CommandText = insert;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ErrorMessage", CheckNull(message));
                cmd.Parameters.AddWithValue("@StackTrace", CheckNull(stackTrace));
                //cmd.Parameters.AddWithValue("@UserId", CheckNull(_ContextAccessor.HttpContext?.User.UserId()));
                //cmd.Parameters.AddWithValue("@VendorId", CheckNull(_ContextAccessor.HttpContext?.User.VendorId()));
                cmd.Parameters.AddWithValue("@Server", CheckNull(_ContextAccessor.HttpContext?.Request.Host.ToString()));
                cmd.Parameters.AddWithValue("@Path", CheckNull(_ContextAccessor.HttpContext?.Request.Path.ToString()));
                cmd.Parameters.AddWithValue("@QueryString", CheckNull(_ContextAccessor.HttpContext?.Request.QueryString.ToString()));
                cmd.Parameters.AddWithValue("@PostData", CheckNull(await ReadFormBody(_ContextAccessor.HttpContext?.Request)));
                cmd.Parameters.AddWithValue("@SessionId", CheckNull(GetSessionId()));

                var xid = new SqlParameter("@xid", DbType.Int32) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(xid);

                await cmd.ExecuteNonQueryAsync();

                return (int)xid.Value;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Log error in {nameof(ErrorLog)}.{nameof(WriteLog)}");
                return -1;
            }
        }

        private string? GetSessionId()
        {
            try
            {
                return _ContextAccessor.HttpContext?.Session.Id;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private async Task<string?> ReadFormBody(HttpRequest? request)
        {
            if (request == null || "GET".Equals(request.Method, StringComparison.InvariantCultureIgnoreCase) || !request.Body.CanSeek)
                return null;

            var s = request.Body;
            s.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(s, leaveOpen: true);
            return await reader.ReadToEndAsync();
        }

        private string BuildExceptionStackTrace(Exception? ex)
        {
            var s = string.Empty;

            while (ex != null)
            {
                s += $"{ex.Message} ({ex.GetType().FullName})\n{ex.StackTrace}\n";
                ex = ex.InnerException;
            }

            return s.Trim('\n');
        }

        private object CheckNull(object? value)
        {
            return value ?? DBNull.Value;
        }

        public async Task<ServiceResponse<T>> WriteServiceError<T>(Exception ex)
        {
            var logId = await Write(ex);
            var message = _Settings.EnableDetailedErrors
                ? BuildExceptionStackTrace(ex)
                : "An error has occurred.";

            return new ServiceResponse<T>
            {
                IsValid = false,
                ErrorDetail = new ServiceError()
                {
                    ErrorLogId = logId,
                    ErrorMessage = message
                }
            };
        }
    }
}

