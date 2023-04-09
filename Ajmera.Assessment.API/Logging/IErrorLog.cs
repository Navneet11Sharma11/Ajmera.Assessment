using System;
using Ajmera.Assessment.API.ApiUtilities;

namespace Ajmera.Assessment.API.Logging
{
    public interface IErrorLog
    {
        Task<string> Write(string message);
        Task<string> Write(Exception exception);

        Task<ServiceResponse<T>> WriteServiceError<T>(Exception ex);
    }
}

