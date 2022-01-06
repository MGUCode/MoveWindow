using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace MoveWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Params MyParameters = new Params();
            ScreenWindFcts MyScreen = new ScreenWindFcts();


            //Get program parameters
            MyParameters.GetParameters(args);

            //Get the process by window name or start directly the program
            MyScreen.GetProcess(MyParameters.WindowName, MyParameters.AppPath, MyParameters.AppParameters, MyParameters.AppWorkingDirectory, MyParameters.Timer, MyParameters.DebugMode);

            //Finally move the window
            MyScreen.MoveTheWindow(MyParameters.XPos, MyParameters.YPos, MyParameters.ScreenIdentifier);

        }
    }
}
