using System;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public static void Bach()
        {
            
        }
        private static ConsoleKey GlobalInput = new ConsoleKey();
        public static void Input()
        {
            while (GlobalInput != ConsoleKey.Escape)
            {
                GlobalInput = Console.ReadKey().Key;
            }
        }
        private static Random rand = new Random();
        public static void Game1(int diff, Character ch)
        {
            Parallel.Invoke(() => { Gameplay.Stage.MoveObstacles(diff); }, () => { Gameplay.Stage.MusicObstacle(); }, () => { Gameplay.Render(); }, () => { ch.Move(); }, () => { Gameplay.Music(); }, () => { Input(); });
        }
        public static void TitleScreen()
        {
            Console.Title = "RIDE";
            Console.SetWindowSize(100, 30);
            Console.SetBufferSize(100, 30);
            Mesh RIDE = new Mesh();
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(20, Screen.GetHeight() - 40), new Screen.Point(20, Screen.GetHeight() - 20), new Screen.Point(25, Screen.GetHeight() - 25), new Screen.Point(20, Screen.GetHeight() - 30), new Screen.Point(25, Screen.GetHeight() - 40)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(30, Screen.GetHeight() - 40), new Screen.Point(30, Screen.GetHeight() - 20)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(35, Screen.GetHeight() - 40), new Screen.Point(35, Screen.GetHeight() - 20), new Screen.Point(40, Screen.GetHeight() - 30), new Screen.Point(35, Screen.GetHeight() - 40)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(50, Screen.GetHeight() - 40), new Screen.Point(45, Screen.GetHeight() - 35), new Screen.Point(50, Screen.GetHeight() - 30), new Screen.Point(45, Screen.GetHeight() - 25), new Screen.Point(50, Screen.GetHeight() - 20)));
            RIDE.MiddlePointMoveTo(new Screen.Point(Screen.GetWidth() / 2, Screen.GetHeight()-20));
            RIDE.Transform(-10, 5);
            string str = "PRESS ENTER TO CONTINUE";
            RIDE.Render();
            ConsoleKey key = new ConsoleKey();
            Console.ForegroundColor = ConsoleColor.Red;
            Parallel.Invoke(() => { while (key != ConsoleKey.Enter) {Console.Beep(rand.Next(147, 698), rand.Next(147, 698));}}, () => { while (key != ConsoleKey.Enter) { key = Console.ReadKey().Key; }; }, () => { while (key != ConsoleKey.Enter) {
                    Console.SetCursorPosition(rand.Next(str.Length, Console.BufferWidth) - str.Length, rand.Next( Console.BufferHeight));
                    Console.Write(str); Thread.Sleep(100);
                }; });
            RIDE.UnRender();
            RIDE = null;
            Console.Clear();
        }
        public static void Music()
        {
            while (GlobalInput != ConsoleKey.Escape)
            {
                Console.Beep(rand.Next(147, 698), rand.Next(147, 698));
            }
        }
        public static void Render()
        {
            while (GlobalInput != ConsoleKey.Escape)
            {
                Console.Clear();
                try
                {
                    foreach (Mesh mes in Mesh.GetRender())
                    {
                        mes.Render();
                    }
                }
                catch { }
                Thread.Sleep(20);
            }
        }
    }
}






