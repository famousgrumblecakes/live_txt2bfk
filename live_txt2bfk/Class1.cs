using System;
using System.Collections.Generic;
using System.Linq;



namespace live_txt2bfk
{
    class DoCompute
    {

        Queue<int> input = new Queue<int>();
        List<int> sorted = new List<int>();
        List<int> spread = new List<int>();

        int cellcount = -1;
        List<Queue<int>> cells = new List<Queue<int>>();
        Queue<int> item = new Queue<int>();

        int x = 0; //cursor
        List<int> output = new List<int>();

        string currentinput;

        public DoCompute()
        {
            cells.Add(item);
            ConsoleKeyInfo input_key = new ConsoleKeyInfo();
            while (input_key.Key != ConsoleKey.Enter)
            {

                //Create an expanding distance list the transcoder can reference with each letter.

                input_key = Console.ReadKey(true);
                if (input_key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (input_key.Key == ConsoleKey.NumPad0)
                {
                    Console.WriteLine("\n"+currentinput);
                    input_key = Console.ReadKey(true);
                }
                currentinput = currentinput + input_key.KeyChar;
                input.Enqueue(input_key.KeyChar);
                int[] input_as_array = input.ToArray();
                int[] dists = new int[input.Count];
                for (int i = 0; i < input.Count; i++)
                {
                    dists[i] = Math.Abs(input_as_array[input.Count - 1] - input_as_array[i]);
                }


                bool found = false;
                int current_low = 5;
                int index = new int();
                for (int i = input.Count - 2; i >= 0; i--)
                {
                    if (dists[i] <= current_low)
                    {
                        current_low = dists[i];

                        index = i;
                        found = true;
                    }
                }
                if(found == true)
                {
                    sorted.Add(sorted[index]);
                    cells[sorted[index]].Enqueue(input_as_array[input_as_array.Length - 1]);
                    //Console.Write(sorted[index] + ": " + input_key.KeyChar + ": ");
                }
                else
                {
                    cellcount++;
                    output.Add(0);
                    sorted.Add(cellcount);
                    cells.Add(item);
                    cells[cellcount].Enqueue(input_as_array[input_as_array.Length - 1]);
                    //Console.Write(cellcount + ": " + input_key.KeyChar + ": ");

                }


                //Begin Transcoding//
                int adjust = 0;
                bool nonl = false;
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
                    //Console.Write("!");
                    int div = 1;

                    int count = 0;



                    Console.Write(">"); //shift bf to next pointer to begin a counter.

                    for (int j = x; output[j]!=0; j++)
                    {
                        Console.Write(">"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                        count++;
                    }


                    while (div == 1) //Find a divisor. numerator won't be negative because starting at 0.
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
                else if (Math.Abs(tmp - output[x]) > 9)
                {
                    tmp = tmp - output[x];
                    adjust = 0;
                    int div = 1;
                    int count = 0;

                    Console.Write(">"); //shift bf to next pointer to begin a counter.

                    output.Add(0);
                    for (int j = x; output[j]!=0; j++)
                    {
                        Console.Write(">"); //adjust for when we have to put the counter in a weird place. j is for adjust.
                        count++;
                    }


                    while (div == 1) //find a divisor. numerator may be negative because we're simplifying the difference between intended output and current cell fill.
                    {
                        for (int j = 1; j < Math.Abs(tmp) / 2; j++)
                        {
                            if ((Math.Abs(tmp) / j) < (Math.Abs(tmp) / div) && Math.Abs(tmp) % j == 0)
                            {
                                div = j;
                                tmp = tmp / div;
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

                    for (int i = 0; i < Math.Abs(tmp); i++)
                    {
                        if (tmp > 0)
                        {
                            Console.Write("+"); //increment letter
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    output[x] += (tmp * div);

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
                }
                else
                {
                    nonl = true;
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
                if (!nonl)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }

    }
}
