using System.Drawing;
using Model.Entity.BulletPac;
using Model.Entity.MapPac.States;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.Element
{
    public class IronElement : ElementSizeTile, ITarget
    {
        private Logic _model;
        public IronElement(Point pos, Logic model)
        {
            _model = model;
            Rect = new Rectangle(0, 0, ElementSize, ElementSize);

            SetPixelPositon(pos);

            StandState stand = new StandState(this, Properties.Resources.iron);
            DestroingState destroing = new DestroingState(this, _model, this);

            stand.transitions.Add(0, stand);
            stand.transitions.Add(1, destroing);

            State = stand;
            State.Direct = Direction.Right;
        }
        
        public void GetBulletShot(Bullet bullet)
        {
            bullet.Explode();
        }
    }
}