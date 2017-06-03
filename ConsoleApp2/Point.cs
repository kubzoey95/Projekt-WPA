using System;
using System.Collections.Generic;
    

namespace ConsoleApp2
{
    public class Screen
    {
        private static int height = Console.BufferHeight;
        private static int width = Console.BufferWidth;
        private static int refx = 0;
        private static int refy = 0;
        public static void SetRef(int x,int y)
        {
            refx = x;
            refy = y;
        }
        public static int GetWidth() { return width; }
        public static int GetHeight() { return height; }
        public class Point:IDisposable
        {
            private int x, y;
            private System.ConsoleColor color;
            public bool IsEqualWith(Screen.Point p) { if (this.x == p.x && this.y == p.y) return true; return false; }

            public Point(int x1,int y1)
            {
                x = x1;
                y = y1;
                color = System.ConsoleColor.White;
            }
            public Point()
            {
                x = 0;
                y = 0;
                color = System.ConsoleColor.White;
            }
            public void SetX(int u) { x = u; }
            public int GetX() { return x; }
            public void SetY(int u) { y = u; }
            public int GetY() { return y; }
            public System.ConsoleColor GetColor()
            {
                return color;
            }
            public void SetColor(System.ConsoleColor col)
            {
                color = col;
            }
            public Point(int x1, int y1,System.ConsoleColor col)
            {
                x = x1;
                y = y1;
                color = col;
            }
            ~Point()
            {

            }
            protected virtual void Dispose(bool ki)
            {

            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                
            }
            public void Transform(Point pt)
            {
                x += pt.GetX();
                y += pt.GetY();
            }
            public void Transform(int x1, int y1)
            {
                x += x1;
                y += y1;
            }
            public void Render()
            {
                if (x>0 && height > y && x<width && y > 0)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(x, height - y);
                    Console.Write("o");
                }
            }
            public void Render(char ch)
            {
                if (x > refx && height+refy  > y && x < width+refx && y > refy)
                {
                    Console.ForegroundColor = color;
                    Console.SetCursorPosition(x, height - y);
                    Console.Write(ch);
                }
            }
            public void UnRender()
            {
                if (x > refx && height + refy > y && x < width + refx && y > refy)
                {
                    Console.SetCursorPosition(x, height - y);
                    Console.Write(" ");
                }
            }
            public int DistanceFrom(Point p)
            {
                return Convert.ToInt16(System.Math.Pow(System.Math.Pow(Convert.ToDouble(p.GetX() - x), 2) + System.Math.Pow(Convert.ToDouble(p.GetY() - y), 2), 0.5));
            }
            public static Point NormalizedVector(Point pt, Point p)
            {
                Point poin = new Point();
                try
                {
                    if (pt.DistanceFrom(p) != 0)
                    {
                        poin.SetX(Convert.ToInt16(Convert.ToDouble((p.GetX() - pt.GetX())) / Convert.ToDouble(pt.DistanceFrom(p))));
                        poin.SetY(Convert.ToInt16(Convert.ToDouble((p.GetY() - pt.GetY())) / Convert.ToDouble(pt.DistanceFrom(p))));
                        return poin;
                    }

                    else { return new Point(0, 0); }
                }
                catch { return new Point(0, 0); }

                }
        }
    }
}