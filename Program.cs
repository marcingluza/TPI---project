using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace graf
{
    public class Edge
    {
        string id;
        string from;
        string to;
        int weight;

        public Edge(string _id, string _from, string _to, int _weight)
        {
            this.id = _id;
            this.from = _from;
            this.to = _to;
            this.Weight = _weight;
        }

        public string Id { get => id; set => id = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public int Weight { get => weight; set => weight = value; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            List<Edge> listOfEdges = new List<Edge>();
            List<String> listOfNodes = new List<String>();

            int nodesCount = 0;
            int edgesCount = 0;

            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode_node, xmlnode_edge;
            int i = 0;
            FileStream fs = new FileStream(@"graf.xml", FileMode.Open, FileAccess.Read);
            Console.WriteLine("Otwieranie graf.xml");
            xmldoc.Load(fs);
            xmlnode_node = xmldoc.GetElementsByTagName("node");
            xmlnode_edge = xmldoc.GetElementsByTagName("edge");

            for (i = 0; i <= xmlnode_node.Count - 1; i++)
            {
                string str_id = xmlnode_node[i].Attributes["id"].Value;
                listOfNodes.Add(str_id);
            }
            nodesCount = xmlnode_node.Count - 1;
            Console.WriteLine("Ilosc wezłow: {0}", nodesCount + 1);

            string[] edgeArray = new string[xmlnode_node.Count];

            for (int l = 0; l < xmlnode_node.Count; l++)
            {
                edgeArray[l] = listOfNodes[l];
                Console.WriteLine("Wezel nr {1}", l, edgeArray[l]);

            }

            Console.WriteLine();
            Console.WriteLine("Ilosc sciezek: " + xmlnode_edge.Count);

            for (i = 0; i <= xmlnode_edge.Count - 1; i++)
            {
                string str_id = xmlnode_edge[i].Attributes["id"].Value;
                string str_from = xmlnode_edge[i].Attributes["from"].Value;
                string str_to = xmlnode_edge[i].Attributes["to"].Value;
                int int_weight = Convert.ToInt16(xmlnode_edge[i].Attributes["weight"].Value);
                Edge Temp = new Edge(str_id, str_from, str_to, int_weight);
                Console.WriteLine("Sciezka nr {0} od: {1}  do: {2}  waga: {3}", i+1, str_from, str_to, int_weight);
                listOfEdges.Add(Temp);

            }
            edgesCount = xmlnode_edge.Count - 1;


            int xAxis = nodesCount;
            int yAxis = nodesCount;

            int[,] costMatrix = new int[xAxis, yAxis];

            for(int  k = 0; k < xAxis; k++)
            {
                for (int j = 0; j < yAxis; j++)
                {
                    costMatrix[k, j] = 0;
                }
            }

            for (int k = 0; k < edgesCount + 1; k++)
            {
                //macierz kosztów
                for (int j = 0; j < nodesCount; j++)
                {
                    if (listOfEdges[k].From == edgeArray[j])
                        xAxis = j;
                    if (listOfEdges[k].To == edgeArray[j])
                        yAxis = j;
                }
                costMatrix[xAxis, yAxis] = listOfEdges[k].Weight;
                costMatrix[yAxis, xAxis] = listOfEdges[k].Weight;
            }

            Console.WriteLine();
            Console.WriteLine("Macierz kosztów: ");

            for (int k = 0; k < costMatrix.GetLength(0); k++)
            {
                for (int j = 0; j < costMatrix.GetLength(1); j++)
                {
                    Console.Write(costMatrix[k, j] + " ");
                }
                Console.WriteLine();
            }



            Console.WriteLine();
            Console.WriteLine("Najlepsze dopasowanie: ");

         

        }


    }
}
