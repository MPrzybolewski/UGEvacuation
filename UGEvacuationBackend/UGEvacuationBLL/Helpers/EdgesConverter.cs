using System;
using System.Collections.Generic;
using System.Linq;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Helpers
{
    public static class EdgesConverter
    {
        public static string GetBlockedEdgesString(List<EdgeTemplate> blockedEdges)
        {
            if (blockedEdges != null)
            {
                return string.Join(',', blockedEdges.Select(e => e.From.ToString() + '-' + e.To.ToString()));   
            }

            return null;
        }
        
        public static List<EdgeTemplate> GetListByString(string blockedEdgesString)
        {
            return blockedEdgesString?.Split(',').Select(s => s.Split('-')).Select(s => new EdgeTemplate
            {
                From = Int32.Parse(s[0]),
                To = Int32.Parse(s[1])
            }).ToList();
        }
    }
}