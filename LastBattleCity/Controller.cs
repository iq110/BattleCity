using System.Windows.Forms;
using Model;
using View;

namespace LastBattleCity
{
    public class Controller
    {
        private readonly BattleCity  _view;
        private readonly Logic  _logic;
        private readonly int _playerId;
        public Controller(BattleCity view, Logic logic,string playerName)
        {
            _view = view;
            _logic = logic;
           
            //Give to Logic screen size of View
            _logic.SetScreenWidht(_view.GetScreenWidht());
            _logic.SetScreenHeight(_view.GetScreenHeight());

            //do subscriptions
            _view.KeyPressed += GetPressedKeyFromView;
            _view.KeyUnpressed += GetUnpressedKeyFromView;
            _view.RedrawMap += RedrawMap;
            _view.MouseMoved += GetMousePosition;
            //init player
            _playerId = _logic.CreatePlayer(playerName);
            _logic.InitRobots();

        }

        private void RedrawMap(PaintEventArgs e)
        {
            _logic.DrawAll(e.Graphics);
            //_logic.Graphic = e.Graphics;
        }

        #region Keyboard work

        public void GetMousePosition(MouseEventArgs e)
        {
            Logic.Players[_playerId].MouseMoved(e);
        }

        public void GetPressedKeyFromView(Keys key)
        {
            Logic.Players[_playerId].ActionByPressedKey(key);
            UpdateView();
        }

        public void GetUnpressedKeyFromView(Keys key)
        {
            Logic.Players[_playerId].MakeUnitActionByUnpressedKey(key);
            Logic.Players[_playerId].MoveUnit();
            UpdateView();
        }

        public void UpdateView()
        {
            _view.UpdateView();
        }

        #endregion

    }
}