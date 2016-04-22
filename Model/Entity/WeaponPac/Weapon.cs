using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;
using Model.Entity.UnitPac;
using System.Windows.Forms;
using Model.Entity.WeaponPac.States;
using Model.Entity.BulletPac;


namespace Model.Entity.WeaponPac
{
    public class Weapon : DrawableObject, IDrawable, IFireable
    {
        public const int WeaponSize = 40;

        public bool _canFire;
        public Timer _fireTimer;

        public PointF MousePos;
        private PointF _gunPos = new Point(0, 0); //Unit
        public Logic model;
        public PointF GunDirection { get; set; }

        public PointF GunPos
        {
            get { return _gunPos; }
            set
            {
                _gunPos = new PointF(value.X + MapPac.UnitSizeTile.UnitSize/2, value.Y - MapPac.UnitSizeTile.UnitSize/2);
                this.Rect = new Rectangle((int) value.X, (int) -value.Y,
                    WeaponSize, WeaponSize);
            }
        }

        public float BulletSpeed = 7f;

        public PointF WeaponOpinion { get; set; }
        public static readonly PointF DEFAULT_UNIT_VECTOR = new PointF(20, 0);

        private float angle = 0;

        private Unit _unit;
        public MapPac.Map LevelMap { get; set; }

        public int Damage = 100;

        private State _state;

        public State State
        {
            get { return _state; }
            set
            {
                if (_state != null)
                    value.Direct = _state.Direct;
                _state = value;
                _state.Action();
            }
        }

        public Weapon(Unit ovner, Logic logic)
        {
            _unit = ovner;
            model = logic;
            
            UpdateUnitPosition();

            StandState stand = new StandState(this);

            stand.transitions.Add(0, stand);

            State = stand;
            this.State.Direct = Direction.Right;

            _canFire = true;

            _fireTimer = new Timer();
            _fireTimer.Interval = 500;
            _fireTimer.Tick += _fireTimer_Tick;
        }

        private void _fireTimer_Tick(object sender, EventArgs e)
        {
            _fireTimer.Stop();
            _canFire = true;
        }

        private void UpdateUnitPosition()
        {
            GunPos = new PointF(_unit.Rect.X, -_unit.Rect.Y);
        }

        public void Draw(Graphics Graph, float time)
        {
            UpdateUnitPosition();

            PointF vectorUnitToMouse = _gunPos.Substract(MousePos);

            angle = DEFAULT_UNIT_VECTOR.GetAngle(vectorUnitToMouse);
           // Console.WriteLine("angle = " + angle);

            State.Anim.RotateImage(angle);

            State.Anim.NextFrame(time);
            State.Anim.Draw(Graph, State.Angles[Direction.Right], this.Rect);
            State.Anim.Source = Properties.Resources.tower;
        }

        public void Fire()
        {
            if (_canFire)
            {
                WeaponOpinion = _gunPos.Substract(MousePos);
                WeaponOpinion = WeaponOpinion.Normalize();

                model.AddBullet(new Bullet(model, _unit));

                _canFire = false;
                _fireTimer.Start();
            }

        }
    }
}
