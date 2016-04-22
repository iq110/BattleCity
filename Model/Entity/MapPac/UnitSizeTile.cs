using System.Drawing;
using Model.Entity.AnimationPac;
using Model.Entity.MapPac.Element;

namespace Model.Entity.MapPac
{
    public class UnitSizeTile : IDrawable
    {
        private Logic _model;
        public const int UnitSize = ElementSizeTile.ElementSize * 2;
        public ElementSizeTile[] Content = new ElementSizeTile[4];

        public UnitPac.Unit UnitOnTile;

        public Point Position;
        public UnitSizeTile(int type, Point pos, Logic model)
        {
            _model = model;
            this.Position = pos;
            if (type == 2)
            {
                Content[0] = new WallElement(pos, _model);
                Content[0].State.Anim.Frames[0] = new Rectangle(0,0,20,20);
                Content[1] = new WallElement(new Point(pos.X + ElementSizeTile.ElementSize, pos.Y), _model);
                Content[1].State.Anim.Frames[0] = new Rectangle(20,0,20,20);
                Content[2] = new WallElement(new Point(pos.X, pos.Y + ElementSizeTile.ElementSize), _model);
                Content[2].State.Anim.Frames[0] = new Rectangle(0, 20, 20, 20);
                Content[3] = new WallElement(new Point(pos.X + ElementSizeTile.ElementSize, pos.Y + ElementSizeTile.ElementSize), _model);
                Content[3].State.Anim.Frames[0] = new Rectangle(20, 20, 20, 20);
            }

            if (type == 1)
            {
                Content[0] = new IronElement(pos, _model);
                Content[1] = new IronElement(new Point(pos.X + ElementSizeTile.ElementSize, pos.Y), _model);
                Content[2] = new IronElement(new Point(pos.X, pos.Y + ElementSizeTile.ElementSize), _model);
                Content[3] = new IronElement(new Point(pos.X + ElementSizeTile.ElementSize, pos.Y + ElementSizeTile.ElementSize), _model);
            }

            if (type == 0)
            {
                
            }

        }

        public bool IsClear()
        {
            for (int i = 0; i < Content.Length; i++)
            {
                if (Content[i] != null) return false;
            }

            return true;
        }
        public void Draw(Graphics Graph, float time)
        {
            if(UnitOnTile!=null)
                UnitOnTile.Draw(Graph, time);
            
            for (int i = 0; i < Content.Length; i++)
            {
                if (Content[i] != null) Content[i].Draw(Graph, time);
            }
        }
    }
}