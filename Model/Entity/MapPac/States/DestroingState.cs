using System;
using Model.Entity.AnimationPac;
using Model.Entity.MapPac.Element;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.States
{
    public class DestroingState : State
    {
        private Logic model;
        private ElementSizeTile _owner;
        public DestroingState(DrawableObject o, Logic m, ElementSizeTile owner)
        {
            model = m;
            _owner = owner;
            Direct = Direction.Right;
        }

        public override void Interrupt()
        {
            Console.WriteLine("DestroingState : Interrupt");
        }

        public override void Action()
        {
            model.LevelMap.RemoveElem(_owner);
        }
    }
}