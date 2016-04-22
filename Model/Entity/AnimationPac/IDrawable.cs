using System.Drawing;

namespace Model.Entity.AnimationPac
{
    public interface IDrawable
    {
        void Draw(Graphics e, float time);
    }
}