using System;
namespace Ajmera.Assessment.API.Logging
{
    public class Settings
    {
        public string AcceptITQueueName { get; set; }
        public string ApplicationInsightsConnectionString { get; set; }
        public string ApplicationInsightsInstrumentationKey { get; set; }
        public string DBConnectionString { get; set; }
        public string Environment { get; set; }
        public string JwtTokenIssuer { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string StorageAccountAccessKeyPrivate { get; set; }
        public string StorageAccountAccessKeyPublic { get; set; }
        public string StorageAccountNamePrivate { get; set; }
        public string StorageAccountNamePublic { get; set; }
        public bool EnableDetailedErrors { get; set; } = false;
        public string VendorDocumentSafeMS { get; set; }
    }
}

