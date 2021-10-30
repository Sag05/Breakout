using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Breakout
{
    class MyProgram
    {
        public void Run()
        {
            int xCord = 0;
            int yCord = 0;
            int xDirection = 1;
            int yDirection = 1;
            bool running = true;
            //bool left = true;
            //bool up = false;


            while (running == true)
            {
                #region Ifloops
                /*if (left == true)
                {
                    xCord++;
                    //if (xCord > 0)
                    //{
                    Console.SetCursorPosition(xCord - 1, yCord);
                    Console.Write(" ");//}

                    if (up == false)
                    {
                        yCord++;
                        Console.SetCursorPosition(xCord, yCord);

                        if (yCord == 10)
                        {
                            up = true;
                        }
                    }

                    else
                    {
                        yCord--;
                        Console.SetCursorPosition(xCord, yCord);
                        
                        if (yCord == 0)
                        {
                            up = false;
                        }
                    }
                    Console.Write("@");
                    Thread.Sleep(100);


                    if (xCord == 20)
                    {
                        left = false;
                    }
                }
                
                if (left == false)
                {
                    xCord--;
                    //if (xCord > 0)
                    //{
                    Console.SetCursorPosition(xCord + 1, yCord);
                    Console.Write(" ");
                    //}

                    Console.SetCursorPosition(xCord, yCord);
                    Console.Write("@");
                    Thread.Sleep(100);

                    if (xCord == 1)
                    {
                        left = true;
                    }
                }*/
                #endregion

                Console.SetCursorPosition(xCord, yCord);


            }
        }
    }
}