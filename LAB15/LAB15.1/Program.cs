using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace LAB15._1
{
    public class DirectoryChangedEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }

    public delegate void DirectoryChangedEventHandler(object sender, DirectoryChangedEventArgs args);

    public class DirectorySystemWatcher
    {
        private string directoryPath;
        public Timer timer;
        private List<string> currentFiles;

        public event DirectoryChangedEventHandler FileChanged;

        public DirectorySystemWatcher(string directoryPath)
        {
            this.directoryPath = directoryPath;
            currentFiles = Directory.GetFiles(directoryPath).ToList();

            timer = new Timer(CheckDirectory, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void CheckDirectory(object state)
        {
            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                if (currentFiles.Contains(file))
                {
                    continue;
                }

                currentFiles.Add(file);

                OnFileChanged(file);
            }

            for (int i = currentFiles.Count - 1; i >= 0; i--)
            {
                var file = currentFiles[i];
                if (!File.Exists(file))
                {
                    currentFiles.RemoveAt(i);
                    OnFileChanged(file);
                }
            }
        }

        protected virtual void OnFileChanged(string fileName)
        {
            FileChanged?.Invoke(this, new DirectoryChangedEventArgs { FileName = fileName });
        }
    }

    public class DirectoryChangeHandler
    {
        public void OnFileChanged(object sender, DirectoryChangedEventArgs args)
        {
            Console.WriteLine($"File {args.FileName} has been changed");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var directorySystemWatcher = new DirectorySystemWatcher("C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp");

            var directoryChangeHandler = new DirectoryChangeHandler();

            directorySystemWatcher.FileChanged += directoryChangeHandler.OnFileChanged;

            while (true)
            {
                directorySystemWatcher.CheckDirectory(directorySystemWatcher.timer);
            }
        }
    }
}