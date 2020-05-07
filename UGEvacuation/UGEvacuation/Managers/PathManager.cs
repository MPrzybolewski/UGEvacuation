using System.Collections.Generic;
using System.Linq;
using UGEvacuation.Models;

namespace UGEvacuation.Managers
{
    public class PathManager
    {
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

        public Dictionary<int, Path> GetBestPaths(Dictionary<int, List<Path>> pathsForNodeIds)
        {
            bool isValid = false;
            Edge? edgeWithExceededDensity = null;
            Dictionary<int, Path> shortestPathsForNodeIds;
            
            while (!isValid)
            {
                shortestPathsForNodeIds = GetShortestPathsForNodeIds(pathsForNodeIds);

                var bucketList = CreateBucketList(shortestPathsForNodeIds);

                edgeWithExceededDensity = GetEdgeWithExceededDensity(bucketList);

                if (edgeWithExceededDensity == null)
                {
                    isValid = true;
                }
                else
                {
                    var pathsWithExceededDensity = shortestPathsForNodeIds.Where(sp => sp.Value.HasEdge(edgeWithExceededDensity)).Select(sp => sp.Value);
                }
            }
            

            return shortestPathsForNodeIds;
        }

        private Edge? GetEdgeWithExceededDensity(Bucket[] bucketList)
        {
            foreach (var bucket in bucketList)
            {
                var bucketWithExceededDensity = bucket.BucketElements.FirstOrDefault(be => be.Density > 30);
                if (bucketWithExceededDensity != null)
                {
                    return bucketWithExceededDensity.Edge;
                }
            }

            return null;
        }

        private Bucket[] CreateBucketList(Dictionary<int, Path> pathsForNodeIds)
        {
            var allPaths = pathsForNodeIds.Select(p => p.Value).ToList();
            int maxPathElements = allPaths.Max(p => p.NodesList.Count);

            var bucketList = new Bucket[maxPathElements];
            for (int i = 0; i < bucketList.Length; i++)
            {
                bucketList[i].TimeInstant = i + 1;
                foreach (var nodeId in pathsForNodeIds.Keys)
                {
                    var edge = new Edge(pathsForNodeIds[nodeId].NodesList[i-1], pathsForNodeIds[nodeId].NodesList[i]);
                    bucketList[i].AddEdgeOrDensity(edge);
                }
            }
        }

        private Dictionary<int, Path> GetShortestPathsForNodeIds(Dictionary<int, List<Path>> pathsForNodeIds)
        {
            var shortestPaths = new Dictionary<int, Path>();
            foreach (var nodeId in pathsForNodeIds.Keys)
            {
                var shortestPath = pathsForNodeIds[nodeId].Where(p => !p.IsRejected).OrderBy(p => p.NodesList.Count).First();
                shortestPaths.Add(nodeId, shortestPath);
            }

            return shortestPaths;
        }
    }
}