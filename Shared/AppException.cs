using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace AspNetCoreWebApiProjManager.Shared
{
    [Serializable]
    public class AppException : Exception
    {
        private readonly string ResourceName;
        private readonly IList<string> ValidationErrors;
        public AppException(string message, HttpStatusCode statusCode) : base($"{statusCode}:{message}") { }
        protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceName = info.GetString("AppException.ResourceName");
            ValidationErrors = (IList<string>)info.GetValue("AppException.ValidationErrors", typeof(IList<string>));
        }
        public string GetResourceName
        {
            get { return ResourceName; }
        }
        public IList<string> GetValiadationErrors
        {
            get { return ValidationErrors; }
        }
    }
}
