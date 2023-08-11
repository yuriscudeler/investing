using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CoreLib
{
    public interface IFileLogger
    {
        void LogInfo(string content);
        void LogWarning(string content);
        void LogError(string content);
        void AppendToFile(string content, string filePath);
        void WriteFile(string content, string filePath);
    }

    public class FileLogger : IFileLogger
    {
        private IConfiguration _configuration;
        private string LogDirectory => _configuration.GetValue<string>("StockService:LogDirectory");

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void LogError(string content)
        {
            Log($"[ERROR] {content}", LogDirectory);
        }

        public void LogInfo(string content)
        {
            Log($"[INFO] {content}", LogDirectory);
        }

        public void LogWarning(string content)
        {
            Log($"[WARNING] {content}", LogDirectory);
        }

        private void Log(string content, string basePath)
        {
            var consoleLog = _configuration.GetValue<bool?>("Logging:Console");
            if (consoleLog.HasValue && consoleLog.Value)
            {
                Console.WriteLine(content);
            }
            var date = DateTimeOffset.Now.Date.ToString("yyyy-MM-dd");
            var filepath = $"{basePath}{date}.txt";
            AppendToFile(content, filepath);
        }

        public void AppendToFile(string content, string filePath)
        {
            CreateFile(filePath);
            using StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine(content);
        }

        public void WriteFile(string content, string filePath)
        {
            CreateFile(filePath);
            File.WriteAllText(filePath, content);
        }

        private void CreateFile(string filePath)
        {
            string directory = filePath.Substring(0, filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Close();
            }
        }
    }
}
