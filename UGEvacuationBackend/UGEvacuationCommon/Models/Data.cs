using System.Collections.Generic;
using System.Linq;
using UGEvacuationCommon.Enums;

namespace UGEvacuationCommon.Models
{
    public class Data
    { 
        public static int MaximumDensity = 30;
        
        public Node Node1 = new Node(id: 1, floor: 0);
        public Node Node2 = new Node(id: 2, floor: 0, isExit: true);
        public Node Node3 = new Node(id: 3, floor: 0);
        public Node Node4 = new Node(id: 4, floor: 0);
        public Node Node5 = new Node(id: 5, floor: 0, isExit: true);
        public Node Node6 = new Node(id: 6, floor: null, isStaircase: true);
        public Node Node7 = new Node(id: 7, floor: 0);
        public Node Node8 = new Node(id: 8, floor: 0);
        public Node Node9 = new Node(id: 9, floor: 0);
        public Node Node10 = new Node(id: 10, floor: 0, isExit: true);
        public Node Node11 = new Node(id: 11, floor: null, isStaircase: true);
        public Node Node12 = new Node(id: 12, floor: 1);
        public Node Node13 = new Node(id: 13, floor: 1, isExit: true);
        public Node Node14 = new Node(id: 14, floor: 1);
        public Node Node15 = new Node(id: 15, floor: 1);
        public Node Node16 = new Node(id: 16, floor: 1);
        public Node Node17 = new Node(id: 17, floor: 1);
        public Node Node18 = new Node(id: 18, floor: null, isStaircase: true);
        public Node Node19 = new Node(id: 19, floor: null, isStaircase: true);
        public Node Node20 = new Node(id: 20, floor: 2);
        public Node Node21 = new Node(id: 21, floor: 2, isExit: true);
        public Node Node22 = new Node(id: 22, floor: 2);
        public Node Node23 = new Node(id: 23, floor: 2);
        public Node Node24 = new Node(id: 24, floor: 2);
        public Node Node25 = new Node(id: 25, floor: null, isStaircase: true);
        public Node Node26 = new Node(id: 26, floor: null, isStaircase: true);
        public Node Node27 = new Node(id: 27, floor: 3);
        public Node Node28 = new Node(id: 28, floor: 3);
        public Node Node29 = new Node(id: 29, floor: 3);
        public Node Node30 = new Node(id: 30, floor: 3);

        public List<Node> GenerateGraph(List<EdgeTemplate> blockedEdges)
        {
            AddAdjacentNodes();

            var graph = new List<Node>
            {
                Node1,
                Node2,
                Node3,
                Node4,
                Node5,
                Node6,
                Node7,
                Node8,
                Node9,
                Node10,
                Node11,
                Node12,
                Node13,
                Node14,
                Node15,
                Node16,
                Node17,
                Node18,
                Node19,
                Node20,
                Node21,
                Node22,
                Node23,
                Node24,
                Node25,
                Node26,
                Node27,
                Node28,
                Node29,
                Node30
            };

            if (blockedEdges != null)
            {
                var mappedBlockedEdges = new List<Edge>();
                foreach (var blockedEdgeId in blockedEdges)
                {
                    var fromNode = graph.First(n => n.Id == blockedEdgeId.From);
                    var toNode = graph.First(n => n.Id == blockedEdgeId.To);
                    mappedBlockedEdges.Add(new Edge(fromNode, toNode));
                }
                
                RemoveBlockedEdgesFromGraph(graph, mappedBlockedEdges);
            }

            return graph;
        }

        private void AddAdjacentNodes()
        {
            Node1.AddAdjacentNodes(new List<Node> { Node2, Node3 });
            Node3.AddAdjacentNodes(new List<Node> { Node2, Node4 });
            Node4.AddAdjacentNodes(new List<Node> { Node3, Node5, Node6, Node7 });
            Node6.AddAdjacentNodes(new List<Node> { Node4, Node16, Node18 });
            Node7.AddAdjacentNodes(new List<Node> { Node4, Node8 });
            Node8.AddAdjacentNodes(new List<Node> { Node7, Node9 });
            Node9.AddAdjacentNodes(new List<Node> { Node8, Node10, Node11 });
            Node11.AddAdjacentNodes(new List<Node> { Node9, Node12, Node19 });
            Node12.AddAdjacentNodes(new List<Node> { Node11, Node13, Node14, Node19 });
            Node14.AddAdjacentNodes(new List<Node> { Node12, Node15 });
            Node15.AddAdjacentNodes(new List<Node> { Node14, Node16 });
            Node16.AddAdjacentNodes(new List<Node> { Node6, Node15, Node18 });
            Node17.AddAdjacentNodes(new List<Node> { Node16 });
            Node18.AddAdjacentNodes(new List<Node> { Node6, Node16, Node24, Node25 });
            Node19.AddAdjacentNodes(new List<Node> { Node11, Node12, Node20, Node26 });
            Node20.AddAdjacentNodes(new List<Node> { Node19, Node21, Node22, Node26 });
            Node22.AddAdjacentNodes(new List<Node> { Node20, Node23 });
            Node23.AddAdjacentNodes(new List<Node> { Node22, Node24 });
            Node24.AddAdjacentNodes(new List<Node> { Node18, Node23, Node25 });
            Node25.AddAdjacentNodes(new List<Node> { Node18, Node24, Node30 });
            Node26.AddAdjacentNodes(new List<Node> { Node19, Node20, Node27 });
            Node27.AddAdjacentNodes(new List<Node> { Node26, Node28 });
            Node28.AddAdjacentNodes(new List<Node> { Node27, Node29 });
            Node29.AddAdjacentNodes(new List<Node> { Node28, Node30 });
            Node30.AddAdjacentNodes(new List<Node> { Node29, Node25 });
        }

        private void RemoveBlockedEdgesFromGraph(List<Node> graph, List<Edge> blockedEdges)
        {
            foreach (var node in graph)
            {
                var adjacentNodesToRemove = new List<Node>();
                foreach (var adjacentNode in node.AdjacentNodes)
                {
                    var nodeBlockedEdge = blockedEdges.FirstOrDefault(e => e == new Edge(node, adjacentNode));
                    if (nodeBlockedEdge != null)
                    {
                        adjacentNodesToRemove.Add(adjacentNode);
                    }
                }

                node.AdjacentNodes.RemoveAll(n => adjacentNodesToRemove.Contains(n));
            }
        }

        public static List<int> GetAllStartNodesIds()
        {
            var startNodeIds = new List<int>
            {
                { 1 },
                { 3 },
                { 4 },
                { 7 },
                { 8 },
                { 9 },
                { 12 },
                { 14 },
                { 15 },
                { 16 },
                { 17 },
                { 20 },
                { 22 },
                { 23 },
                { 24 },
                { 27 },
                { 28 },
                { 29 },
                { 30 },
            };

            return startNodeIds;
        }
    }
}