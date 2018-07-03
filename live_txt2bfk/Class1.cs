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
        Queue<int> used = new Queue<int>();
        List<int> sorted = new List<int>();

        int cellcount = -1;
        List<Queue<int>> cells = new List<Queue<int>>();
        Queue<int> item = new Queue<int>();

        int x = 0; //cursor
        List<int> output = new List<int>();
        
        public DoCompute()
        {
            cells.Add(item);

            while (true)
            {
                int adjust = 0;

                //Create an expanding distance list the transcoder can reference with each letter.

                ConsoleKeyInfo input_key = Console.ReadKey(true);
                if (input_key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (input_key.Key == ConsoleKey.NumPad0)
                {
                    Console.Write("Sorted[] = ");
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        Console.Write(sorted[i] + ", ");
                    }
                    Console.WriteLine();
                    input_key = Console.ReadKey(true);
                }
                input.Enqueue(input_key.KeyChar);
                int[] input_as_array = input.ToArray();
                int[] dists = new int[input.Count];
                for (int i = 0; i < input.Count; i++)
                {
                    dists[i] = Math.Abs(input_as_array[input.Count - 1] - input_as_array[i]);
                }


                bool found = false;
                for (int i = input.Count - 2; i >= 0; i--)
                {
                    if (dists[i] <= 8 && found == false)
                    {
                        sorted.Add(sorted[i]);
                        cells[sorted[i]].Enqueue(input_as_array[input_as_array.Length - 1]);
                        found = true;
                    }
                }
                if (found == false)
                {
                    cellcount++;
                    sorted.Add(cellcount);
                    cells.Add(item);
                    cells[cellcount].Enqueue(input_as_array[input_as_array.Length - 1]);
                }




                /*
                Console.Write((char)input_as_array[input.Count-1] + ": ");
                for (int i = 0; i < input.Count; i++)
                {
                    Console.Write(dists[i] + ", ");
            
                }
                */
                //Console.WriteLine();

                //Begin Transcoding//
                if (output.Count < input.Count)
                {
                    output.Add(0);
                }
                while (x != sorted.Last())
                {
                    if (x > sorted.Last())
                    {
                        x--;
                        Console.Write("<");
                        // decrement bfk pointer <
                    }
                    else
                    {
                        x++;
                        Console.Write(">");
                        //increment bfk pointer >
                    }
                }
                int tmp = cells[x].Dequeue();
                //Optimize a cell on its first call, kinda
                if (output[x] == 0)
                {

                    int div = 1;

                    int count = 0;


                    for (int j = x; j < output.Count; j++)
                    {
                        if (output[j] != 0)
                        {
                            count++;
                        }

                    }
                    //Console.Write("count = {0}", count);
                    Console.Write(">"); //shift bf to next pointer to begin a counter.

                    for (int j = 0; j < count; j++)
                    {
                        Console.Write(">"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                    }


                    while (div == 1)
                    {
                        for (int j = 1; j < tmp / 2; j++)
                        {
                            if (tmp % j == 0 && j < tmp / div)
                            {
                                div = j;
                            }
                        }
                        if (div == 1)
                        {
                            adjust++;
                            tmp--;
                        }
                    }

                    for (int j = 0; j < div; j++)
                    {
                        Console.Write("+"); //do the loop this many times
                    }
                    Console.Write("["); //begin loop
                    Console.Write("<"); //go back to letter
                    for (int j = 0; j < count; j++)
                    {
                        Console.Write("<"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                    }

                    while (output[x] != tmp / div)
                    {
                        output[x]++;
                        Console.Write("+"); //increment letter
                    }

                    Console.Write(">");//go back to the counter
                    for (int j = 0; j < count; j++)
                    {
                        Console.Write(">"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                    }
                    Console.Write("-]"); //decrement counter
                    Console.Write("<"); //go back to letter
                    for (int j = 0; j < count; j++)
                    {
                        Console.Write("<"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                    }
                    output[x] = tmp;

                }
                else
                {
                    while (output[x] != tmp)
                    {
                        if (output[x] > tmp)
                        {
                            output[x]--;
                            Console.Write("-");
                            //decrement whatever output[x] is at
                        }
                        else
                        {
                            output[x]++;
                            Console.Write("+");
                            //increment whatever output[x] is at until it = the value we want
                        }
                    }
                }
                for (int j = 0; j < adjust; j++)
                {
                    Console.Write("+");
                    output[x]++;

                }
                Console.Write(".");


            }

            Console.Write("\nSorted[] = ");
            for (int i = 0; i < sorted.Count; i++)
            {
                Console.Write(sorted[i] + ", ");
            }

            Console.WriteLine();
            Console.ReadKey();
        }

    }
}
