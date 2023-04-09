using System;
namespace Ajmera.Assessment.API.ApiUtilities
{
    /// <summary>
    /// Standard response object for service apis
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// True if responsedata ok, false if there is an error
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public ServiceError? ErrorDetail { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public T? ResponseData { get; set; }

        /// <summary>
        /// Utility method to create a failure ServiceResponse
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorLogId"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Failure(string message, string? errorLogId = null)
        {
            return new ServiceResponse<T>()
            {
                IsValid = false,
                ErrorDetail = new ServiceError()
                {
                    ErrorLogId = errorLogId,
                    ErrorMessage = message
                }
            };
        }

        /// <summary>
        /// Implicit operator to wrap results in a ServiceResponse object
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static implicit operator ServiceResponse<T>(T response)
        {
            return new ServiceResponse<T>()
            {
                IsValid = true,
                ResponseData = response
            };
        }
    }
}

