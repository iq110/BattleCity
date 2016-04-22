using System.Drawing;

namespace Model.Entity.AnimationPac
{
    public abstract class DrawableObject
    {
        public Rectangle Rect { get; set; }
        public DrawableObject()
        {
        }

        public void SetPixelPositon(Point newPos)
        {
            Rect = new Rectangle(newPos.X, newPos.Y, Rect.Width, Rect.Height);

        }

        public Point GetPixelPositon()
        {
            return new Point(Rect.X, Rect.Y);
        }


        public DrawableObject(int x, int y, int width, int height)
        {

            Rect = new Rectangle(x, y, width, height);
        }

    }
}