using System.Diagnostics;
using System.Net.Mail;

namespace Email_Parser
{
    internal class Logger
    {
        public static int Num = 0;
        public static string[] LogArray;
        public static FileInfo LogFile;
        public static FileInfo[] LogFiles;
        public static string LoggerDirectory;
        public static string LoggerFileName;
        public static string LoggerFilePath;
        public static string AppName = EventLogger.Settings.Default.LogFileName;
        public static string LogDirectory = EventLogger.Settings.Default.LogDir;
        public static short ErrorCount = 0;

        public static string LastErrorMessage = "";

        public enum LoggerType
        {
            Error,
            Warning,
            Success,
            Workaround
        }

        public Logger()
        {
            LoggerDirectory = LogDirectory;
            if (LogDirectory == "")
            {
                LoggerDirectory = Directory.GetCurrentDirectory();
            }

            LoggerFileName = $"{AppName}_{DateTime.Now.Date.ToString("dd-MM-yyyy")}_{Num}";
            LoggerFilePath = $@"{LoggerDirectory}\{LoggerFileName}.log";

            if (File.Exists(LoggerFilePath))
            {
                LogFile = new FileInfo(LoggerFilePath);
                LogFiles = LogFile.Directory.GetFiles("*" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "*");

                LogCheck(LogFile, LogFiles);
            }
            else
            {
                File.Create(LoggerFilePath).Close();
            }
        }

        public void LogEvent(string message, LoggerType Loggertype)
        {
            lock (this)
            {
                try
                {
                    LogFile = new FileInfo(LoggerFilePath);
                    LogFiles = LogFile.Directory.GetFiles("*" + DateTime.Now.Date.ToString("dd-MM-yyyy") + "*");

                    LogCheck(LogFile, LogFiles);

                    if (LogFile.Length > 5242880 && LogFile.Exists)
                    {
                        Num++;
                        LoggerFileName = $"{AppName}_{DateTime.Now.Date.ToString("dd-MM-yyyy")}_{Num}";
                        LoggerFilePath = $@"{LoggerDirectory}\{LoggerFileName}.log";
                    }
                    else if (!LogFile.Exists)
                    {
                        Num = 0;
                    }

                    using (StreamWriter w = File.AppendText(LoggerFilePath))
                    {
                        w.WriteLine($"{DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss")} | {Loggertype} | {Process.GetCurrentProcess().Threads.Count} | {message} ");
                        w.Close();
                    }

                    // Use SMTP Email Client function to send a Log email.

                    if (Loggertype == LoggerType.Error && message != LastErrorMessage && ErrorCount <=3 )
                    {
                        LastErrorMessage = message;

                        MailMessage msg = new MailMessage();
                        msg.Body = message;
                        msg.Subject = $"{AppName} {Loggertype}";                 

                        // SMTPLogSend(msg);
                    }

                }
                catch (FileNotFoundException ex)
                {
                    
                    LogEvent($"{ex.Message}", LoggerType.Error);
                }
                catch (UnauthorizedAccessException ex)
                {
                    
                    LogEvent($"{ex.Message}", LoggerType.Error);

                }
                catch (Exception ex)
                {
                    LogEvent($"{ex.Message}", LoggerType.Error);
                }
            }
        }
        public void LogCheck(FileInfo logFile, FileInfo[] logFiles)
        {
            if (logFile.Length != 0 && logFile.Exists)
            {
                FileInfo file;
                if (logFiles.Length != 0)
                {
                    file = logFiles[logFiles.Length - 1];
                    LogArray = file.Name.ToString().Split('_', '.');
                    Int32.TryParse(LogArray[2], out Num);
                    LoggerFileName = $"{AppName}_{DateTime.Now.Date.ToString("dd-MM-yyyy")}_{Num}";
                    LoggerFilePath = $@"{LoggerDirectory}\{LoggerFileName}.log";

                }
                else
                {
                    Num = 0;
                    LoggerFileName = $"{AppName}_{DateTime.Now.Date.ToString("dd-MM-yyyy")}_{Num}";
                    LoggerFilePath = $@"{LoggerDirectory}\{LoggerFileName}.log";
                    File.Create(LoggerFilePath).Close();
                }
                LogFile = new FileInfo(LoggerFilePath);

            }
        }
    }
}




