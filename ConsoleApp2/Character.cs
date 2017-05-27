using System.Collections.Generic;
using System;
using System.Threading;

namespace ConsoleApp2
{         
                public class Character:IDisposable 
                {
        /*private static HashSet<Mesh> bullets = new HashSet<Mesh>();
        public static HashSet<Mesh> GetBullets()
        {
            return bullets;
        }
        public static void AddBullets(Mesh pt)
        {
            bullets.Add(pt);
        }
        public static void BulletsFly(int moveduration)
        {
            HashSet<Mesh> trash = new HashSet<Mesh>();
            try
            {
                foreach (Mesh bull in bullets)
                {
                    if (Mesh.GetMiddlePoint(bull).GetY() > Screen.GetHeight())
                    {
                        trash.Add(bull);
                    }
                    else
                    {
                        bull.Transform(0, 1);
                    }
                }
            }
            catch { }
            foreach(Mesh tr in trash)
            {
                Mesh.Remove(tr);
            }
            Thread.Sleep(moveduration);
            BulletsFly(moveduration);
        }*/
        protected virtual void Dispose(bool ti)
        {

        }
        public void Dispose()
        {
            try { enemies.Remove(this); } catch { }
            try { playables.Remove(this); } catch { }
            try { this.mesh.SetRender(false); } catch { }
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public static HashSet<Character> GetEnemies()
        {
            return enemies;
        }
        public static HashSet<Character> GetPlayables()
        {
            return playables;
        }
        public Character(int x, int y, ConsoleColor col,bool plbl)
        {
            mesh = new Mesh();
            mesh.AddPoint(new Screen.Point(x, y, col));
            this.SetPlayableOrEnemy(plbl);
            this.mesh.SetRender(true);
        }
        /*public void CheckIfDies()
        {
            while (true)
            {
                    foreach (Mesh mes *in Gameplay.Stage.GetObstacles())
                    {
                        if (this.mesh.CollidesWith(mes)) { this.mesh.SetRender(false); this.Dispose(); }
                    }
            }
        }*/
        ~Character()
        {
            try { enemies.Remove(this); } catch { }
            try { playables.Remove(this); } catch { }
            
        }
        public static void EnemyAttack(int moveduration){
            HashSet<Character> plb = new HashSet<Character>();
            foreach(Character mes in playables)
            {
                foreach(Character ms in enemies)
                {
                    if (mes.mesh.CollidesWith(ms.mesh)) { plb.Add(mes); }
                    ms.mesh.Transform(Screen.Point.NormalizedVector(Mesh.GetMiddlePoint(ms.mesh), Mesh.GetMiddlePoint(mes.mesh)));
                }
            }
            foreach(Character ch in plb) { ch.SetPlayableOrEnemy(false); }
            Thread.Sleep(moveduration);
            EnemyAttack(moveduration);
        } 
        public static HashSet<Character> enemies = new HashSet<Character>();
        public static HashSet<Character> playables = new HashSet<Character>();
        Mesh mesh;
        bool enemy;
        bool playable;
        public void SetPlayableOrEnemy(bool ch)
        {
            playable = ch;
            enemy = !ch;
            if (ch)
            {
                playables.Add(this);
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
                    public void Move()
                    {
            if (this.playable)
            {
                Mesh.Animate.KeyMove(this.mesh);
                Move();
            }
        }
            }
        }
