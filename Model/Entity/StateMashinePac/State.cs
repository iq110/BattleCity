using System.Collections.Generic;
using Model.Entity.AnimationPac;

namespace Model.Entity.StateMashinePac
{
    public abstract class State
    {
        public static readonly Dictionary<Direction, int> Angles = new Dictionary<Direction, int>
        {
            {Direction.Up, -90},
            {Direction.Right, 0},
            {Direction.Down, 90},
            {Direction.Left, 180},
        };

        private Direction _direction = Direction.Left;

        public Direction Direct
        {
            get { return _direction; }
            set
            {
                if (CanChangeState)
                    _direction = value;
            }
        }

        public DrawableObject DrawingObject;

        //next states
        //0 - transition on itself
        //1 - transition on next state
        public Dictionary<int, State> transitions = new Dictionary<int, State>();

        public AnimationPac.Animation Anim;

        public bool CanChangeState = true;

        public abstract void Interrupt();
        public abstract void Action(); 
    }
}