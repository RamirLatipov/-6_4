using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лабраторная_6_4
{
   
    class Program
    {
        //<summary>
        // Находит и выводит кратчайший путь (по числу рёбер) между start и end.
        // graph — список смежности: graph[v] = список соседей вершины v.
        // </summary>
        static void PrintShortestPath(Dictionary<int, List<int>> graph, int start, int end)
        {
            if (start == end)
            {
                Console.WriteLine(start);
                return;
            }

            var queue = new Queue<int>();
            var parent = new Dictionary<int, int?>(); // вершина -> откуда пришли

            queue.Enqueue(start);
            parent[start] = null;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();

                if (!graph.TryGetValue(v, out var neighbors)) continue;

                foreach (int to in neighbors)
                {
                    if (parent.ContainsKey(to)) continue; // уже посещена

                    parent[to] = v;

                    if (to == end)
                    {
                        // Восстанавливаем путь от end к start
                        var path = new List<int>();
                        int? cur = end;
                        while (cur != null)
                        {
                            path.Add(cur.Value);
                            cur = parent[cur.Value];
                        }
                        path.Reverse();

                        Console.WriteLine(string.Join(" -> ", path));
                        return;
                    }

                    queue.Enqueue(to);
                }
            }

            Console.WriteLine("Путь не найден");
        }

        static void Main()
        {
            var graph = new Dictionary<int, List<int>>
            {
                [1] = new List<int> { 2, 3 },
                [2] = new List<int> { 1, 4 },
                [3] = new List<int> { 1, 4, 5 },
                [4] = new List<int> { 2, 3, 5 },
                [5] = new List<int> { 3, 4 }
            };

            PrintShortestPath(graph, 1, 5);
        }
    }
}
