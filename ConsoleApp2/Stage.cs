using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public class Stage
        {

            private static Mesh bounds = new Mesh();
            private static int stagetype = 0;
            public static int GetStageType()
            {
                return stagetype;
            }
            public static void SetStageType(int st)
            {
                stagetype = st;
            }
            private static HashSet<Mesh> goods = new HashSet<Mesh>();
            private static HashSet<Mesh> obstacles = new HashSet<Mesh>();
            public static HashSet<Mesh> GetObstacles()
            {
                return obstacles;
            }
            public static void MakePlatforms(int quantity)
            {
                int pos = 1;
                int height = Screen.GetHeight()/2;
                int width = 19;
                new Mesh(true, true, ConsoleColor.DarkRed, new Screen.Point(pos, 1), new Screen.Point(pos, height), new Screen.Point(width, height), new Screen.Point(width, 0));
                pos += rand.Next(10, 30);
                while (quantity>0) {
                    pos += rand.Next(10, 30);
                    height += rand.Next(-10, 9);
                    while (height < 1)
                    {
                        height += rand.Next(10);
                    }
                    new Mesh(true, true, ConsoleColor.DarkRed, new Screen.Point(pos, 1), new Screen.Point(pos,height), new Screen.Point(pos+width, height), new Screen.Point(pos + width, 0));
                    quantity--;
                        }
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
                    if (GlobalInput == ConsoleKey.Escape) break;
                }
            }
            public static void MakeObstacle()
            {
                obstacles.Add(new Mesh(true,true,ConsoleColor.DarkRed,new Screen.Point(rand.Next(0,Screen.GetWidth()),Screen.GetHeight()),new Screen.Point(rand.Next(0,Screen.GetWidth()),Screen.GetHeight())));
            }
            public static void MakeGoods()
            {
                goods.Add(new Mesh(true, false, ConsoleColor.Green, new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight()), new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight())));

            }
            public static void MusicObstacle()
            {
                while (GlobalInput != ConsoleKey.Escape)
                {
                    Console.Beep(rand.Next(147, 698), rand.Next(1000, 2000));
                    obstacles.Add(new Mesh(true, true, ConsoleColor.DarkRed, new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight()), new Screen.Point(rand.Next(0, Screen.GetWidth()), Screen.GetHeight())));
                }
            }
            static int obsmoveduration;
            static int goodsmoveduration;
            public static void MoveObstacles(int moveduration)
            {
                obsmoveduration = moveduration;
                 HashSet<Mesh> trash = new HashSet<Mesh>();
             HashSet<Character> trashch = new HashSet<Character>();
                while (GlobalInput!=ConsoleKey.Escape) {
                    while (GlobalInput == ConsoleKey.P)
                    {

                    }
                    try
                    {
                        foreach (Mesh mes in obstacles)
                        {
                            mes.Transform(0, -1);
                            try
                            {
                                foreach (Character ch in Character.GetPlayables())
                                {
                                    if (mes.CollidesWith(ch.GetMesh())) { trashch.Add(ch); trash.Add(mes); ch.HealthDecrease(1); }
                                }
                            }
                            catch
                            {

                            }
                            if (Mesh.GetMiddlePoint(mes).GetY() < 0)
                            {
                                trash.Add(mes);
                                Character.AddScoreForeach(1);
                            }
                        }
                    }
                    catch { }
                foreach (Mesh mes in trash)
                {
                    try
                    {
                        obstacles.Remove(mes);
                        mes.Dispose();
                    }
                    catch { }
                }
                trash.Clear();
                Thread.Sleep(obsmoveduration);
            }
            }
            public static void MoveGoods(int moveduration)
            {
                goodsmoveduration = moveduration;
                HashSet<Mesh> trashg = new HashSet<Mesh>();
                while (GlobalInput != ConsoleKey.Escape)
                {
                    while (GlobalInput == ConsoleKey.P)
                    {

                    }
                    try
                    {
                        foreach (Mesh mes in goods)
                        {
                            mes.Transform(0, -1);
                            try
                            {
                                foreach (Character ch in Character.GetPlayables())
                                {
                                    if (mes.CollidesWith(ch.GetMesh())) { trashg.Add(mes); Character.AddScoreForeach(50); }
                                }
                            }
                            catch
                            {

                            }
                            if (Mesh.GetMiddlePoint(mes).GetY() < 0)
                            {
                                trashg.Add(mes);
                            }
                        }
                    }
                    catch { }
                    foreach (Mesh mes in trashg)
                    {
                        try
                        {
                            goods.Remove(mes);
                            mes.Dispose();
                        }
                        catch { }
                    }
                    trashg.Clear();
                    Thread.Sleep(goodsmoveduration);
                }
            }
             public static void Dilation(int obs,int goods,int period, int min)
            {
                while (GlobalInput != ConsoleKey.Escape)
                {
                    while (GlobalInput == ConsoleKey.P)
                    {

                    }
                    Thread.Sleep(period);
                    if (goodsmoveduration > min)
                    {
                        goodsmoveduration -= goods;
                    }
                    if (obsmoveduration > min)
                    {
                        obsmoveduration -= obs;
                    }
                }
            }
            public static void MakeObstaclesAndGoods(int period,int quantity, bool inf)
            {
                Thread.Sleep(3000);
                int r;
                if (inf) {
                    while (GlobalInput != ConsoleKey.Escape)
                    {
                        while (GlobalInput == ConsoleKey.P)
                        {

                        }
                        stagetype = 0;
                        r = rand.Next(0, Screen.GetWidth());
                        if (rand.Next(0, 9) == 4)
                        {
                            goods.Add(new Mesh(true, false, ConsoleColor.Green, new Screen.Point(r, Screen.GetHeight()), new Screen.Point(r + 5, Screen.GetHeight())));
                        }
                        else
                        {
                            obstacles.Add(new Mesh(true, false, ConsoleColor.DarkRed, new Screen.Point(r, Screen.GetHeight()), new Screen.Point(r + rand.Next(4, 20), Screen.GetHeight())));
                        }
                        Thread.Sleep(period);
                    }
                }
                else {
                    while (quantity > 0 && GlobalInput != ConsoleKey.Escape)
                    {
                        while (GlobalInput == ConsoleKey.P)
                        {

                        }
                        stagetype = 0;
                        r = rand.Next(0, Screen.GetWidth());
                        if (rand.Next(0, 9) == 4)
                        {
                            goods.Add(new Mesh(true, false, ConsoleColor.Green, new Screen.Point(r, Screen.GetHeight()), new Screen.Point(r + 5, Screen.GetHeight())));
                        }
                        else
                        {
                            obstacles.Add(new Mesh(true, false, ConsoleColor.DarkRed, new Screen.Point(r, Screen.GetHeight()), new Screen.Point(r + rand.Next(4, 20), Screen.GetHeight())));
                        }
                        quantity--;
                        Thread.Sleep(period);
                    }
                   /* if (quantity< 1)
                    {
                        stagetype = 1;
                        while (stagetype == 1)
                        {
                            Character.MakeBoss(stagetype);
                        }
                    }*/
                }
            }
        }

    }
}






