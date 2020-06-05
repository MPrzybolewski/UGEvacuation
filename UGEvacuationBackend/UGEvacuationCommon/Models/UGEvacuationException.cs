using System;
using UGEvacuationCommon.Enums;

namespace UGEvacuationCommon.Models
{
    public class UGEvacuationException : Exception
    {
        public ErrorType Type { get; set; }
        
        public UGEvacuationException(string message,Exception innerException=null,ErrorType? type=null) : base($"UGEvacuationException: {message}", innerException)
        {
            Type = ErrorType.Unspecified;

            if (innerException is UGEvacuationException innerUGEvacuationException)
            {
                Type = innerUGEvacuationException.Type;
            }

            if (type.HasValue)
                Type = type.Value;
        }
    }
}