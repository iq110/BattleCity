using System.Drawing;
using Model.Entity.MapPac.Element;

namespace Model.Entity.MapPac
{
    public class Map
    {
        private Logic _model;
        public const int MapWidth = 15;
        public const int MapHeight = 15;
        private UnitSizeTile[,] Level = new UnitSizeTile[MapWidth, MapHeight];

        public int Lenght { get { return Level.Length; } }

        public int[,] DefaultLevel =
        {
            {2,0,0,0,0,0,2,0,2,0,0,0,0,0,2},
            {2,0,0,0,0,0,2,2,2,0,0,0,0,0,2},
            {2,0,0,1,1,0,0,0,0,0,1,1,0,0,2},
            {2,0,0,1,1,0,0,0,0,0,1,1,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,1,1,1,1,1,1,1,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {2,0,0,1,1,0,0,0,0,0,1,1,0,0,2},
            {2,0,0,1,1,0,0,0,0,0,1,1,0,0,2},
            {2,0,0,0,0,0,2,2,2,0,0,0,0,0,2},
            {2,0,0,0,0,0,2,0,2,0,0,0,0,0,2},
        };
        public Map(Logic model)
        {
            _model = model;
            Point tilePlace = new Point(0, 0);
            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    Level[i, j] = new UnitSizeTile(DefaultLevel[i, j], tilePlace, model);
                    tilePlace.X += UnitSizeTile.UnitSize;
                }
                tilePlace.X = 0;
                tilePlace.Y += UnitSizeTile.UnitSize;
            }
        }

        public UnitSizeTile this[int i, int j]
        {
            get { return this.Level[j, i]; }
            set { this.Level[j, i] = value; }
        }

        public void RemoveElem(ElementSizeTile tile)
        {
            foreach (var unitSizeTile in Level)
            {
                for (int i = 0; i < unitSizeTile.Content.Length; i++)
                {
                    if (unitSizeTile.Content[i] == tile) unitSizeTile.Content[i] = null;
                }
            }
        }
    }
}