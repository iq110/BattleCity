using System.Collections.Generic;
using System.Drawing;

namespace Model.Entity.AnimationPac
{
    public class Animation
    {
         public List<Rectangle> Frames = new List<Rectangle>();
        private int _index;
        private float _currentFrame;
        public float Speed;
        public  bool Loop, IsPlaying;   // loop показвает зациклена ли анимация. Например анимация взрыва должна проиграться один раз и остановиться, loop=false
        public Image Source = Properties.Resources._null;

        public Animation()
        {
            _currentFrame = 0;
            IsPlaying = true;
            Loop = true;
        }
        public Animation(Image source, int x, int y, int width, int height,
           int count, float speed, int stepBetbeenSprites = 0, bool loop = true):this()
        {
            Speed = speed;
            Loop = loop;
            Source = source;

            for (int i = 0; i < count; i++)
            {
                Frames.Add(new Rectangle(x + i * stepBetbeenSprites + i * width, y, width, height));
            }
        }

        public void Draw(Graphics graphics,int angle, Rectangle scrRect)
        {
            Rectangle cur = Frames[_index];

            Draw(graphics, angle, Source,
                scrRect.X, scrRect.Y, scrRect.Height, scrRect.Width,
                cur.X, cur.Y, cur.Height, cur.Width);
        }

        public void Draw(Graphics graphics,int angle, Image source,
            int scrX, int scrY, int scrH, int scrW,
            int imX, int imY, int imH, int imW)
        {
            switch (angle)
            {
                default:
                case 0:
                    graphics.DrawImage(source,
                                new Rectangle(scrX, scrY, scrW, scrH),
                                new Rectangle(imX, imY, imW, imH), GraphicsUnit.Pixel);
                    break;
                case 90:
                    graphics.RotateTransform(angle);     // поворачиваем экран
                    graphics.DrawImage(source,
                                  new Rectangle(scrY, -scrX - scrH, scrH, scrW),
                                  new Rectangle(imX, imY, imW, imH), GraphicsUnit.Pixel);
                    graphics.ResetTransform();          // возвращаем назад
                    break;
                case -90:
                    graphics.RotateTransform(angle);     // поворачиваем экран
                    graphics.DrawImage(source,
                                  new Rectangle(-scrY - scrW, scrX, scrH, scrW),
                                  new Rectangle(imX, imY, imW, imH), GraphicsUnit.Pixel);
                    graphics.ResetTransform();          // возвращаем назад
                    break;
                case 180:
                    graphics.DrawImage(source,
                    new Rectangle(scrX, scrY, scrW, scrH),
                    new Rectangle(imX + imW, imY + imH, -imW, -imH), GraphicsUnit.Pixel);
                    break;
            }
        }


        /// <summary>
        /// Creates a new Image containing the same image only rotated
        /// </summary>
        /// <param name="offset">The position to rotate from.</param>
        /// <param name="angle">The amount to rotate the image, clockwise, in degrees</param>
        public void RotateImage(float angle)
        {
            PointF offset = new PointF((float)Source.Width / 2, (float)Source.Height / 2);
            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(Source.Width, Source.Height);
            rotatedBmp.SetResolution(Source.HorizontalResolution, Source.VerticalResolution);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);
            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);
            //rotate the image
            g.RotateTransform(angle);
            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);
            //draw passed in image onto graphics object
            g.DrawImage(Source, new PointF(0, 0));
            this.Source = rotatedBmp;
        }
        public void NextFrame(float time)
        {
            if (!IsPlaying) return;

            _currentFrame += Speed * time;

            if (_currentFrame > Frames.Count)
            {
                _currentFrame -= Frames.Count;
                if (!Loop)
                {
                    IsPlaying = false;
                    return;
                }
            }

            _index = (int)_currentFrame;
        }

        public Rectangle GetFrame()
        {
            return Frames[_index];
        }

    }
}