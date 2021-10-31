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


        //struct for brick array
        struct Bricks
        {
            public bool active;
            public int xPos;
            public int yPos;
        }

        struct Point
        {
           public int x, y;
            public Point(int mx, int my)
            {
                x = mx;
                y = my;
            }
            public Point(Point p)
            {
                x = p.x;
                y = p.y;
            }
        }

        struct Ball
        {
            public Point position, velocity;
            public Ball(Point p, Point v)
            {
                position = p;
                velocity = v;
            }
        }

        struct Platform
        {
            public int direction;
            public Point position;

            public Platform(int dir, Point pos)
            {
                direction = dir;
                position = pos;
            }
        }
        public void Run()
        {

            #region Instructions
            /* TODO
            Variabler
            x-y position
            directions
            vÃ¤ggar + tak
            (loop)
	        uppdatera position boll/platta
	        paus
        	kollision med bricks
            */
            #endregion


            #region Variables
            //walls/roof
            int roofCord = 0;
            int floorCord = 15;
            int leftWall = 0;
            int rightWall = 31;

            //platform
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            ConsoleKeyInfo oldInput;
            Platform platform = new Platform(1, new Point(1, floorCord)); 

            //ball Cordinates
            Point spawn = new Point(platform.position.x, floorCord - 2);
            Ball ball = new Ball(spawn, new Point(1, 1));
            Point prevPosition = new Point(ball.position);


            //Other
            int speedChanger = 0;
            int lives = 3;
            int score = 0;
            int sleepTime = 300;
            bool running = true;

            //Brick array
            int brickAmmount = 50;
            Bricks[] myBricks = new Bricks[brickAmmount];
            //set all values to 0
            for (int i = 0; i < myBricks.Length; i++)
            {
                myBricks[i].active = true;
                myBricks[i].xPos = 0;
                myBricks[i].yPos = 0;
            }
            #endregion


            void Speed()
            {

                #region Sleep
                if (sleepTime > 200)
                {
                    sleepTime -= 10;
                }
                else if (sleepTime > 100)
                {
                    sleepTime -= 5;
                }
                else if (sleepTime > 30)
                {
                    sleepTime -= 2;
                }
                else if (sleepTime > 10)
                {
                    speedChanger++;
                    if (speedChanger >= 10)
                    {
                        sleepTime -= 1;
                        speedChanger = 0;
                    }
                }
                else if (sleepTime > 5)
                {
                    speedChanger++;
                    if (speedChanger >= 25)
                    {
                        sleepTime -= 1;
                        speedChanger = 0;
                    }
                }
                #endregion


                #region Print
                score++;
                Console.SetCursorPosition(0, floorCord + 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Score:" + score);
                Console.WriteLine("Lives:" + lives);
                #endregion
            }


            #region Walls
            Console.ForegroundColor = ConsoleColor.Yellow;
            //roof
            for (int i = 0; i <= rightWall; i++)
            {
                Console.SetCursorPosition(i, roofCord);
                Console.Write("@");
            }
            //walls
            for (int i = 1; i < floorCord; i++)
            {
                Console.SetCursorPosition(leftWall, i);
                Console.Write("@");
                Console.SetCursorPosition(rightWall, i);
                Console.Write("@");
            }
            #endregion


            #region Bricks
            //Set brick position
            int brickNumber;
            for (int y = 0; y < Math.Ceiling((double)myBricks.Length / (rightWall / 2)); y++)
            {
                brickNumber = y * ((rightWall) / 2);

                for (int x = 0; x < (rightWall)  / 2; x++)
                {   
                    if (brickNumber < myBricks.Length)
                    {
                        myBricks[brickNumber].yPos = y + 2;
                        myBricks[brickNumber].xPos = 1 + x * 2;
                        brickNumber++;
                    }
                }
            }


            //Write Bricks
            for (int i = 0; i < myBricks.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(myBricks[i].xPos, myBricks[i].yPos);
                Console.Write("[]");
            }
            #endregion


            #region Misc
            Console.CursorVisible = false;
            Thread.Sleep(sleepTime);

            //Write score/lives
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, floorCord + 2);
            Console.WriteLine("Score:" + score);
            Console.WriteLine("Lives:" + lives);

            ball.position.x = spawn.x;
            ball.position.y = spawn.y;
            #endregion


            while (running == true)
            {

                #region Platfrom 
                // read input
                while (Console.KeyAvailable)
                {
                    //oldInput = input;
                    input = Console.ReadKey(true);

                    //Platform Direction
                    if (input.Key == ConsoleKey.LeftArrow && platform.direction != -1)
                    {
                        platform.direction = -1;
                    }
                    else if (input.Key == ConsoleKey.RightArrow && platform.direction != 1)
                    {
                        platform.direction = 1;
                    }
                }

                //stops the platform from going outside of the play area
                if (platform.position.x >= rightWall - 2)
                {
                    Console.SetCursorPosition(rightWall, floorCord);
                    Console.Write(" ");
                    platform.position.x--;
                }
                else if (platform.position.x <= leftWall)
                {
                    platform.position.x++;
                    Console.SetCursorPosition(leftWall, floorCord);
                    Console.Write(" ");
                }

                #region Print
                //remove old platform
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(platform.position.x, floorCord);
                Console.Write("   ");

                //print new platform
                platform.position.x += platform.direction;
                Console.SetCursorPosition(platform.position.x, floorCord);
                Console.Write("===");
                #endregion
                #endregion


                #region MoveBall
                ball.position.x += ball.velocity.x;
                ball.position.y += ball.velocity.y;

                // clear old ball
                Console.SetCursorPosition(prevPosition.x, prevPosition.y);
                Console.Write(" ");


                //pring new ball
                Console.SetCursorPosition(ball.position.x, ball.position.y);
                Console.Write("@");


                Thread.Sleep(sleepTime);

                prevPosition.x = ball.position.x;
                prevPosition.y = ball.position.y;
                #endregion


                #region Change ball.velocity.x
                if (ball.position.x <= leftWall + 1 || ball.position.x >= rightWall - 1)
                {
                    ball.velocity.x *= -1;
                    Speed();
                }
                #endregion


                #region Change ball.velocity.y
                if (ball.position.y <= roofCord + 1)
                {
                    ball.velocity.y *= -1;
                    Speed();
                }

                if (ball.position.y == floorCord - 1 && ball.position.x == platform.position.x || 
                    ball.position.y == floorCord - 1 && ball.position.x == platform.position.x + 1 || 
                    ball.position.y == floorCord - 1 && ball.position.x == platform.position.x + 2)
                {
                    ball.velocity.y *= -1;
                    Speed();
                }
                #endregion


                #region Brick Collision
                for (int i = 0; i < myBricks.Length; i++)
                {
                    if (myBricks[i].active)
                    {
                        if ((ball.position.x == myBricks[i].xPos || ball.position.x == (myBricks[i].xPos + 1)) && 
                            (ball.position.y == (myBricks[i].yPos - ball.velocity.y) || ball.position.y == myBricks[i].yPos))
                        {
                            //Change Direction
                            ball.velocity.y *= -1;

                            //Clear brick
                            Console.SetCursorPosition(myBricks[i].xPos, myBricks[i].yPos);
                            Console.Write("  ");

                            //Deactivate Brick
                            myBricks[i].active = false;

                            //Change Speed
                            Speed();

                            //Add more score
                            score++;
                        }
                    }
                
                    /*else if (ball.position.y == myBricks[i].yPos +1 && ball.position.x == myBricks[i].xPos - ball.position.x && myBricks[i].active == true || ball.position.y == myBricks[i].yPos - 1 && ball.position.x == myBricks[i].xPos - ball.position.x && myBricks[i].active == true)
                    {
                        //Change Direction
                        ball.position.x *= -1;

                        //Clear brick
                        Console.SetCursorPosition(myBricks[i].xPos, myBricks[i].yPos);
                        Console.Write("  ");

                        //Deactivate Brick
                        myBricks[i].active = false;

                        //Change Speed
                        Speed();

                        //Add more score
                        score++;
                    }*/


                }
                #endregion


                #region Lives
                if (ball.position.y > floorCord)
                {
                    lives--;
                    ball.position.x = spawn.x;
                    ball.position.y = spawn.y;
                    ball.velocity.x = 1;
                    ball.velocity.y = - 1;
                    Console.SetCursorPosition(0, floorCord + 3);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Lives:" + lives);

                    if (lives == 0)
                    {
                        running = false;
                    }
                }
                #endregion

            }


            #region GameOver
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(5, 4);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("Score: " + score);
            Console.SetCursorPosition(0, 10);
            Console.ReadKey();
            #endregion


        }
    }
}
