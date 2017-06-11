using System;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public static void MyMusic(params int[] notes)
        {
            while (GlobalInput != ConsoleKey.Escape)
            {
                for(int i = 0; i < notes.Length; i++)
                {
                    Console.Beep(Convert.ToInt16(Convert.ToDouble(440) * Math.Pow(2, Convert.ToDouble(notes[i])/12)),250);
                    if (GlobalInput == ConsoleKey.Escape) break;
                }
            }
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
        private static int score = 0;
        public static void RIDE()
        {
            RIDETitleScreen();
            Character ch = new Character();
            ch.SetPlayableOrEnemy(true);
            int diff=50;
            Console.ForegroundColor = ConsoleColor.Blue;
            string str = "SELECT DIFFICULTY:";
            Console.SetCursorPosition(Screen.GetWidth() / 2-10, Screen.GetHeight() / 2-10);
            Console.Write(str);
            Console.SetCursorPosition(Screen.GetWidth() / 2 - 8+str.Length, Screen.GetHeight() / 2 - 9);
            Console.Write("EASY");

            Console.SetCursorPosition(Screen.GetWidth() / 2 - 8 + str.Length, Screen.GetHeight() / 2 - 8);
            Console.Write("NORMIEE");

            Console.SetCursorPosition(Screen.GetWidth() / 2 - 8 + str.Length, Screen.GetHeight() / 2 - 7);
            Console.Write("GET OUT");
            int margin = Screen.GetWidth() / 2 - 9 + str.Length-1;
            str = ">";
            ConsoleKey ky = new ConsoleKey();
            Console.SetCursorPosition(margin, Screen.GetHeight() / 2 - 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(str);
            Console.SetCursorPosition(margin, Screen.GetHeight() / 2 - 9);
            while (ky != ConsoleKey.Enter && ky != ConsoleKey.Escape)
            {
                ky = Console.ReadKey(true).Key;
                if (ky == ConsoleKey.UpArrow&&Console.CursorTop> Screen.GetHeight() / 2 - 9)
                {
                    Console.Write(" ");
                    Console.CursorLeft=margin;
                    Console.CursorTop--;
                    Console.Write(str);
                    Console.CursorLeft = margin;
                }
                if (ky == ConsoleKey.DownArrow && Console.CursorTop < Screen.GetHeight() / 2 - 7)
                {
                    Console.Write(" ");
                    Console.CursorLeft = margin;
                    Console.CursorTop++;
                    Console.Write(str);
                    Console.CursorLeft = margin;
                }
            }
            int per = 2000, dil = 0, how = 0;
            if(Console.CursorTop== Screen.GetHeight() / 2 - 9) { diff = 60; str = "EASY"; }
            if(Console.CursorTop == Screen.GetHeight() / 2 - 8) { diff = 60; dil = 20; how = 1; str = "NORMIEE"; }
            if (Console.CursorTop == Screen.GetHeight() / 2 - 7) { diff = 50; dil = 10; per = 1000; how = 2; str = "GET OUT"; }
            Console.Clear();
            for (int i=0;i<100;i++)
                {
                    Console.SetCursorPosition(rand.Next(str.Length, Screen.GetWidth()) - str.Length, rand.Next(Screen.GetHeight()));
                    Console.Write(str); Thread.Sleep(10);
                }
            Thread.Sleep(200);
            Console.Clear();
            Console.SetCursorPosition(Screen.GetWidth() / 2- "ARROWS TO MOVE, P TO PAUSE".Length/2, Screen.GetHeight() / 2);
            Console.Write("ARROWS TO MOVE, P TO PAUSE");
            Thread.Sleep(2000);
            Mesh m = new Mesh(ConsoleColor.Blue, new Screen.Point(2, 0), new Screen.Point(1, -1), new Screen.Point(2, -1), new Screen.Point(3, -1), new Screen.Point(2, 0));
            m.MiddlePointMoveTo(new Screen.Point(Screen.GetWidth() / 2, 3));
            m.SetRender(true);
            ch.SetMesh(m);
            Parallel.Invoke(() => { ch.Move1(); }, ()=> { Stage.MoveGoods(diff); },() => { while (GlobalInput != ConsoleKey.Escape) { score = ch.GetScore(); }; },()=> { Stage.Dilation(how, how, per,dil); },() => { Gameplay.Stage.MoveObstacles(diff); }, () => { Gameplay.Stage.MakeObstaclesAndGoods(500,1000); }, () => { Gameplay.Render(); }, () => { Gameplay.MyMusic(0, -5, 0, 3, 0, -5, 0, 3, 0, -4, 0, 5, 7, 5, 2, -1); }, () => { Input(); },()=> { Character.CheckIfDies(); });
        }
        public static void RIDETitleScreen()
        {
            Console.Title = "RIDE";
            Console.SetWindowSize(100, 30);
            Console.SetBufferSize(100, 30);
            Mesh RIDE = new Mesh();
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(20, Screen.GetHeight() - 40), new Screen.Point(20, Screen.GetHeight() - 20), new Screen.Point(25, Screen.GetHeight() - 25), new Screen.Point(20, Screen.GetHeight() - 30), new Screen.Point(25, Screen.GetHeight() - 40)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(30, Screen.GetHeight() - 40), new Screen.Point(30, Screen.GetHeight() - 20)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(35, Screen.GetHeight() - 40), new Screen.Point(35, Screen.GetHeight() - 20), new Screen.Point(40, Screen.GetHeight() - 30), new Screen.Point(35, Screen.GetHeight() - 40)));
            RIDE.MergeWith(new Mesh(ConsoleColor.Blue, new Screen.Point(50, Screen.GetHeight() - 40), new Screen.Point(45, Screen.GetHeight() - 35), new Screen.Point(50, Screen.GetHeight() - 30), new Screen.Point(45, Screen.GetHeight() - 25), new Screen.Point(50, Screen.GetHeight() - 20)));
            RIDE.MiddlePointMoveTo(new Screen.Point(Screen.GetWidth() / 2, Screen.GetHeight()/2));
            string str = "PRESS ENTER TO CONTINUE";
            RIDE.Render();
            ConsoleKey key = new ConsoleKey();
            Console.ForegroundColor = ConsoleColor.Red;
            int[] notes = {6,3,6,10,15,18,17,15,14,10,14,17,22,20,18,17, 6+12, 3+12, 6+12, 10+12, 15+12, 18+12, 17+12, 15+12,17+12,15+12,14+12,12+12,10+12,8+12,6+12,17, 6+12, 3+12, 6+12, 10+12, 15+12, 18+12, 17+12, 15+12, 14+12, 10+12, 14+12, 17+12, 22+12, 20+12, 18+12, 17+12, 6 + 12+12, 3 + 12+12, 6 + 12+12, 10 + 12+12, 15 + 12+12, 18 + 12+12, 17 + 12+12, 15 + 12+12, 17 + 12+12, 15 + 12+12, 14 + 12+12, 12 + 12+12, 10 + 12+12, 8 + 12+12, 6 + 12+12, 17+12 };
            
            Parallel.Invoke( () => { while (key != ConsoleKey.Enter) { key = Console.ReadKey().Key; }; }, () => { while (key != ConsoleKey.Enter) {
                    Console.SetCursorPosition(rand.Next(str.Length, Screen.GetWidth()) - str.Length, rand.Next(Screen.GetHeight()));
                    Console.Write(str); Thread.Sleep(100);
                }; },()=> {
                    while (key != ConsoleKey.Enter)
                    {
                        for (int i = 0; i < notes.Length; i++)
                        {
                            Console.Beep(Convert.ToInt16(Convert.ToDouble(440) * Math.Pow(2, Convert.ToDouble(notes[i]) / 12)), 250);
                            if (key == ConsoleKey.Enter) break;
                        }
                    };
                });
            RIDE.UnRender();
            RIDE.Dispose();
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
            string stats = "";
            while (GlobalInput != ConsoleKey.Escape)
            {
                stats = "PTS:" + score.ToString();
                Console.Clear();
                Console.SetCursorPosition(Screen.GetWidth() - stats.Length - 1, Screen.GetHeight() - 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(stats);
                stats = Character.GetHealthForeach();
                Console.SetCursorPosition(Screen.GetWidth() - stats.Length - 1, 2);
                Console.Write(stats);
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






