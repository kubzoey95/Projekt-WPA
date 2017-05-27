using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
        public partial class Mesh:IDisposable
        {
            private HashSet<Screen.Point> points;
            private bool collider;
            private bool rendered;
        private static HashSet<Mesh> render = new HashSet<Mesh>();
        private static HashSet<Screen.Point> renderpoints = new HashSet<Screen.Point>();
        private static HashSet<Mesh> colliders= new HashSet<Mesh>();
        public static HashSet<Mesh> GetRender()
        {
            return render;
        }
        public static HashSet<Screen.Point> GetRenderPoints()
        {
            return renderpoints;
        }
        public bool Collides()
        {
                foreach (Mesh msh in colliders)
                {
                if (this.CollidesWith(msh)) return true;
                }
            return false;
        }
        public bool CollidesWith(Mesh mes)
        {
            try
            {
                HashSet<Screen.Point> pts = mes.GetPoints();
                foreach (Screen.Point p in GetPoints())
                {
                    foreach (Screen.Point pt in pts)
                    {
                        if (p.IsEqualWith(pt)) return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        ~Mesh()
        {
            try { render.Remove(this); } catch { }
            try { colliders.Remove(this); } catch { }

        }
        protected virtual void Dispose(bool disp)
        {
            
        }
        public void Dispose()
        {
            try { render.Remove(this); } catch { }
            try { colliders.Remove(this); } catch { }
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private static HashSet<Screen.Point> Joincolls()
        {
            HashSet<Screen.Point> pts = new HashSet<Screen.Point>();
            foreach (Mesh mes in colliders)
            {
                pts.UnionWith(mes.points);
            }
            return pts;
        }
        public void SetCollider(bool cl) {
            collider = cl;
            if (cl)
            {
                colliders.Add(this);
            }
        }
        public Mesh(HashSet<Screen.Point> pts, bool ren)
        {
            points = pts;
            collider = false;
            rendered = ren;
            if(ren)
            render.Add(this);
        }
            public Mesh()
            {
                points = new HashSet<Screen.Point>();
                collider = false;
                rendered = false;
            }
            public Mesh(HashSet<Screen.Point> pts)
            {
                points = pts;
                collider = false;
                rendered = false;
            }
            public Mesh GetMesh() { return this; }
            public HashSet<Screen.Point> GetPoints() { return points; }
            public void SetPoints(HashSet<Screen.Point> pts) { points = pts; }
            public bool GetCollider() { return collider; }
            public bool GetRendered() { return rendered; }
        public Mesh(bool rend, bool coll, ConsoleColor col, Screen.Point Start, Screen.Point End)
        {
            HashSet<Screen.Point> pts = new HashSet<Screen.Point>();
            double dist = Start.DistanceFrom(End);
            for (int i = 0; i < dist; i++)
            {
                pts.Add(new Screen.Point(Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(Start.GetX()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(End.GetX())), Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(Start.GetY()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(End.GetY())), col));
            }
            points = pts;
            collider = coll;
            rendered = rend;
            colliders.Add(this);
            render.Add(this);
        }
            public void AddPoint(Screen.Point p)
            {
                points.Add(p);
            }
            public void AddPoint(int x, int y, System.ConsoleColor col)
            {
                points.Add(new Screen.Point(x, y, col));
            }
            public void Transform(int x, int y)
            {
                foreach (Screen.Point p in points)
                {
                    p.Transform(x, y);
                }
            }
            public void Transform(Screen.Point pt)
            {
                foreach (Screen.Point p in points)
                {
                    p.Transform(pt);
                }
            }
            public void Render()
            {
                foreach (Screen.Point p in points)
                {
                    p.Render();
                }
            }
        public void SetRender(bool st)
        {
            if (st)
            {
                rendered = true;
                render.Add(this);
            }
            else
            {
                rendered = false;
                render.Remove(this);
            }
        }
        public static void Remove(Mesh mes)
        {
            mes = null;
        }
            public void Render(char ch)
            {
                foreach (Screen.Point p in points)
                {
                    p.Render(ch);
                }
            }
            public void UnRender()
            {
                foreach (Screen.Point p in points)
                {
                    p.UnRender();
                }
            }
            public Mesh(Screen.Point Start, Screen.Point End, System.ConsoleColor col)
            {
                HashSet<Screen.Point> pts = new HashSet<Screen.Point>();
                double dist = Start.DistanceFrom(End);
                for (int i = 0; i < dist; i++)
                {
                    pts.Add(new Screen.Point(Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(Start.GetX()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(End.GetX())), Convert.ToInt16((1 - Convert.ToDouble(i) / dist) * Convert.ToDouble(Start.GetY()) + (Convert.ToDouble(i) / dist) * Convert.ToDouble(End.GetY())), col));
                }
                points = pts;
                collider = false;
                rendered = false;
            }
            public static Screen.Point GetMiddlePoint(Mesh mes)
            {
                int i = 0;
                Screen.Point pt = new Screen.Point();
            try
            {
                if (mes.GetPoints().Count > 0)
                {
                    foreach (Screen.Point p in mes.GetPoints())
                    {
                        pt.Transform(p);
                        i++;
                    }
                    pt.SetX(Convert.ToInt16(Convert.ToDouble(pt.GetX()) / Convert.ToDouble(i)));
                    pt.SetY(Convert.ToInt16(Convert.ToDouble(pt.GetY()) / Convert.ToDouble(i)));
                    return pt;
                }
            }
            catch
            {
            }
            return null;
            }
            public void MiddlePointMoveTo(Screen.Point pt)
            {
                Screen.Point poin =Mesh.GetMiddlePoint(this);
                int movx = pt.GetX() - poin.GetX();
                int movy = pt.GetY() - poin.GetY();
                this.Transform(movx, movy);
            }
            public void MergeWith(Mesh mes)
            {
                points.UnionWith(mes.GetPoints());
            }
            public void ColorChange(System.ConsoleColor col)
            {
                foreach (Screen.Point p in points)
                {
                    p.SetColor(col);
                }
            }
            public static bool OutOfWindow(Mesh mes)
            {
                foreach (Screen.Point p in mes.points)
                {
                    if (p.GetX() < 0 || p.GetX() > Screen.GetWidth() || p.GetY() > Screen.GetHeight() || p.GetY() < 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool TouchesTop(Mesh mes)
            {
                foreach (Screen.Point p in mes.points)
                {
                    if (p.GetY() > Screen.GetHeight())
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool TouchesFloor(Mesh mes)
            {
                foreach (Screen.Point p in mes.points)
                {
                    if (p.GetY() < 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool TouchesLeft(Mesh mes)
            {
                foreach (Screen.Point p in mes.points)
                {
                    if (p.GetX() < 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool TouchesRight(Mesh mes)
            {
                foreach (Screen.Point p in mes.points)
                {
                    if (p.GetX() > Screen.GetWidth())
                    {
                        return true;
                    }
                }
                return false;
            }
        public Mesh( ConsoleColor col, params Screen.Point[] pts)
        {
            HashSet<Screen.Point> pt = new HashSet<Screen.Point>();
            Mesh line = new Mesh();
            for(int i = 0; i < pts.Length-1; i++)
            {
                line = new Mesh(pts[i], pts[i + 1], col);
                pt.UnionWith(line.points);
            }
            points = pt;
            collider = false;
            rendered = false;
        }
            public Mesh(HashSet<Screen.Point> pts, ConsoleColor col)
            {
                foreach (Screen.Point p in pts) { p.SetColor(col); }
                collider = false;
                rendered = false;
                points = pts;
            }
        }
    }

