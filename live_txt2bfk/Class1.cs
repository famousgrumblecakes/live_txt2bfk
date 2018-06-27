using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace live_txt2bfk
{
    class DoCompute
    {

        Queue<int> input = new Queue<int>();


        public DoCompute()
        {
            while (true)
            {

                //Create an expanding distance table the printer can reference with each letter. Honestly, the only distances we should care about are the distances from each prior letter to the one we just entered. O(n^3) -> O(n^2).

                ConsoleKeyInfo input_key = Console.ReadKey(true);
                input.Enqueue(input_key.KeyChar);
                int[][] dists = new int[input.Count][];
                int[] input_as_array = input.ToArray();
                for (int i = 0; i < input.Count; i++)
                {
                    dists[i] = new int[input.Count];
                    for (int k = 0; k < input.Count; k++)
                    {
                        dists[i][k] = Math.Abs(input_as_array[i] - input_as_array[k]);
                    }
                }

                /*
                
                for (int i = 0; i < input.Count; i++)
                {
                    Console.Write(input_as_array[i] + ": ");
                    for (int k = 0; k < input.Count; k++)
                    {
                        Console.Write(dists[i][k] + ", ");
                    }
                    Console.WriteLine();
                }
                
                */

            }
        }

    }
}
