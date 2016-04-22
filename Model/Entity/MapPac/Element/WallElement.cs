using System.Drawing;
using Model.Entity.BulletPac;
using Model.Entity.MapPac.States;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.Element
{
    public class WallElement : ElementSizeTile, ITarget
    {
        private Logic _model;
        public WallElement(Point pos, Logic model)
        {
            _model = model;
            Rect = new Rectangle(0, 0, ElementSize, ElementSize);
            SetPixelPositon(pos);

            StandState stand = new StandState(this, Properties.Resources.wall);
            FirstDamageState firstDamage = new FirstDamageState(this, stand.Anim);
            SecondDamageState secondDamage = new SecondDamageState(this, stand.Anim);
            DestroingState destroing = new DestroingState(this, _model, this);

            stand.transitions.Add(5, firstDamage);

            firstDamage.transitions.Add(5, secondDamage);

            secondDamage.transitions.Add(5, destroing);

            State = stand;
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
            bullet.Explode();
        }
    }
}