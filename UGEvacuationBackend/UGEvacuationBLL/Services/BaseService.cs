using System;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Services
{
    public class BaseService
    {
        protected UGEvacuationException HandleExcpetion(Exception ex, string additionalInfo)
        {
            if (ex is UGEvacuationException ugEvacuationException)
            {
                return new UGEvacuationException(additionalInfo, ugEvacuationException, ugEvacuationException.Type);
            }
            
            return new UGEvacuationException(additionalInfo, ex);
        }
    }
}