using System;
using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.States
{
    public class StandState : State
    {
        public StandState(DrawableObject o, Image source)
        {
            this.DrawingObject = o;

            Anim = new Animation
                (source,
                    0, 0, 20, 20, 1, 0.04F, 1, false);
            Direct = Direction.Right;
        }


        public override void Interrupt()
        {
            Console.WriteLine("StandState : Interrupt");
        }

        public override void Action()
        {
        }
    }
}