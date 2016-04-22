using System.Drawing;
using System.Windows.Forms;
using Model.Entity.AnimationPac;
using Model.Entity.StateMashinePac;

namespace Model.Entity.MapPac.Element
{
    public abstract class ElementSizeTile : DrawableObject,IDrawable
    {
        public const int ElementSize = 20;
        private State _state;

        public State State
        {
            get { return _state; }
            set
            {
                if (_state != null)
                {
                    _state.Interrupt();
                }
                _state = value;
                _state.Action();
            }
        }
        public ElementSizeTile() { }
        public void Draw(Graphics Graph, float time)
        {
            State.Anim.NextFrame(time);
            State.Anim.Draw(Graph, State.Angles[State.Direct], this.Rect);
        }
    }
}