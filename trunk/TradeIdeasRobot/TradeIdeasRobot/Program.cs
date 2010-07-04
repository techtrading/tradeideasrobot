using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NLog;

namespace TradeIdeasRobot
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            logger.Info("* * * * * * * Starting TradeIdeasRobot * * * * * * *");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BotModel model = new BotModel();
            BotForm form = new BotForm();
            BotController controller = new BotController(model);
            form.Controller = controller;
            controller.LoadModel();
            Application.Run(form);
        }
    }
}
