using System.Collections.Generic;
using System.Linq;
using UGEvacuationCommon.Enums;
using UGEvacuationCommon.Models;

namespace UGEvacuationBLL.Helpers
{
    public static class PathManager
    {
        public static Dictionary<int, Path> GetBestPathForNodesIds(List<Node> graph, Dictionary<int, int> startNodesIdsWithDensities)
        {
            var pathsForNodeIds = new Dictionary<NodeWithDensity, List<Path>>();
            
            foreach (var startNode in graph.Where(n => startNodesIdsWithDensities.Keys.Contains(n.Id)))
            {
                var nodeWithDensity = new NodeWithDensity(startNode, startNodesIdsWithDensities[startNode.Id]);
                pathsForNodeIds.Add(nodeWithDensity, new List<Path>());
                var pathsForNode = GetAllRescuePathsForNode(startNode, new List<Node>() { startNode }).Where(p => p.NodesList != null).ToList();
                pathsForNodeIds[nodeWithDensity] = pathsForNode;
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

        public static Dictionary<int, Path> GetBestPaths(Dictionary<NodeWithDensity, List<Path>> pathsForNodeIds)
        {
            bool isValid = false;
            Edge? edgeWithExceededDensity;
            Dictionary<NodeWithDensity, List<Path>> orderedPathsForNodes;
            Dictionary<NodeWithDensity, Path> shortestPathsForNodes;

            if (pathsForNodeIds.Values.Any(p => p == null || p.Count == 0))
                throw new UGEvacuationException("GetBestPaths - there is node without any path", type: ErrorType.NoPath);
            
            while (true)
            {
                orderedPathsForNodes = OrderPathsByLength(pathsForNodeIds);

                shortestPathsForNodes = orderedPathsForNodes.ToDictionary(p => p.Key, p => p.Value[0]);

                var bucketList = CreateBucketList(shortestPathsForNodes);

                edgeWithExceededDensity = GetEdgeWithExceededDensity(bucketList);

                if (edgeWithExceededDensity == null)
                {
                    return shortestPathsForNodes.Select(s => new KeyValuePair<int,Path>(s.Key.Node.Id, s.Value)).ToDictionary(x=>x.Key, x=>x.Value);
                }
                
                var nodeIdsForPathsWithExceededDensity = shortestPathsForNodes.Where(sp => sp.Value.HasEdge(edgeWithExceededDensity)).Select(sp => sp.Key).ToList();
                var nodeIdWithNextShortestPath = orderedPathsForNodes
                    .Where(sp => nodeIdsForPathsWithExceededDensity.Contains(sp.Key) && sp.Value.Count > 1)
                    .OrderBy(sp => sp.Value[1].NodesList.Count).Select(sp => sp.Key).FirstOrDefault();
                
                if (nodeIdWithNextShortestPath == null || !nodeIdsForPathsWithExceededDensity.Contains(nodeIdWithNextShortestPath))
                {
                    return shortestPathsForNodes.Select(s => new KeyValuePair<int,Path>(s.Key.Node.Id, s.Value)).ToDictionary(x=>x.Key, x=>x.Value);
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

        private static List<Bucket> CreateBucketList(Dictionary<NodeWithDensity, Path> shortestPathsForNodes)
        {
            int maxPathElements = shortestPathsForNodes.Values.Max(p => p.NodesList.Count);

            var bucketList = new List<Bucket>();
            for (int i = 0; i < maxPathElements; i++)
            {
                var bucket = new Bucket()
                {
                    TimeInstant = i + 1
                };
                
                foreach (var node in shortestPathsForNodes.Keys)
                {
                    if (shortestPathsForNodes[node].NodesList.Count - 1 > i)
                    {
                        var edge = new Edge(shortestPathsForNodes[node].NodesList[i], shortestPathsForNodes[node].NodesList[i+1]);
                        bucket.AddEdgeOrDensity(edge, node.Density);   
                    }
                }
                bucketList.Add(bucket);
            }

            return bucketList;
        }

        private static Dictionary<NodeWithDensity, List<Path>> OrderPathsByLength(Dictionary<NodeWithDensity, List<Path>> pathsForNodeIds)
        {
            var orderedPaths = new Dictionary<NodeWithDensity, List<Path>>();
            foreach (var nodeWithDensity in pathsForNodeIds.Keys)
            {
                var orderedPathsForNode = pathsForNodeIds[nodeWithDensity].OrderBy(p => p.NodesList.Count).ToList();
                orderedPaths.Add(nodeWithDensity, orderedPathsForNode);
            }

            return orderedPaths;
        }
    }
}