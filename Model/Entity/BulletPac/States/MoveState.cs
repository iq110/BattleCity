using System;
using Model.Entity.AnimationPac;
using System.Windows.Forms;
using Model.Entity.StateMashinePac;


namespace Model.Entity.BulletPac.States
{
    public class MoveState : State
    {
        private UnitPac.Unit _unit;
        private Bullet _owner;

        public Timer _bulletMoveTimer = new Timer();

        public MoveState(Bullet o)
        {
            this.DrawingObject = o;
            _owner = o;

            Anim = new Animation
                (Properties.Resources.bulletMove,
                0, 0, 20, 20, 2, 0.002F, 10);

            _bulletMoveTimer.Interval = 20;
            _bulletMoveTimer.Tick += _bulletMoveTimer_Tick;
        }

        void _bulletMoveTimer_Tick(object sender, EventArgs e)
        {
            _owner.CurrentPos = _owner.CurrentPos.Addition(_owner.MovingDirection.Multiplication(_owner.Speed));
        }

        public override void Interrupt()
        {
            _bulletMoveTimer.Stop();
        }

        public override void Action()
        {
            _bulletMoveTimer.Start();
        }
    }
}