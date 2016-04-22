using System;
using System.Drawing;
using System.Windows.Forms;
using Model;
using SFML.Window;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;

namespace View
{
    public delegate void MouseMoving(MouseEventArgs e);

    public delegate void KeyPress(Keys keys);

    public delegate void ReDraw(PaintEventArgs e);
    public partial class BattleCity : Form
    {
        private readonly int _screenWidth = 40 * 15;//600
        private readonly int _screenHeight = 40 * 15;//600

        public event MouseMoving MouseMoved;
        public event KeyPress KeyPressed;
        public event KeyPress KeyUnpressed;
        public event ReDraw RedrawMap;
        public Graphics Graphic;

        public Logic Model { get; set; }
        private Timer _keyInterviewTimer;

        private Keys _key;

        public BattleCity()
        {

            _keyInterviewTimer = new Timer();
            _keyInterviewTimer.Interval = 10;
            _keyInterviewTimer.Tick += KeyInterviewTimerOnTick;
            _keyInterviewTimer.Start(); 

             Timer timerInvalidate = new Timer();
             timerInvalidate.Interval = 10;
             timerInvalidate.Tick += InvalidateFunction;
             timerInvalidate.Start();

            this.KeyUp += View_KeyUp;
            this.Paint += View_Paint;

          
            MouseMove += BattleCity_MouseMove;

            InitializeComponent();
        }

        private void InvalidateFunction(object sender, EventArgs e)
        {
            InvalidateFunction();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BattleCity
            // 
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "BattleCity";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BattleCity_MouseDown);
            this.ResumeLayout(false);

        }

        void BattleCity_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseMoved != null) MouseMoved(e);
        }

        public void InvalidateFunction()
        {
            Invalidate();
        }

        public int GetScreenWidht()
        {
            return _screenWidth;
        }
        public int GetScreenHeight()
        {
            return _screenHeight;
        }

        #region PlayersControl

        private void KeyInterviewTimerOnTick(object sender, EventArgs eventArgs)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                if (KeyPressed != null) KeyPressed(Keys.W);

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                if (KeyPressed != null) KeyPressed(Keys.A);

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                if (KeyPressed != null) KeyPressed(Keys.S);

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                if (KeyPressed != null) KeyPressed(Keys.D);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                if (KeyPressed != null) KeyPressed(Keys.Space);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                if (KeyPressed != null) KeyPressed(Keys.Up);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                if (KeyPressed != null) KeyPressed(Keys.Down);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                if (KeyPressed != null) KeyPressed(Keys.Left);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                if (KeyPressed != null) KeyPressed(Keys.Right);
        }
        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            _key = e.KeyCode;
            if (KeyUnpressed != null) KeyUnpressed(_key);
            Console.WriteLine("Отжата " + _key.ToString());
        }
        #endregion

        #region DrawingWork
        private void View_Paint(object sender, PaintEventArgs e)
        {
              if (RedrawMap != null) RedrawMap(e);
            //  model.Graphic = e.Graphics;
//            Model.DrawAll(e.Graphics);
//            KeyInterviewTimerOnTick(null, null);
//            InvalidateFunction();
        }



        public void UpdateView()
        {
            this.Invalidate();
        }



        #endregion

        private void BattleCity_MouseDown(object sender, MouseEventArgs e)
        {
            if (KeyPressed != null) KeyPressed(Keys.Space);
        }

    }
}
