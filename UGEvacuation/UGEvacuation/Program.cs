using System;
using System.Collections.Generic;
using System.Linq;
using UGEvacuation.Managers;
using UGEvacuation.Models;

namespace UGEvacuation
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Data.GenerateGraph();

            var paths = new List<Path>();

            var startNodeIds = new Dictionary<int, List<Path>>
            {
                { 1, new List<Path>() },
                { 7, new List<Path>() },
                { 15, new List<Path>() },
            };

            foreach (var node in graph.Where(n => startNodeIds.Keys.Contains(n.Id)))
            {
                var pathsForNode = PathManager.GetRescuePathForNode(node, new List<Node>() { node }).Where(p => p.NodesList != null).ToList();
                startNodeIds[node.Id] = pathsForNode;
                paths.AddRange(pathsForNode);
            }

            int maxLengthPath = paths.Max(p => p.NodesList.Count);
        }
    }
}