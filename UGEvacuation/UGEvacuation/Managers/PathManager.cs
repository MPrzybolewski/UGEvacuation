using System.Collections.Generic;
using System.Linq;
using UGEvacuation.Models;

namespace UGEvacuation.Managers
{
    public class PathManager
    {
        public static List<Path> GetRescuePathForNode(Node node, List<Node> alreadySelectedNodes)
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
                                pathsForCurrentNode.AddRange(GetRescuePathForNode(adjacentNode, updatedSelectedNodesList));
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
                                    pathsForCurrentNode.AddRange(GetRescuePathForNode(adjacentNode, updatedSelectedNodesList));
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
                                    pathsForCurrentNode.AddRange(GetRescuePathForNode(adjacentNode, updatedSelectedNodesList));
                                }
                            }
                        }
                    }
                }
            }
            return pathsForCurrentNode;
        }
    }
}