using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Mesh mes = new Mesh(new Screen.Point(10, 2), new Screen.Point(10, 7), ConsoleColor.Magenta);
            Gameplay.TitleScreen();
            Gameplay.Stage.MakeBounds();
            Character zuzanka = new Character(mes);
            Mesh mm = new Mesh(new Screen.Point(2, 4), new Screen.Point(20, 30), ConsoleColor.Magenta);
            Console.ReadKey();
            Mesh mms = new Mesh(new Screen.Point(7, 9), new Screen.Point(3, 2), ConsoleColor.Magenta);
            Console.ReadKey();
            ConsoleKey key = new ConsoleKey();
            Parallel.Invoke(() => { Gameplay.Stage.MoveObstacles(50); }, () => { Gameplay.Stage.MakeObstacles(300,100); }, () => { Gameplay.Render(); }, () => { zuzanka.Move(); }, () => { Gameplay.Music(); },()=> { while (key != ConsoleKey.Escape) { key = Console.ReadKey().Key; }; });
            Console.ReadKey();
        }
    }
}






