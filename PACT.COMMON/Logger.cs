using System;
using System.Collections;
using System.IO;
using System.Threading;


namespace PACT.COMMON
{
    public class Logger
    {
        private const int _DEBUG = 1;
        private const int _INFO = 2;
        private const int _WARN = 3;
        private const int _ERROR = 4;
        private const string DEBUG = "DEBUG";
        private const string INFO = "INFO";
        private const string WARN = "WARN";
        private const string ERROR = "ERROR";

        private static String logDirectory = GetLogFilePath();
        private static String filePath = null;
        private static Logger _Instance = null;
        private static volatile object _Lock = new object();
        private static ArrayList logLevel = null; // holds the configuration keys 
        private static bool isConfigurationKeyExists = true;

        public static Logger Instance()
        {
            if (_Instance == null)
            {
                lock (_Lock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new Logger();
                        // Create the loglevel array,which contains the log levels defined in the cofiguration file.
                        // If the data is missing in the config file, we throw an exception.
                        try
                        {
                            logLevel = new ArrayList(System.Configuration.ConfigurationManager.AppSettings["LogLevel"].Split(','));
                        }
                        catch (System.NullReferenceException e)
                        {
                            //Since the key is not present we need to log that fact
                            //and set the boolean flag to false, preventing any further logging.
                            isConfigurationKeyExists = false;
                            writeToLogFile(ERROR, e.Message + ": " + "Could not find Log information in configuration file");
                        }
                        catch (System.Exception e)
                        {
                            //Since the key is not present we need to log that fact
                            //and set the boolean flag to false, preventing any further logging.
                            isConfigurationKeyExists = false;
                            writeToLogFile(ERROR, e.Message);
                        }
                    }
                }
            }
            return _Instance;
        }

        private Logger()
        {
            string tempfile = "";
            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }

            filePath = logDirectory + "\\PACTLOGS.log";
            if (File.Exists(filePath))
            {
                tempfile = logDirectory + "\\PACTLOG." + File.GetCreationTime(filePath).ToString("yyyyMMddHHmmss") + ".log";
                File.Move(filePath, tempfile);
            }

            if (!File.Exists(filePath))
            {
                lock (_Lock)
                {
                    if (!File.Exists(filePath))
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                        File.SetCreationTime(filePath, System.DateTime.Now);
                    }
                }
            }
        }

        public static void DebugLog(string message)
        {
            logMessage(_DEBUG, message);
        }

        public static void DebugLog(string classname, string methodname, string message)
        {
            logMessage(_DEBUG, classname + "|" + methodname + "|" + message);
        }

        public static void InfoLog(string message)
        {
            logMessage(_INFO, message);
        }

        public static void InfoLog(string classname, string methodname, string message)
        {
            logMessage(_INFO, classname + "|" + methodname + "|" + message);
        }

        public static void WarnLog(string message)
        {
            logMessage(_WARN, message);
        }
        public static void WarnLog(string message, Exception excp)
        {
            logMessage(_WARN, message + "\n" + excp.Message + "\n" + excp.StackTrace);
        }
        public static void ErrorLog(string message)
        {
            System.Diagnostics.Debug.Assert(message.Length == 0);
            logMessage(_ERROR, message);
        }

        public static void ErrorLog(string classname, string methodname, string message)
        {
            System.Diagnostics.Debug.Assert(message.Length == 0);
            logMessage(_ERROR, classname + "|" + methodname + "|" + message);
        }

        public static void ErrorLog(string message, Exception excp)
        {
            System.Diagnostics.Debug.Assert(excp.Message.Length == 0);
            logMessage(_ERROR, message + "\n" + excp.Message + "\n" + excp.StackTrace);
        }

        public static void ErrorLog(string classname, string methodname, string message, Exception excp)
        {
            System.Diagnostics.Debug.Assert(excp.Message.Length == 0);
            logMessage(_ERROR, classname + "|" + methodname + "|" + message + "\n" + excp.Message + "\n" + excp.StackTrace);
        }

        private static void logMessage(int level, String messageToLog)
        {
            string levelStr = string.Empty;

            switch (level)
            {
                case _DEBUG:
                    levelStr = DEBUG;
                    break;
                case _INFO:
                    levelStr = INFO;
                    break;
                case _WARN:
                    levelStr = WARN;
                    break;
                case _ERROR:
                    levelStr = ERROR;
                    break;
            }

            // Due to a failure while getting the Logging configuration information, we are defaulting to log everything.
            // This will make the application slow, but is the desired behavior.
            if (isConfigurationKeyExists == false)
            {
                if (logLevel == null)
                {
                    logLevel = new ArrayList();
                    logLevel.Add(DEBUG);
                    logLevel.Add(INFO);
                    logLevel.Add(WARN);
                    logLevel.Add(ERROR);
                    // We set this flag to true to avoid running an extra if statement if (logLevel == null )
                    isConfigurationKeyExists = true;
                }
            }
            //Check to see if we need to log the msg, based on the log level defined in the configuration file.          
            if (logLevel != null)
            {
                if (logLevel.Contains(levelStr) == true)
                    writeToLogFile(levelStr, messageToLog);
            }
        }
        /// <summary>
        /// This function performs the actual write to the log file.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private static void writeToLogFile(string levelStr, string messageToLog)
        {
            DateTime dt1 = DateTime.Now;

            try
            {

                lock (_Lock)
                {
                    StreamWriter sw = File.AppendText(filePath);
                    sw.WriteLine("[Thread: " + Thread.CurrentThread.GetHashCode() + "] " + levelStr
                        + " | " + dt1.ToString("MM/dd/yyyy HH:mm:ss.fff") + " | " + messageToLog);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        private static string GetLogFilePath()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            filePath += System.Configuration.ConfigurationManager.AppSettings["LogFilePath"];

            return filePath;
        }

    }
}
