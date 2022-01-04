using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace MoveWindow
{
    class ScreenWindFcts
    {
        /// <summary>Width of the window</summary>
        private int WindowWidth = 0;
        /// <summary>Heigh of the window</summary>
        private int WindowHeigh = 0;


        /// <summary>
        /// Get process by his window name
        /// </summary>
        /// <param name="WindowName">Name of the main window</param>
        /// <param name="DebugMode">Enable messages in console</param>
        /// <returns>Return a Process object</returns>
        public Process FindWindowByName(string WindowName, bool DebugMode)
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
        public Process StartProgram(string AppPath, string AppParameters, string AppWorkingDirectory)
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
        /// <param name="MyProcess">Process for getting sizes</param>
        /// <returns>Return WindowWidth and WindowHeight</returns>
        private void GetWindowSizes(Process MyProcess)
        {
            // Initialization of the RECT structure
            RECT Wind = new RECT();

            // Get coordinates
            WinAPI.GetWindowRect(MyProcess.MainWindowHandle, out Wind);

            // Calculate
            WindowWidth = Wind.Right - Wind.Left;
            WindowHeigh = Wind.Bottom - Wind.Top;
        }


        /// <summary>
        /// Move a window to specific coordinates
        /// </summary>
        /// <param name="MyProcess">The process we want to move</param>
        /// <param name="XPos">X position</param>
        /// <param name="YPos">Y position</param>
        public void MoveWindow(Process MyProcess, int XPos, int YPos)
        {
            if (MyProcess != null)
            {
                GetWindowSizes(MyProcess);

                // Finally move the window
                WinAPI.MoveWindow(MyProcess.MainWindowHandle, XPos, YPos, WindowWidth, WindowHeigh, true);
            }
        }

    }

}
