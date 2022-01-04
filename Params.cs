using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveWindow
{
    /// <summary>
    /// Manage application parameters
    /// </summary>
    class Params
    {
        /// <summary>Name of the main window</summary>
        public string WindowName = "";
        /// <summary>X position of the high-left corner</summary>
        public int XPos = 0;
        /// <summary>Y position of the high-left corner</summary>
        public int YPos = 0;
        /// <summary>Time in seconds before moving the app</summary>
        public int Timer = 0;
        /// <summary>Debug Mode</summary>
        public bool DebugMode = false;
        /// <summary>Path to the program for execute it</summary>
        public string AppPath = "";
        /// <summary>Program parameters</summary>
        public string AppParameters = "";
        /// <summary>Program working directory</summary>
        public string AppWorkingDirectory = "";


        /// <summary>
        /// Get all parameters
        /// Return all parameters in individual propriety
        /// </summary>
        /// <param name="parameters">Array of app parameters</param>
        public void GetParameters(string[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == "-w")
                {
                    WindowName = parameters[i + 1];
                }

                if (parameters[i] == "-x")
                {
                    XPos = Int32.Parse(parameters[i + 1]);
                }

                if (parameters[i] == "-y")
                {
                    YPos = Int32.Parse(parameters[i + 1]);
                }

                if (parameters[i] == "-t")
                {
                    Timer = Int32.Parse(parameters[i + 1]);
                }

                if (parameters[i] == "-c")
                {
                    AppPath = parameters[i + 1];
                }

                if (parameters[i] == "-p")
                {
                    AppParameters = parameters[i + 1];
                }

                if (parameters[i] == "-wd")
                {
                    AppWorkingDirectory = parameters[i + 1];
                }

                if (parameters[i] == "-d")
                {
                    DebugMode = Boolean.Parse(parameters[i + 1]);
                }

                if (parameters[i] == "-h")
                {
                    DisplayHelp();
                }
            }
        }


        /// <summary>
        /// Display help
        /// </summary>
        private void DisplayHelp()
        {
            string[] lines = {  "Syntax : MoveWindow.exe -w WindowName -x 42 -y 42",
                                @"MoveWindow.exe -c C:\Windows\SysWOW64\win32calc.exe -x 42 -y 42",
                                "",
                                "Description of application parameters",
                                "",
                                "-w : Name of the main window",
                                "-x : X position of the high - left corner",
                                "-y : Y position of the high - left corner",
                                "-t : Time in seconds before moving the app (optionnal)",
                                "-c : Path to the program for execute it",
                                "-p : Program parameters. Use it with - c (optionnal)",
                                "-wd: Program Working directory (optionnal)",
                                "-d : Debug Mode (optionnal)",
                                "-h : Get help (optionnal)" };

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
