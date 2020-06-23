using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UGEvacuationDAL.Entities;
using UGEvacuationDAL.Repositories.Interfaces;

namespace UGEvacuationDAL.Repositories
{
    public class EvacuationNodeRepository: IEvacuationNodeRepository
    {
        public async Task Create(int nodeId, Guid evacuationId)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuationNode = new EvacuationNode()
                {
                    NodeId = nodeId,
                    EvacuationId = evacuationId
                };

                context.EvacuationsNodes.Add(evacuationNode);
                await context.SaveChangesAsync();
            }
        }

        public async Task<EvacuationNode> AddDensity(int nodeId, Guid evacuationId, int density)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuationNode = await GetByEvacuationIdAndNodeId(nodeId, evacuationId);
                evacuationNode.Density += density;
                context.EvacuationsNodes.Update(evacuationNode);
                await context.SaveChangesAsync();
                return evacuationNode;
            }
        }

        public async Task<EvacuationNode> GetByEvacuationIdAndNodeId(int nodeId, Guid evacuationId)
        {
            using (var context = new UGEvacuationContext())
            {
                var evacuationNode = await context.EvacuationsNodes.FirstOrDefaultAsync(u =>
                    u.NodeId == nodeId && u.EvacuationId == evacuationId && u.IsDeleted == false);
                return evacuationNode;
            }
        }

        public async Task<List<EvacuationNode>> SearchByEvacuationId(Guid evacuationId)
        {
            using (var context = new UGEvacuationContext())
            {
                return await context.EvacuationsNodes.Where(en => en.EvacuationId == evacuationId && en.IsDeleted == false)
                    .ToListAsync();
            }
        }
    }
}