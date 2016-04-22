using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.MapPac;
using Model.Entity.StateMashinePac;
using System.Windows.Forms;



namespace Model.Entity.UnitPac.States
{
    public class MoveState : State
    {
        public const int MovingStep = UnitSizeTile.UnitSize;
        public const int StepSize = 2;
        public const int ShiftInterval = 15;
        private int _shiftCounter = 0;
        private Unit _unit;
        readonly Timer _timerShiftPicture = new Timer();
        private readonly State _backState;
        public MoveState(DrawableObject o, Unit u, State backState)
        {
            DrawingObject = o;
            _unit = u;
            _backState = backState;
            Anim = new Animation
                (Properties.Resources.spriteMoveWithoutTower,
                0, 5, 40, 40, 7, 0.02F, 10);

            _timerShiftPicture.Interval = ShiftInterval;
            _timerShiftPicture.Tick += _timerShiftPicture_Tick;
        }

        private void _timerShiftPicture_Tick(object sender, EventArgs e)
        {
            if (_shiftCounter < MovingStep)
            {
                Point pos = DrawingObject.GetPixelPositon();

                if (Direct == Direction.Up)
                    pos.Y -= StepSize;
                else if (Direct == Direction.Right)
                    pos.X += StepSize;
                else if (Direct == Direction.Down)
                    pos.Y += StepSize;
                else
                    pos.X -= StepSize;

                DrawingObject.SetPixelPositon(pos);
                _shiftCounter += StepSize;
            }
            else
            {
                _timerShiftPicture.Stop();
                CanChangeState = true;
                _unit.State = _backState;
            }
        }

        public override void Interrupt()
        {
            _timerShiftPicture.Stop();
        }

        public override void Action()
        {
            _shiftCounter = 0;
            CanChangeState = false;
            _timerShiftPicture.Start();
        }
    }
}