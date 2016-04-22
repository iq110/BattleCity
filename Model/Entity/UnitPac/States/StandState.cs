using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;

namespace Model.Entity.UnitPac.States
{
    public class StandState : State
    {
        public StandState(DrawableObject o)
            : this()
        {
            this.DrawingObject = o;
        }

        public StandState()
        {
            Anim = new AnimationPac.Animation
                (Properties.Resources.spriteMoveWithoutTower,
                0, 5, 40, 40, 1, 0.04F, 1, false);
        }

        public override void Interrupt()
        {
            ;
        }

        public override void Action()
        {

        }
    }
}