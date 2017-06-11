using System.Collections.Generic;
using System;
using System.Threading;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public class Character : IDisposable
        {
            protected virtual void Dispose(bool ti)
            {
            }
            public void Dispose()
            {
                try { enemies.Remove(this); } catch { }
                try { playables.Remove(this); } catch { }
                try { this.mesh.SetRender(false); } catch { }
                mesh.Dispose();
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            int score;
            public static HashSet<Character> GetEnemies()
            {
                return enemies;
            }
            public void AddScore(int pt)
            {
                score += pt;
            }
            public static void CheckIfDies()
            {
                HashSet<Character> trash = new HashSet<Character>();
                while (GlobalInput != ConsoleKey.Escape)
                {
                    trash.Clear();
                    try
                    {
                        foreach (Character ch in playables)
                        {
                            if (ch.health < 1)
                            {
                                trash.Add(ch);
                            }
                        }
                    }
                    catch { }
                    try
                    {
                        foreach (Character ch in enemies)
                        {
                            if (ch.health < 1)
                            {
                                trash.Add(ch);
                            }
                        }
                    }
                    catch { }
                    foreach(Character ch in trash)
                    {
                        ch.Dispose();
                    }
                }
            }
            /*public static void MakeBoss(int st)
            {
                HashSet<Screen.Point> stings = new HashSet<Screen.Point>();
                for(int i = 0; i < st; i++)
                {
                    stings.Add(new Screen.Point(rand.Next(0, st), 0));
                }
                for(int i = 0; i < st+4; i++)
                {
                    stings.Add(new Screen.Point(rand.Next(st / 2 - st - 4, st / 2 + st + 4), rand.Next(0, st + 4)));
                }
                Mesh mes = new Mesh(true,false,ConsoleColor.Blue, stings);
                Character ch = new Character(mes);
                ch.mesh.MiddlePointMoveTo(new Screen.Point(Screen.GetWidth() / 2, Screen.GetHeight() - (st + 4)));
                while (Gameplay.Stage.GetStageType() > 0&&GlobalInput!=ConsoleKey.Escape)
                {
                    EnemyTrace();
                    Thread.Sleep(2000 / st);
                }
            }*/
            public static void EnemyTrace()
            {
                
                    foreach(Character plb in playables)
                    {
                    foreach (Character ch in enemies)
                    {
                        plb.mesh.Transform(0, (Mesh.GetMiddlePoint(plb.mesh).GetY() - Mesh.GetMiddlePoint(ch.mesh).GetY()) / Math.Abs(Mesh.GetMiddlePoint(plb.mesh).GetY() - Mesh.GetMiddlePoint(ch.mesh).GetY()));
                    }

                    }
            }
            public int GetHealth()
            {
                return health;
            } 
            public static string GetHealthForeach()
            {
                string str;
                int i;
                while (GlobalInput != ConsoleKey.Escape)
                {
                    i = 0;
                    str = "";
                    foreach (Character ch in playables)
                    {
                        i++;
                        str += "HP" + i.ToString() + ":" + ch.health.ToString() + " ";
                    }
                    return str;
                }
                return "";
            }
            public void HealthDecrease(int dec)
            {
                health -= dec;
            }
            public static HashSet<Character> GetPlayables()
            {
                return playables;
            }
            public Character(int x, int y, ConsoleColor col, bool plbl)
            {
                mesh = new Mesh();
                mesh.AddPoint(new Screen.Point(x, y, col));
                this.SetPlayableOrEnemy(plbl);
                this.mesh.SetRender(true);
            }
            ~Character()
            {
                try { enemies.Remove(this); } catch { }
                try { playables.Remove(this); } catch { }

            }
            public static void EnemyAttack(int moveduration)
            {
                HashSet<Character> plb = new HashSet<Character>();
                foreach (Character mes in playables)
                {
                    foreach (Character ms in enemies)
                    {
                        if (mes.mesh.CollidesWith(ms.mesh)) { plb.Add(mes); }
                        ms.mesh.Transform(Screen.Point.NormalizedVector(Mesh.GetMiddlePoint(ms.mesh), Mesh.GetMiddlePoint(mes.mesh)));
                    }
                }
                foreach (Character ch in plb) { ch.SetPlayableOrEnemy(false); }
                Thread.Sleep(moveduration);
                EnemyAttack(moveduration);
            }
            private static HashSet<Character> enemies = new HashSet<Character>();
            private static HashSet<Character> playables = new HashSet<Character>();
            Mesh mesh;
            int health;
            bool enemy;
            bool playable;
            public static void AddScoreForeach(int sc)
            {
                foreach(Character ch in playables)
                {
                    ch.score += sc;
                }
            }
            public int GetScore()
            {
                return score;
            }
            public void SetPlayableOrEnemy(bool ch)
            {
                playable = ch;
                enemy = !ch;
                if (ch)
                {
                    playables.Add(this);
                    this.health = 3;
                    enemies.Remove(this);
                }
                else
                {
                    enemies.Add(this);
                    playables.Remove(this);
                }
                this.mesh.Render();
            }
            public void SetMesh(Mesh mes)
            {
                mesh = mes;
            }
            public Character()
            {
                mesh = new Mesh();
            }
            public Character(ConsoleColor col, Screen.Point pt)
            {
                mesh = new Mesh(ConsoleColor.Yellow, new Screen.Point(2, 0), new Screen.Point(1, 1), new Screen.Point(2, 1), new Screen.Point(3, 1), new Screen.Point(2, 0));
                mesh.MiddlePointMoveTo(pt);
                mesh.SetRender(true);
                playable = false;
                enemy = true;
                enemies.Add(this);
            }
            public Character(Mesh mes)
            {
                mesh = mes;
                mesh.SetRender(true);
                playable = true;
                enemy = false;
                playables.Add(this);
            }

            public Mesh GetMesh()
            {
                return mesh;
            }
            public void Move1()
            {
                while (GlobalInput != ConsoleKey.Escape)
                {
                    while (GlobalInput == ConsoleKey.P)
                    {

                    }
                    if (this.playable)
                    {
                        while (GlobalInput == ConsoleKey.LeftArrow)
                        {
                            mesh.Transform(-1, 0);
                            if (Mesh.OutOfWindow(mesh)) { mesh.Transform(1, 0); }
                            else { }
                            Thread.Sleep(20);
                        }
                        while (GlobalInput == ConsoleKey.RightArrow)
                        {
                            mesh.Transform(1, 0);
                            if (Mesh.OutOfWindow(mesh)) { mesh.Transform(-1, 0); }
                            else { }
                            Thread.Sleep(20);
                        }
                    }
                }
            }
            public void Move()
            {
                while (GlobalInput != ConsoleKey.Escape)
                {
                    if (this.playable)
                    {
                            while (Console.ReadKey().Key == ConsoleKey.UpArrow)
                            {
                                mesh.Transform(new Screen.Point(0, 1));
                                if (Mesh.OutOfWindow(mesh)) { mesh.Transform(0, -1); }
                                else { }
                            }
                            while (Console.ReadKey().Key == ConsoleKey.DownArrow)
                            {
                                mesh.Transform(new Screen.Point(0, -1));
                                if (Mesh.OutOfWindow(mesh)) { mesh.Transform(0, 1); }
                                else { }
                            }
                            while (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                            {
                                mesh.Transform(new Screen.Point(-1, 0));
                                if (Mesh.OutOfWindow(mesh)) { mesh.Transform(new Screen.Point(1, 0)); }
                                else { }
                            }
                            while (Console.ReadKey().Key == ConsoleKey.RightArrow)
                            {
                                mesh.Transform(new Screen.Point(1, 0));
                                if (Mesh.OutOfWindow(mesh)) { mesh.Transform(new Screen.Point(-1, 0)); }
                                else { }
                            }

                            while (Console.ReadKey().Key == ConsoleKey.Spacebar)
                            {
                                HashSet<Gameplay.Character> trash = new HashSet<Gameplay.Character>();
                                foreach (Gameplay.Character ch in Gameplay.Character.GetEnemies())
                                {
                                    try
                                    {
                                        foreach (Screen.Point o in ch.GetMesh().GetPoints())
                                        {
                                            if (o.GetX() == Mesh.GetMiddlePoint(mesh).GetX()) { trash.Add(ch); }
                                        }
                                    }
                                    catch { }
                                }
                                foreach (Gameplay.Character ch in trash)
                                {
                                    ch.GetMesh().SetRender(false);
                                    ch.Dispose();
                                }
                            
                        }
                    }
                }
            }
        }
    }
}