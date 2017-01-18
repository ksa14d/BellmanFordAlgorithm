using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    class Bellman
    {
        Dictionary<int, string> Cities;
        const int x = int.MaxValue;
        int[,] c = new int[,] 
        { { 0,x,x,6,x,x,x,1,5,x,x }, 
          { x,0,x,x,8,3,1,x,x,x,x }, 
          { x,x,0,4,x,x,x,x,x,2,1 },            
          { 6,x,4,0,x,x,x,2,x,4,x },
          { x,8,x,x,0,x,x,x,2,x,x },
          { x,3,x,x,x,0,2,7,x,x,9 },
          { x,1,x,x,x,2,0,3,3,x,x },
          { 1,x,x,2,x,7,3,0,x,x,8 },
          { 5,x,x,x,2,x,3,x,0,x,x },
          { x,x,2,4,x,x,x,x,x,0,x },
          { x,x,1,x,x,9,x,8,x,x,0 },};

        List<int> PreviouslyProcessed = new List<int>();

        List<int> Q = new List<int>();
        List<List<KeyValuePair<int, int>>> FromValue = new List<List<KeyValuePair<int, int>>>();
        int Source = 7;
        public Bellman()
        {
            Cities = new Dictionary<int, string>() { 
            {0,"La"},{1,"Lo"},{2,"M"},{3,"ND"},{4,"NY"},{5,"O"},{6,"P"},{7,"R"},{8,"SA"},{9,"SY"},{10,"T"}, {-1,"-"}
            };
            Q.Add(Source);
            List<KeyValuePair<int, int>> distance = new List<KeyValuePair<int, int>>();
            for (int i = 0; i <= 10; i++)
            {
                if (i != Source)
                {
                    distance.Add(new KeyValuePair<int, int>( -1, x));
                }
                else
                {
                    distance.Add(new KeyValuePair<int, int>( -1, 0));
                }
            }
            FromValue.Add(distance);
            for (int i = 0; i < 13; i++)
            {
                ShortestDistance();
            }
        }


        private void ShortestDistance()
        {
            while (Q.Count != 0)
            {
                List<KeyValuePair<int, int>> distance = new List<KeyValuePair<int, int>>();
                int NodeToProcess = Q[0];
                if (!PreviouslyProcessed.Contains(NodeToProcess))
                {
                    PreviouslyProcessed.Add(NodeToProcess);
                }
                Q.RemoveAt(0);
                int PrevValues = FromValue.Count - 1;
                KeyValuePair<int, int> Di = FromValue[PrevValues].ElementAt(NodeToProcess);
                List<int> TempQ = new List<int>();
                for (int i = 0; i <= 10; i++)
                {
                    KeyValuePair<int, int> prevDist = FromValue[PrevValues].ElementAt(i);
                    if (c[NodeToProcess, i] != x && c[NodeToProcess, i] != 0)  // only for neighbor
                    {

                        if (prevDist.Value > Di.Value + c[NodeToProcess, i])
                        {
                            distance.Add(new KeyValuePair<int, int>(NodeToProcess, Di.Value + c[NodeToProcess, i]));
                            TempQ.Add(i);
                        }
                        else
                        {

                            distance.Add(new KeyValuePair<int, int>(prevDist.Key, prevDist.Value));
                        }
                    }
                    else
                    {

                        distance.Add(new KeyValuePair<int, int>(prevDist.Key, prevDist.Value));
                    }
                }
                if (TempQ.Count > 0)
                {
                    TempQ.Sort(); // maintains alphabatic order
                    for (int i = 0; i < TempQ.Count; i++)
                    {
                        if (PreviouslyProcessed.Contains(TempQ[i]))
                        {
                            if (!Q.Contains(TempQ[i]))
                            {
                                Q.Insert(0, TempQ[i]);
                            }
                        }
                        else
                        {
                            if (!Q.Contains(TempQ[i]))
                            {
                                Q.Add(TempQ[i]);
                            }
                        }
                    }
                }
                FromValue.Add(distance);
                for (int i = 0; i < Q.Count; i++)
                    Console.Write(Cities[Q[i]] + " ");

                for (int i = 0; i < distance.Count; i++)
                    Console.Write( " ("+Cities[distance[i].Key]+","+(distance[i].Value==int.MaxValue ? "i" : distance[i].Value.ToString())+")" );
                Console.WriteLine("\n");
            } 
        }
    }
}
