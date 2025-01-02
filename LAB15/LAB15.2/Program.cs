using System;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Runtime.ConstrainedExecution;

namespace LAB15._2
{
    public interface ILoggerRepository
    {
        void LogText(string text);
        void LogJson(object data);

        public class FileLoggerRepository : ILoggerRepository
        {
            private string textFilePath;
            private string jsonFilePath;

            public FileLoggerRepository(string textPath, string jsonPath)
            {
                textFilePath = textPath;
                jsonFilePath = jsonPath;
            }

            public void LogText(string text)
            {
                using (StreamWriter writer = new StreamWriter(textFilePath, true))
                {
                    writer.WriteLine(text);
                }
            }

            public void LogJson(object data)
            {
                string json = JsonSerializer.Serialize(data);

                using (StreamWriter writer = new StreamWriter(jsonFilePath, true))
                {
                    writer.WriteLine(json);
                }
            }
        }

        public class MyLogger
        {
            private ILoggerRepository repository;

            public MyLogger(ILoggerRepository repo)
            {
                repository = repo;
            }

            public void LogText(string text)
            {
                repository.LogText(text);
            }

            public void LogJson(object data)
            {
                repository.LogJson(data);
            }
        }

        public class Program
        {
            public static void Main(string[] args)
            {
                string textFilePath = "C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\TD.txt";
                string jsonFilePath = "C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\json1.json";

                ILoggerRepository repository = new FileLoggerRepository(textFilePath, jsonFilePath);
                MyLogger logger = new MyLogger(repository);

                logger.LogText("This is a text log entry");
                logger.LogJson(new { Name = "John", Age = 30 });

                Console.WriteLine("Logs have been written to files.");
            }
        }
    }
}