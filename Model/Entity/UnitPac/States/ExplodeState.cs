using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;
using System.Windows.Forms;


namespace Model.Entity.UnitPac.States
{
    public class ExplodeState : State
    {
        private Logic _logic;
        private Unit _unit;
        private int _tickCounter;
        readonly Timer _timer = new Timer();

        public ExplodeState(DrawableObject o, Unit u, Logic m)
        {
            _logic = m;

            this.DrawingObject = o;
            this._unit = u;

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
                    _logic.RemoveUnit(_unit);
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