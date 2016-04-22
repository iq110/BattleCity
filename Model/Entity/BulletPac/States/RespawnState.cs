using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;
using System.Windows.Forms;


namespace Model.Entity.BulletPac.States
{
    public class RespawnState : State
    {
        private int _tickCounter;
        private readonly Bullet _bullet;
        readonly Timer _timer = new Timer();
        private readonly State _backState;

        public RespawnState(DrawableObject o, Bullet b, State backState)
        {
            this.DrawingObject = o;
            this._bullet = b;
            _backState = backState;
            Anim = new Animation
                (Properties.Resources.bulResp,
                0, 0, 20, 20, 1, 0.04F, 1, false);

            _timer.Interval = 30;
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
                    CanChangeState = true;
                    _bullet.State = _backState;
                    _bullet.IsAlive = true;
                    break;
            }
        }

        public override void Interrupt()
        {
            _timer.Stop();
        }

        public override void Action()
        {
            _tickCounter = 0;
            CanChangeState = false;
            _timer.Start();
        }
    }
}