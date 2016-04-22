using System;
using System.Collections.Generic;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.MapPac;
using Model.Entity.StateMashinePac;
using System.Windows.Forms;
using Model.Entity.BulletPac;
using Model.Entity.WeaponPac;
using Model.Entity.UnitPac.States;
using Model.Entity.MapPac;


namespace Model.Entity.UnitPac
{
    internal delegate void Del(int i);

    public class Unit : DrawableObject, ITarget, IDrawable, IFireable
    {
        public Map LevelMap { get; set; }

        private Point _positionMap;
        public Point PositionMap
        {
            get { return _positionMap; }
            set
            {
                Console.Out.WriteLine(value);
                _positionMap = value;
            }
        }

        private Logic model;
        public Boolean IsAlive { get; set; }
        public bool IsPlayer { get; set; }
        public List<Unit> DeadTargets { get; set; }
        public Weapon Gun { get; set; }
        public int Health = 100;
        public int PriceForDeath = 200;

        private State _state;

        public State State
        {
            get { return _state; }
            set
            {
                if (_state != null)
                {
                    value.Direct = _state.Direct;
                }
                _state = value;
                _state.Action();
            }
        }

        public Unit()
        {
            StandState stand = new StandState(this);
            MoveState move = new MoveState(this, this, stand);
            ExplodeState explosion = new ExplodeState(this, this, model);

            stand.transitions.Add(0, stand);
            stand.transitions.Add(1, move);
            stand.transitions.Add(5, explosion);

            move.transitions.Add(0, stand);
            move.transitions.Add(5, explosion);

            Rect = new Rectangle(0, 0, UnitSizeTile.UnitSize, UnitSizeTile.UnitSize);

            State = stand;
            State.Direct = Direction.Right;

        }

        public Unit(Logic m, Map map, int x, int y)
            : this()
        {
            model = m;
            LevelMap = map;
            LevelMap[x, y].UnitOnTile = this;
            SetPixelPositon(LevelMap[x, y].Position);
            PositionMap = new Point(x, y);
            Gun = new Weapon(this, m);

        }

        public void FinishTransition()
        {
            HandleEvent(0);
        }

        public void Explode()
        {
            HandleEvent(5);
        }
        public void Draw(Graphics Graph, float time)
        {
            State.Anim.NextFrame(time);
            State.Anim.Draw(Graph, State.Angles[State.Direct], Rect);

            Gun.Draw(Graph, time);
        }
        public void HandleEvent(int even)
        {
            if (State.CanChangeState)
                if (State.transitions.ContainsKey(even))
                    State = State.transitions[even];
        }
        public bool CanMove(int x, int y)
        {
            if (State.CanChangeState)
                if ((x < Map.MapWidth && x >= 0) && (y < Map.MapHeight && y >= 0)
                    && LevelMap[x, y].UnitOnTile == null && LevelMap[x, y].IsClear())
                    return true;
            return false;
        }
        public void MoveLeft()
        {
            if (CanMove(PositionMap.X - 1, PositionMap.Y))
            {
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = null;
                PositionMap = new Point(PositionMap.X - 1, PositionMap.Y);
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = this;
                HandleEvent(1);
            }
        }
        public void MoveRight()
        {
            if (CanMove(PositionMap.X + 1, PositionMap.Y))
            {
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = null;
                PositionMap = new Point(PositionMap.X + 1, PositionMap.Y);
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = this;
                HandleEvent(1);

            }
        }
        public void MoveUp()
        {
            if (CanMove(PositionMap.X, PositionMap.Y - 1))
            {
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = null;
                PositionMap = new Point(PositionMap.X, PositionMap.Y - 1);
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = this;
                HandleEvent(1);
            }
        }
        public void MoveDown()
        {
            if (CanMove(PositionMap.X, PositionMap.Y + 1))
            {
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = null;
                PositionMap = new Point(PositionMap.X, PositionMap.Y + 1);
                LevelMap[PositionMap.X, PositionMap.Y].UnitOnTile = this;
                HandleEvent(1);

            }
        }
        public void GetBulletShot(Bullet bullet)
        {
            if (bullet.Owner.IsPlayer)
            {
                bullet.Owner.DeadTargets.Add(this);
            }
            Explode();
            model.RemoveBullet(bullet);

        }

        public void Fire()
        {
            this.Gun.Fire();
        }
    }
}