using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Model.Entity.StateMashinePac;
using Model.Entity.UnitPac;

namespace Model.Entity
{
    public class Player
    {
        public static int PlayerCount { get; set; }
        public Unit Unit { get; set; }
        public int PlayerId { get; set; }
        public String PlayerName { get; set; }
        public List<Unit> DeadTargets { get; set; }

        public void MoveUnit()
        {
            //  Unit.Update();
        }
        public void ActionByPressedKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.W:
                    Unit.State.Direct = Direction.Up;
                    Unit.MoveUp();
                    break;

                case Keys.Right:
                case Keys.D:
                    Unit.State.Direct = Direction.Right;
                    Unit.MoveRight();
                    break;

                case Keys.Down:
                case Keys.S:
                    Unit.State.Direct = Direction.Down;
                    Unit.MoveDown();
                    break;

                case Keys.Left:
                case Keys.A:
                    Unit.State.Direct = Direction.Left;
                    Unit.MoveLeft();
                    break;

                case Keys.Space:
                    Unit.Fire();
                    break;
            }
        }
        public void MakeUnitActionByUnpressedKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                case Keys.W:
                    Unit.HandleEvent(0);
                    break;

                case Keys.Right:
                case Keys.D:
                    Unit.HandleEvent(0);
                    break;

                case Keys.Down:
                case Keys.S:
                    Unit.HandleEvent(0);
                    break;

                case Keys.Left:
                case Keys.A:
                    Unit.HandleEvent(0);
                    break;
            }
        }

        public void MouseMoved(MouseEventArgs e)
        {
            this.Unit.Gun.MousePos = new PointF(e.X, -e.Y);
        }

        public void UnitFire()
        {
            //     Unit.Fire();
        }

        public Player()
        {
            PlayerName = "Player " + PlayerCount;
            PlayerId = PlayerCount;
            PlayerCount++;
            Unit = new Unit();
        }

        public Player(String playrName)
            : this()
        {
            PlayerName = playrName;
        }
    } 
}