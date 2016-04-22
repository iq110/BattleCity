using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using View;
using View = System.Windows.Forms.View;

namespace LastBattleCity
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
//            Game screen size
//            int x = 40 * 15;
//            int y = 40 * 15;
//
//            BattleCity view = new BattleCity(x,y);
            BattleCity view = new BattleCity();
            Logic model = new Logic();
            Controller controller = new Controller(view, model, "Player 0");


            Application.Run(view);
        }
    }
}
