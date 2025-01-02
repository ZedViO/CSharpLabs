using System.IO.Compression;

namespace Searcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь для поиска файла: ");
            string directoryPath = Console.ReadLine() + "";

            Console.Write("Введите имя файла для поиска: ");
            string fileName = Console.ReadLine() + "";

            string[] filePath = SearchFile(directoryPath, fileName);

            if (filePath != null)
            {
                Console.WriteLine($"Файл(ы) найден(ы)! Их {filePath.Length} штук");
                foreach (var file in filePath)
                {
                    Console.WriteLine("Содержимое файла:");
                    DisplayFileContent(file);
                }
                Console.Write("Хотите сжать файл? (y/n): ");
                string compressChoice = Console.ReadLine() + "";

                if (compressChoice.ToLower() == "y")
                {
                    foreach (var file in filePath)
                    {
                        CompressFile(file);
                        Console.WriteLine("Файл успешно сжат.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Файл с именем '{fileName}' не найден в указанной директории и её поддиректориях.");
            }

            static string[] SearchFile(string directoryPath, string fileName)
            {
                string[] files = Directory.GetFiles(directoryPath, fileName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    return files;
                }

                return null;
            }

            static void DisplayFileContent(string filePath)
            {
                FileStream fileStream = File.OpenRead(filePath);
                StreamReader reader = new StreamReader(fileStream);
                Console.WriteLine(reader.ReadToEnd());
            }

            static void CompressFile(string filePath)
            {
                string compressedFilePath = filePath + ".zip";

                using (FileStream originalFileStream = File.OpenRead(filePath))
                using (FileStream compressedFileStream = File.Create(compressedFilePath))
                using (ZipArchive archive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(filePath));

                    using (Stream entryStream = entry.Open())
                    {
                        originalFileStream.CopyTo(entryStream);
                    }
                }
            }
        }
    }
}
