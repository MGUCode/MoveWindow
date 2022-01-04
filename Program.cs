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
            Process MyMovingProcess;


            //Get program parameters
            MyParameters.GetParameters(args);

            /*
             * If the path app is not null, start the programm with the path
             * else, find the program by his window name
             */
            if (MyParameters.AppPath != "")
            {
                MyMovingProcess = MyScreen.StartProgram(MyParameters.AppPath, MyParameters.AppParameters, MyParameters.AppWorkingDirectory);

                //Wait some time before moving program
                //Force the timer for waiting the windows to open
                if (MyParameters.Timer == 0)
                {
                    WaitTime((float)0.5);
                }
                else
                {
                    WaitTime(MyParameters.Timer);
                }
            }
            else
            {
                //Wait some time before finding program
                WaitTime(MyParameters.Timer);

                //Find the program corresponding process
                MyMovingProcess = MyScreen.FindWindowByName(MyParameters.WindowName, MyParameters.DebugMode);
            }

            //Finally move the program
            MyScreen.MoveWindow(MyMovingProcess, MyParameters.XPos, MyParameters.YPos);

        }

        /// <summary>
        /// Wait before execute the next statement
        /// </summary>
        /// <param name="Time">Waiting time in seconds</param>
        static void WaitTime(float Time)
        {
            if (Time != 0)
            {
                //convert to milliseconds
                int tmpint = (int)(Time * 1000);
                Thread.Sleep(tmpint);
            }
        }

    }
}
