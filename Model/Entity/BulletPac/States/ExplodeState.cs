using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;
using System.Windows.Forms;


namespace Model.Entity.BulletPac.States
{
    public class ExplodeState : State
    {
        private Logic _logic;

        private int _tickCounter;
        private readonly Bullet _bullet;
        readonly Timer _timer = new Timer();

        public ExplodeState(DrawableObject o, Bullet b, Logic m)
        {
            _logic = m;

            this.DrawingObject = o;
            this._bullet = b;
            Anim = new Animation
                (Properties.Resources.bulResp,
                0, 0, 20, 20, 1, 0.04F, 1, false);

            _timer.Interval = 100;
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _tickCounter++;
            switch (_tickCounter)
            {
                case (1):
                    Anim.Frames[0] = new Rectangle(30, 0, 20, 20);
                    break;

                case (2):
                    Anim.Frames[0] = new Rectangle(60, 0, 20, 20);
                    break;

                default:
                    _logic.RemoveBullet(_bullet);
                    Interrupt();
                    break;
            }
        }

        public override void Interrupt()
        {
            _timer.Stop();
        }

        public override void Action()
        {
            _bullet.IsAlive = false;
            _bullet.Speed = 0;
            CanChangeState = false;

            _tickCounter = 0;
            _timer.Start();
        }
    }
}