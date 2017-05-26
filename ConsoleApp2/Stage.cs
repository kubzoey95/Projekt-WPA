using System;

namespace ConsoleApp2
{
    public partial class Gameplay
    {
        public class Stage
        {
            private static Mesh bounds = new Mesh();
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
        }
    }
}






