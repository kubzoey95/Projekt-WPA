using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public class Stage
        {

            private static Mesh bounds = new Mesh();
            private static HashSet<Mesh> obstacles = new HashSet<Mesh>();
            public static HashSet<Mesh> GetObstacles()
            {
                return obstacles;
            }
            public static void MakeBounds()
            {
                bounds.MergeWith(new Mesh(new Screen.Point(0, 0), new Screen.Point(0, Console.WindowHeight), ConsoleColor.Black));
                bounds.MergeWith(new Mesh(new Screen.Point(Console.WindowWidth, 0), new Screen.Point(Console.WindowWidth, Console.WindowHeight), ConsoleColor.Black));
                bounds.SetCollider(true);
            }
            public static void MakeEnemies(int number)
            {

                for (int i = 0; i < number; i++)
                {
                    new Character(ConsoleColor.Yellow,new Screen.Point(rand.Next(Screen.GetWidth()),rand.Next(20,Screen.GetWidth()))) ;
                }
            }
            private static Random rand;
            public static void MakeObstacle()
            {
                obstacles.Add(new Mesh(true,true,ConsoleColor.DarkRed,new Screen.Point(rand.Next(0,Screen.GetWidth()),Screen.GetHeight()),new Screen.Point(rand.Next(0,Screen.GetWidth()),Screen.GetHeight())));
            }
            private static HashSet<Mesh> trash = new HashSet<Mesh>();
            public static void MoveObstacles(int moveduration)
            {

                foreach(Mesh mes in obstacles)
                {
                    mes.Transform(0, -1);
                    if (Mesh.GetMiddlePoint(mes).GetY() < 0)
                    {
                        trash.Add(mes);
                    }
                }
                foreach(Mesh mes in trash)
                {
                    try
                    {
                        obstacles.Remove(mes);
                        mes.Dispose();
                    }
                    catch { }
                }
                trash.Clear();
            }
            public static void MakeObstacles(int period,int quantity)
            {
                while (quantity > 0)
                {
                    obstacles.Add(new Mesh(true, true, ConsoleColor.DarkRed, new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight()), new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight())));
                    quantity--;
                    Thread.Sleep(period);
                }
            }
        }

    }
}






