using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreLib
{
    public class FileIO
    {
        public static void WriteLog(string content, string basePath)
        {
            string filepath = basePath + "logs/ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            AppendToFile(content, filepath);
        }

        private static void CreateFile(string filePath)
        {
            string directory = filePath.Substring(0, filePath.LastIndexOf('/') + 1);
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

        public static void AppendToFile(string content, string filePath)
        {
            CreateFile(filePath);

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(content);
            }
        }

        public static void WriteFile(string content, string filePath)
        {
            CreateFile(filePath);
            File.WriteAllText(filePath, content);
        }

        public static string ReadFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
