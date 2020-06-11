using System.Collections.Generic;
using System.Linq;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Helpers
{
    public static class PathManager
    {

        public static Dictionary<int, Path> GetBestPathForNodesIds(List<Node> graph, List<int> startNodesIds)
        {
            var pathsForNodeIds = new Dictionary<int, List<Path>>();

            foreach (var startNodeId in startNodesIds)
            {
                pathsForNodeIds.Add(startNodeId, new List<Path>());
            }
            
            foreach (var startNode in graph.Where(n => startNodesIds.Contains(n.Id)))
            {
                var pathsForNode = GetAllRescuePathsForNode(startNode, new List<Node>() { startNode }).Where(p => p.NodesList != null).ToList();
                pathsForNodeIds[startNode.Id] = pathsForNode;
            }

            return GetBestPaths(pathsForNodeIds);
        }
        public static List<Path> GetAllRescuePathsForNode(Node node, List<Node> alreadySelectedNodes)
        {
            List<Path> pathsForCurrentNode = new List<Path>();

            foreach(var adjacentNode in node.AdjacentNodes)
            {
                if(!alreadySelectedNodes.Contains(adjacentNode))
                {
                    var updatedSelectedNodesList = new List<Node>(alreadySelectedNodes);
                    updatedSelectedNodesList.Add(adjacentNode);
                    if (adjacentNode.IsExit)
                    {
                        pathsForCurrentNode.Add(new Path()
                        {
                            NodesList = updatedSelectedNodesList
                        });   
                    }
                    else
                    {
                        if(!adjacentNode.IsStaircase && alreadySelectedNodes.Last().IsStaircase)
                        {
                            var nodeBeforeLastStaircase = alreadySelectedNodes[alreadySelectedNodes.Count - 2];

                            if(nodeBeforeLastStaircase.IsStaircase && nodeBeforeLastStaircase.AdjacentNodes.Contains(adjacentNode))
                            {
                                pathsForCurrentNode.Add(new Path());
                            }
                            else
                            {
                                pathsForCurrentNode.AddRange(GetAllRescuePathsForNode(adjacentNode, updatedSelectedNodesList));
                            }
                        }
                        else
                        {
                            if(adjacentNode.IsStaircase && alreadySelectedNodes.Last().IsStaircase)
                            {
                                var lastNotStaircaseNode = alreadySelectedNodes.Last(n => !n.IsStaircase);
                                if(lastNotStaircaseNode.AdjacentNodes.Contains(adjacentNode))
                                {
                                    pathsForCurrentNode.Add(new Path());
                                }
                                else
                                {
                                    pathsForCurrentNode.AddRange(GetAllRescuePathsForNode(adjacentNode, updatedSelectedNodesList));
                                }
                            }
                            else
                            {
                                if(alreadySelectedNodes.Count() > 2 && adjacentNode.IsStaircase && !alreadySelectedNodes.Last().IsStaircase && alreadySelectedNodes[alreadySelectedNodes.Count - 2].IsStaircase)
                                {
                                    pathsForCurrentNode.Add(new Path());
                                }
                                else
                                {
                                    pathsForCurrentNode.AddRange(GetAllRescuePathsForNode(adjacentNode, updatedSelectedNodesList));
                                }
                            }
                        }
                    }
                }
            }
            return pathsForCurrentNode;
        }

        public static Dictionary<int, Path> GetBestPaths(Dictionary<int, List<Path>> pathsForNodeIds)
        {
            bool isValid = false;
            Edge? edgeWithExceededDensity;
            Dictionary<int, List<Path>> orderedPathsForNodeIds;
            Dictionary<int, Path> shortestPathsForNodeIds;

            if (pathsForNodeIds.Values.Any(p => p == null || p.Count == 0))
                throw new UGEvacuationException("GetBestPaths - there is node without any path", type: ErrorType.NoPath);
            
            while (true)
            {
                orderedPathsForNodeIds = OrderPathsByLength(pathsForNodeIds);

                shortestPathsForNodeIds = orderedPathsForNodeIds.ToDictionary(p => p.Key, p => p.Value[0]);

                var bucketList = CreateBucketList(shortestPathsForNodeIds.Values.ToList());

                edgeWithExceededDensity = GetEdgeWithExceededDensity(bucketList);

                if (edgeWithExceededDensity == null)
                {
                    return shortestPathsForNodeIds;
                }
                
                var nodeIdsForPathsWithExceededDensity = shortestPathsForNodeIds.Where(sp => sp.Value.HasEdge(edgeWithExceededDensity)).Select(sp => sp.Key).ToList();
                var nodeIdWithNextShortestPath = orderedPathsForNodeIds
                    .Where(sp => nodeIdsForPathsWithExceededDensity.Contains(sp.Key) && sp.Value.Count > 1)
                    .OrderBy(sp => sp.Value[1].NodesList.Count).Select(sp => sp.Key).FirstOrDefault();
                
                if (!nodeIdsForPathsWithExceededDensity.Contains(nodeIdWithNextShortestPath))
                {
                    return shortestPathsForNodeIds;
                }

                pathsForNodeIds[nodeIdWithNextShortestPath].RemoveAt(0);
            }
        }

        private static Edge? GetEdgeWithExceededDensity(List<Bucket> bucketList)
        {
            foreach (var bucket in bucketList)
            {
                var bucketWithExceededDensity = bucket.BucketElements.FirstOrDefault(be => be.Density > Data.MaximumDensity);
                if (bucketWithExceededDensity != null)
                {
                    return bucketWithExceededDensity.Edge;
                }
            }

            return null;
        }

        private static List<Bucket> CreateBucketList(List<Path> shortestPathsForNodeIds)
        {
            int maxPathElements = shortestPathsForNodeIds.Max(p => p.NodesList.Count);

            var bucketList = new List<Bucket>();
            for (int i = 0; i < maxPathElements; i++)
            {
                var bucket = new Bucket()
                {
                    TimeInstant = i + 1
                };
                
                foreach (var path in shortestPathsForNodeIds)
                {
                    if (path.NodesList.Count - 1 > i)
                    {
                        var edge = new Edge(path.NodesList[i], path.NodesList[i+1]);
                        bucket.AddEdgeOrDensity(edge);   
                    }
                }
                bucketList.Add(bucket);
            }

            return bucketList;
        }

        private static Dictionary<int, List<Path>> OrderPathsByLength(Dictionary<int, List<Path>> pathsForNodeIds)
        {
            var orderedPaths = new Dictionary<int, List<Path>>();
            foreach (var nodeId in pathsForNodeIds.Keys)
            {
                var orderedPathsForNode = pathsForNodeIds[nodeId].OrderBy(p => p.NodesList.Count).ToList();
                orderedPaths.Add(nodeId, orderedPathsForNode);
            }

            return orderedPaths;
        }
    }
}