using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace MoveWindow
{
    class ScreenWindFcts
    {     
        Process MyMovingProcess;


        public void GetProcess(string WindowName, string AppPath, string AppParameters, string AppWorkingDirectory, int Timer, bool DebugMode)
        {
            /*
             * If the app path is null, find the program with the window name
             * else, start the program thanks to the app path
             */
            if (AppPath == "")
            {
                //Wait some time before finding program
                WaitTime(Timer);

                //Find the program corresponding process
                MyMovingProcess = FindWindowByName(WindowName, DebugMode);
            }
            else
            {
                MyMovingProcess = StartProgram(AppPath, AppParameters, AppWorkingDirectory);

                //Wait some time before moving program
                //Force the timer for waiting the window to open
                if (Timer == 0)
                {
                    WaitTime((float)0.5);
                }
                else
                {
                    WaitTime(Timer);
                }
            }

        }


        public void MoveTheWindow(int XPos, int YPos, int ScreenIdentifier)
        {
            //Get size of the window like a Rectangle object
            Rectangle WindowRectangle = GetWindowSizes();

            //Move the window by coordinates
            if (ScreenIdentifier == 0)
            {
                WindowRectangle.X = XPos;
                WindowRectangle.Y = YPos;
                MoveWindow(WindowRectangle);
            }
            //Move the window by screen identifier
            else
            {
                Rectangle MyWindow = CreateCoordinates(ScreenIdentifier, WindowRectangle);
                MoveWindow(MyWindow);
            }

        }


        /// <summary>
        /// Get process by his window name
        /// </summary>
        /// <param name="WindowName">Name of the main window</param>
        /// <param name="DebugMode">Enable messages in console</param>
        /// <returns>Return a Process object</returns>
        private Process FindWindowByName(string WindowName, bool DebugMode)
        {
            Process MovingProcess = GetProcess(WindowName, DebugMode);
            return MovingProcess;
        }


        /// <summary>
        /// Get process by his name on the local computer
        /// </summary>
        /// <param name="WindowName">Name of the main window</param>
        /// <param name="DebugMode">Enable messages in console</param>
        /// <returns>Return a Process object. In case of error, return null value</returns>
        private Process GetProcess(string WindowName, bool DebugMode)
        {
            //Get all processes of the local computer on class initialization
            Process[] ComputerProcesses = Process.GetProcesses();

            //If more than 1 process found, it takes the 1st found
            Process aProcess = ComputerProcesses.Where(o => o.MainWindowTitle == WindowName).FirstOrDefault();

            if (aProcess != null)
            {
                return aProcess;
            }
            else
            {
                if (DebugMode)
                {
                    Console.WriteLine("No process found for the Window name:" + WindowName);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
                return null;
            }
        }


        /// <summary>
        /// Start a program with specific parameters
        /// </summary>
        /// <param name="AppPath">Path to the program for execute it</param>
        /// <param name="AppParameters">Program parameters</param>
        /// <param name="AppWorkingDirectory">Program working directory</param>
        /// <returns>Return the corresponding Process object. In case of error, return null value</returns>
        private Process StartProgram(string AppPath, string AppParameters, string AppWorkingDirectory)
        {
            //Set process informations
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = AppPath;
            Console.WriteLine("Program complete path : " + AppPath);
            if (AppParameters != "")
            {
                startInfo.Arguments = AppParameters;
                Console.WriteLine("Program parameters : " + AppParameters);
            }
            if (AppWorkingDirectory != "")
            {
                startInfo.UseShellExecute = false;
                startInfo.WorkingDirectory = AppWorkingDirectory;
                
                Console.WriteLine("Program directory : " + AppWorkingDirectory);
            }
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            //Start program
            try
            {
                Process exeProcess = new Process();
                exeProcess = Process.Start(startInfo);
                return exeProcess;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error have been occured during starting of the program : " + AppPath);
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// Get sizes (Width and Heigh) of a window
        /// </summary>
        /// <returns>Return width and heigh in a Rectangle object</returns>
        private Rectangle GetWindowSizes()
        {
            // Initialization of the RECT structure
            RECT Wind = new RECT();

            // Get coordinates
            WinAPI.GetWindowRect(MyMovingProcess.MainWindowHandle, out Wind);

            // Calculate
            int WindowWidth = Wind.Right - Wind.Left;
            int WindowHeigh = Wind.Bottom - Wind.Top;

            Console.WriteLine("Window Width : " + WindowWidth);
            Console.WriteLine("Window Heigh : " + WindowHeigh);

            return new Rectangle(0, 0, WindowWidth, WindowHeigh);
        }


        /// <summary>
        /// Move a window to specific coordinates
        /// </summary>
        /// <param name="MyMovingWindow">The window we want to move</param>
        private void MoveWindow(Rectangle MyMovingWindow)
        {
            if (MyMovingProcess != null)
            {
                Console.WriteLine("Window X position : " + MyMovingWindow.X);
                Console.WriteLine("Window Y position : " + MyMovingWindow.Y);
                WinAPI.MoveWindow(MyMovingProcess.MainWindowHandle, MyMovingWindow.X, MyMovingWindow.Y, MyMovingWindow.Width, MyMovingWindow.Height, true);
            }
        }


        /// <summary>
        /// Generate window coordinates for center a window in screen
        /// </summary>
        /// <param name="ScreenIdentifier">Identifier of the screen</param>
        /// <param name="MyMovinWindow">The window we want to move</param>
        /// <returns>Return a Rectangle object corresponding to the window final position</returns>
        private Rectangle CreateCoordinates(int ScreenIdentifier, Rectangle MyMovinWindow)
        {
            foreach (Screen item in Screen.AllScreens)
            {
                //if the screen name ends with the screen identifier
                if (item.DeviceName.EndsWith(ScreenIdentifier.ToString()))
                {
                    //get coordinates and sizes (width/heigh) like a rectangle
                    Rectangle ScreenRectangle = item.Bounds;


                    //calc window's final coordinates in the center of the screen
                    int WindowXPos = ScreenRectangle.X + ((ScreenRectangle.Width - MyMovinWindow.Width) / 2);
                    int WindowYPos = ScreenRectangle.Y + ((ScreenRectangle.Height - MyMovinWindow.Height) / 2);

                    Console.WriteLine("Create Window X position : " + WindowXPos);
                    Console.WriteLine("Create Window Y position : " + WindowYPos);

                    return new Rectangle(WindowXPos, WindowYPos, MyMovinWindow.Width, MyMovinWindow.Height);
                }
            }
            return new Rectangle();
        }


        /// <summary>
        /// Wait before execute the next statement
        /// </summary>
        /// <param name="Time">Waiting time in seconds</param>
        private void WaitTime(float Time)
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
