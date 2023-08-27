using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private string _logDirectory => _configuration.GetValue<string>("StockService:LogDirectory");

        public FileLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void LogError(string content)
        {
            Log(LogLevel.Error, content);
        }

        public void LogInfo(string content)
        {
            Log(LogLevel.Information, content);
        }

        public void LogWarning(string content)
        {
            Log(LogLevel.Warning, content);
        }

        private void Log(LogLevel logLevel, string content)
        {
            var now = DateTimeOffset.Now;
            // [TIMESTAMP] [LEVEL] Content

            // set level
            if (logLevel == LogLevel.Information) content = $"[INFO] {content}";
            else if (logLevel == LogLevel.Error) content = $"[ERROR] {content}";
            else if (logLevel == LogLevel.Warning) content = $"[WARNING] {content}";

            // set timestamp
            var timestamp = now.ToString("HH:mm:ss");
            content = $"[{timestamp}] {content}";

            // log to console
            var consoleLog = _configuration.GetValue<bool?>("Logging:Console");
            if (consoleLog.HasValue && consoleLog.Value)
            {
                Console.WriteLine(content);
            }
            
            var date = now.Date.ToString("yyyy-MM-dd");
            var filepath = $"{_logDirectory}{date}.txt";
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
