﻿using System;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleApp2
{

        public partial class Mesh
        {
            public class Animate
            {
                public static void MiddlePointMoveTo(Mesh mes, Screen.Point pt, int moveduration)
                {

                    Screen.Point mat = Mesh.GetMiddlePoint(mes);
                    int dist = pt.DistanceFrom(mat);
                    for (int i = 0; i < dist; i++)
                    {
                        mes.MiddlePointMoveTo(new Screen.Point(Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(mat.GetX()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(pt.GetX())), Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(mat.GetY()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(pt.GetY()))));
                        Thread.Sleep(moveduration);
                    }
                }
                public static void Transform(Mesh mes, Screen.Point pt, int moveduration)
                {
                    int norm = pt.DistanceFrom(new Screen.Point(0, 0));
                    double normx = Convert.ToDouble(pt.GetX()) / Convert.ToDouble(norm);
                    double normy = Convert.ToDouble(pt.GetY()) / Convert.ToDouble(norm);
                    for (int i = 0; i < norm; i++)
                    {
                        foreach (Screen.Point p in mes.points)
                        {
                            p.Transform(Convert.ToInt16(normx * Convert.ToDouble(i)), Convert.ToInt16(normy * Convert.ToDouble(i)));
                        }
                        Thread.Sleep(moveduration);
                    }
                }
            private static Mesh pos = new Mesh();
            public static void KeyMove3(Mesh mes, ConsoleKey Input)
            {
                while (Input != ConsoleKey.Escape)
                {
                    while (Input == ConsoleKey.UpArrow)
                    {
                        mes.Transform(new Screen.Point(0, 1));
                        if (mes.Collides()) { mes.Transform(new Screen.Point(0, -1)); }
                        else { }
                    }
                    while (Input == ConsoleKey.DownArrow)
                    {
                        mes.Transform(new Screen.Point(0, -1));
                        if (mes.Collides()) { mes.Transform(new Screen.Point(0, 1)); }
                        else { }
                    }
                    while (Input == ConsoleKey.LeftArrow)
                    {
                        mes.Transform(new Screen.Point(-1, 0));
                        if (mes.Collides()) { mes.Transform(new Screen.Point(1, 0)); }
                        else { }
                    }
                    while (Input == ConsoleKey.RightArrow)
                    {
                        mes.Transform(new Screen.Point(1, 0));
                        if (mes.Collides()) { mes.Transform(new Screen.Point(-1, 0)); }
                        else { }
                    }

                    while (Input == ConsoleKey.Spacebar)
                    {
                        HashSet<Gameplay.Character> trash = new HashSet<Gameplay.Character>();
                        foreach (Gameplay.Character ch in Gameplay.Character.GetEnemies())
                        {
                            try
                            {
                                foreach (Screen.Point o in ch.GetMesh().GetPoints())
                                {
                                    if (o.GetX() == Mesh.GetMiddlePoint(mes).GetX()) { trash.Add(ch); }
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
            public static void KeyMove(Mesh mes,ConsoleKey Input)
            {
                while (Input != ConsoleKey.Escape) { 
                while (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    mes.Transform(new Screen.Point(0, 1));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(0, -1)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.DownArrow)
                {
                    mes.Transform(new Screen.Point(0, -1));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(0, 1)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    mes.Transform(new Screen.Point(-1, 0));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(1, 0)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    mes.Transform(new Screen.Point(1, 0));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(-1, 0)); }
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
                                if (o.GetX() == Mesh.GetMiddlePoint(mes).GetX()) { trash.Add(ch); }
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
            
            public static void KeyMove2(Mesh mes)
            {
                while (Console.ReadKey().Key == ConsoleKey.UpArrow)
                {
                    mes.Transform(new Screen.Point(0, 1));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(0, -1)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.DownArrow)
                {
                    mes.Transform(new Screen.Point(0, -1));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(0, 1)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.LeftArrow)
                {
                    mes.Transform(new Screen.Point(-1, 0));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(1, 0)); }
                    else { }
                }
                while (Console.ReadKey().Key == ConsoleKey.RightArrow)
                {
                    mes.Transform(new Screen.Point(1, 0));
                    if (mes.Collides()) { mes.Transform(new Screen.Point(-1, 0)); }
                    else { }
                }
                KeyMove2(mes);
            }
        }
        }
    }

