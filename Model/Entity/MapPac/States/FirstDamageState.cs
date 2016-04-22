using System;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.States
{
    public class FirstDamageState : State
    {

        public FirstDamageState(DrawableObject o, Animation anim)
        {
            this.DrawingObject = o;
            this.Anim = anim;
            Direct = Direction.Right;
        }

        public override void Interrupt()
        {
            Console.WriteLine("FirstDamageState : Interrupt");
        }

        public override void Action()
        {
            Anim.Source = Properties.Resources.wallFirst;
        }
    }
}