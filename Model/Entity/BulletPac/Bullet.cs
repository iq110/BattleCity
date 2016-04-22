using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;
using Model.Entity.UnitPac;
using Model.Entity.BulletPac.States;
using Model.Entity;

namespace Model.Entity.BulletPac
{
    public class Bullet: DrawableObject, ITarget, IDrawable
    {
        public const int BulletSize = 12;
        public readonly Unit Owner;
        private PointF _currentPos;
        private Logic _logic;
        public Boolean IsAlive { get; set; }
        public PointF CurrentPos
        {
            get { return _currentPos; }
            set 
            { 
                _currentPos = value;
                Rect = new Rectangle((int)CurrentPos.X, (int)-CurrentPos.Y,
                    BulletSize, BulletSize);
            }
        }
        public PointF MovingDirection;

        private State _state;
        public float Speed;
        public State State
        {
            get { return _state; }
            set
            {
                if (_state != null)
                {
                    value.Direct = _state.Direct;
                    _state.Interrupt();
                }
                 _state = value;
                _state.Action();
            }
        }
        public Bullet(Logic m, Unit owner)
        {
            _logic = m;
            Owner = owner;
            Speed = owner.Gun.BulletSpeed;

            MoveState moveState = new MoveState(this);
            RespawnState respawningState = new RespawnState(this, this, moveState);
            ExplodeState explodeState = new ExplodeState(this, this,_logic);

            MovingDirection = Owner.Gun.WeaponOpinion;

            CurrentPos = Owner.Gun.GunPos;
            CurrentPos = CurrentPos.Addition(MovingDirection.Multiplication(Speed * 4));

            moveState.transitions.Add(5, explodeState);
            State = respawningState;
            State.Direct = Direction.Right;
        }

        public void HandleEvent(int even)
        {
            if (State.CanChangeState)
                if (State.transitions.ContainsKey(even))
                    State = State.transitions[even];
        }
        public void Explode()
        {
            HandleEvent(5);
        }

        public void GetBulletShot(Bullet bullet)
        {
            Explode();
        }

        public void Remove()
        {
            this.State.Interrupt();
            _logic.RemoveBullet(this);
        }

        public void Draw(Graphics Graph, float time)
        {
            State.Anim.NextFrame(time);
            State.Anim.Draw(Graph, State.Angles[State.Direct], Rect);
        }
    }
}