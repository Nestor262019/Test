using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNestorCanales
{
    public class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool LogToDatabase;

        public JobLogger (bool logToFile, bool logToConsole, bool logToDatabase, bool
        logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            LogToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }
        
        public static void LogMessage(string message, bool message2, bool warning, bool error)
        {
            message.Trim();
            if (message == null || message.Length == 0)
            {
                return;
            }
            if (!_logToConsole && !_logToFile && !LogToDatabase)
            {
                throw new Exception("Invalid configuration!");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!message2 && !warning
            && !error))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            
            try
            {               
                int t = 0;

                if (message2 && _logMessage)
                    t = 1;
                
                if (error && _logError)                
                    t = 2;
                
                if (warning && _logWarning)                
                    t = 3;                

                connection.Open();
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("Insert into Log1 Values('" + message + "', " + t.ToString() + ")", connection);                
                command.ExecuteNonQuery();

                string l = string.Empty;

                if (System.IO.File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + Convert.ToString(DateTime.Now.ToShortDateString()).Replace("/", "") + ".txt"))
                {
                  l = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + Convert.ToString(DateTime.Now.ToShortDateString()).Replace("/", "") + ".txt");
                }                                
                         
                if (error && _logError)
                {
                    l = l + DateTime.Now.ToShortDateString() + " "  + message + "\r\n";
                }
                if (warning && _logWarning)
                {
                    l = l + DateTime.Now.ToShortDateString() + " " + message + "\r\n";
                }
                if (message2 && _logMessage)
                {
                    l = l + DateTime.Now.ToShortDateString() + " " + message + "\r\n";
                }

                System.IO.File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + Convert.ToString(DateTime.Now.ToShortDateString()).Replace("/", "") + ".txt", l);

                if (error && _logError)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (warning && _logWarning)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (message2 && _logMessage)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(DateTime.Now.ToShortDateString() + " " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mensaje de error: " + ex);              
            }
            finally
            {
                connection.Close();
            }            
        }
    }
}
