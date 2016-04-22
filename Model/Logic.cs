using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Model.Entity;
using Model.Entity.BulletPac;
using Model.Entity.MapPac;
using Model.Entity.UnitPac;
using System.Windows.Forms;

namespace Model
{
    public class Logic
    {
        public static int ScreenWidht { get; private set; }
        public static int ScreenHeight { get; private set; }
        public static List<Player> Players { get; set; }
        public static List<Unit> Units { get; set; }
        public static List<Bullet> Bullets { get; set; }

        public Map LevelMap;
        Stopwatch Clock = new Stopwatch();

        Timer timer = new Timer();


        private int unitsCount;
        private int bulletsCount;



        public Logic()
        {
            LevelMap = new Map(this);
            Bullets = new List<Bullet>();
            Units = new List<Unit>();
            Players = new List<Player>();
            unitsCount++;
        }
        public void InitRobots()
        {
            ;
        }

        public void SetScreenWidht(int widht)
        {
            Logic.ScreenWidht = widht;
        }
        public void SetScreenHeight(int height)
        {
            Logic.ScreenHeight = height;
        }

        public void AddBullet(Bullet b)
        {
            Bullets.Add(b);
            bulletsCount++;
        } 
        
        public void AddUnit(Unit u)
        {
            Units.Add(u);
            unitsCount++;
        }

        public void DrawAll(Graphics Graph)
        {
             float time = Clock.ElapsedMilliseconds;
            Clock.Restart();
            time /= 500;
            if (time < 40) time = 40;

            for (int i = 0; i < Map.MapHeight; i++)
                for (int j = 0; j < Map.MapWidth; j++)
                    LevelMap[j, i].Draw(Graph, time);

           
            foreach (Unit unit in Units)
            {
                unit.Draw(Graph, time);
            }

            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(Graph, time);
            }
            
             CheckItersections();
        }

        

        #region PlayerWork
        public int CreatePlayer(string playerName)
        {
            Player newPlayer = new Player(playerName);
            newPlayer.Unit = new Unit(this, LevelMap, 5,14);
            Units.Add(newPlayer.Unit);
            Players.Add(newPlayer);
            return newPlayer.PlayerId;
        }
        #endregion

        #region RobotWork
        public void RobotAction()
        {
            ;
        }

     
        public void RemoveBullet(Bullet b)
        {
            Bullets.Remove(b);
            this.bulletsCount--;
        }

        public void RemoveUnit(Unit u)
        {
            Units.Remove(u);
            this.unitsCount--;
        }
        public void CheckItersections()
        {
//            #region BulletWithBullet
//            for (int i = 0; i < bulletsCount; i++)
//                for (int j = 0; j < bulletsCount; j++)
//                    if (Bullets[i] != Bullets[j] && Bullets[i].IsAlive && Bullets[j].IsAlive)
//                        if (Bullets[i].Rect.IntersectsWith(Bullets[j].Rect))
//                        {
//                            Bullets[i].GetBulletShot(Bullets[j]);
//                            RemoveBullet(Bullets[j]);
//                        }
//            #endregion
//
//            #region BulletWithUnit
//            for (int i = 0; i < bulletsCount; i++)
//                if (Bullets[i].IsAlive)
//                    for (int j = 0; j < unitsCount; j++)
//                        if (Bullets[i].Owner != Units[j] && Bullets[i].IsAlive && Units[j].IsAlive)
//                        {
//                            if (Bullets[i].Rect.IntersectsWith(Units[j].Rect))
//                                Units[j].GetBulletShot(Bullets[i]);
//                        }
//            #endregion
            #region BulletWithElements
            for (int b = 0; b < bulletsCount; b++)
                if (Bullets[b].IsAlive)
                    for (int i = 0; i < Map.MapHeight; i++)
                        for (int j = 0; j < Map.MapWidth; j++)
                            for (int k = 0; k < LevelMap[j, i].Content.Length; k++)
                                if(LevelMap[j, i].Content[k] is ITarget)
                                    if (Bullets[b].Rect.IntersectsWith(LevelMap[j, i].Content[k].Rect))
                                        ((ITarget)LevelMap[j, i].Content[k]).GetBulletShot(Bullets[b]);     
            #endregion
        }
        #endregion

       

     
    
    }
}