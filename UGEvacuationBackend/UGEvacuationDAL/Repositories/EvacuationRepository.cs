using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class EvacuationRepository: IEvacuationRepository
    {

        public async Task<Evacuation> Get(Guid id)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuation =
                    await context.Evacuations.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
                return evacuation;
            }
        }

        public async Task<Evacuation> Create(string blockedEdgesString)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuation = new Evacuation
                {
                    BlockedEdges = string.IsNullOrEmpty(blockedEdgesString) ? null : blockedEdgesString
                };

                await context.Evacuations.AddAsync(evacuation);
                await context.SaveChangesAsync();

                return evacuation;
            }
        }
    }
}