using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class EvacuationNodeAppUserRepository: IEvacuationNodeAppUserRepository
    {
        public async Task<EvacuationNodeAppUser> Create(Guid appUserId, Guid evacuationNodeId)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuationNodeAppUser = new EvacuationNodeAppUser
                {
                    AppUserId = appUserId,
                    EvacuationNodeId = evacuationNodeId
                };

                context.EvacuationsNodesAppUsers.Add(evacuationNodeAppUser);
                await context.SaveChangesAsync();
                return evacuationNodeAppUser;
            }
        }

        public async Task<IEnumerable<EvacuationNodeAppUser>> SearchByEvacuationIdAndNodeId(Guid evacuationId, int nodeId)
        {
            using (var context = new UGEvacuationContext())
            {
                return await context.EvacuationsNodesAppUsers.Include(enau => enau.AppUser)
                    .Include(enau => enau.EvacuationNode).Where(enau =>
                        enau.EvacuationNode.EvacuationId == evacuationId &&
                        enau.EvacuationNode.EvacuationId == evacuationId).ToListAsync();
            }
        }
    }
}