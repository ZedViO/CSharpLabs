using Microsoft.VisualBasic.FileIO;
using System.Net;
using System.Text;

namespace LAB9
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            string ticker = "C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\LAB9\\ticker.txt"; //Путь до листа с тикерами
            string[] codes = await File.ReadAllLinesAsync(ticker); //Асинхронное считывание тикеров
            SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); //Аналог mutex - блокирует доступ к потоку и разрешает только один доступ к защищенному ресурсу
            await LoadData(codes, semaphore); 
        }

        public static async Task MiddleCounter(string code, SemaphoreSlim semaphore) //Счетчик усредненных значений (code - строка, представляющая тикер)
        {
            UnicodeEncoding encoding = new UnicodeEncoding(); //Юникод кодировщик в файле result.txt
            double mid = 0; //Средние значения
            int days = 0;   //Число дней
            using (TextFieldParser parser = new TextFieldParser($"{code}.csv")) //Считывает информацию о тикерах из CSV файла
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(","); //Установка разделительного знака
                while (!parser.EndOfData) //Считываем до конца информацию в парсере
                {
                    string[] fields = parser.ReadFields(); //Берем построчно
                    if (fields[1] == "null") break; //При существовании значений для тикеров и существовании даты меняем , на . и суммируем минимум с максимумом и делим на 2
                    if (fields[0] != "Date")
                    {
                        mid += (Convert.ToDouble(fields[1].Replace('.', ',')) + Convert.ToDouble(fields[2].Replace('.', ','))) / 2;
                        ++days; //инкрементируем количество дней
                    }
                }
                await semaphore.WaitAsync(); //В отдельный поток выносим операцию 
                try
                {
                    using (FileStream stream = File.Open("result.txt", FileMode.Append, FileAccess.Write)) //Открываем файл для записи (тикер и среднее)
                    {
                        using (StreamWriter writer = new StreamWriter(stream, encoding))
                        {
                            await writer.WriteAsync(code + ": " + (mid / days) + "\n");
                        }
                        Console.WriteLine("count");
                    }
                }
                finally
                {
                    semaphore.Release(); //Освобождаем поток
                }
            }
        }

        public static async Task LoadData(string[] codes, SemaphoreSlim semaphore) //Загрузчик тикеров
        {
            long now = DateTimeOffset.Now.ToUnixTimeSeconds();
            long year_ago = now - 31556926;
            string url;
            WebClient client = new WebClient();
            foreach (string code in codes)
            {
                //Запрос в соответствии с кодом тикера
                url = $"https://query1.finance.yahoo.com/v7/finance/download/{code}?period1={year_ago}&period2={now}&interval=1d&events=history&includeAdjustedClose=true";
                Console.WriteLine(code);
                await semaphore.WaitAsync();
                try
                {
                    await client.DownloadFileTaskAsync(new Uri(url), $"{code}.csv"); //Загружаем CSV файлы с тикерами
                }
                catch (System.Net.WebException)
                {
                    Console.WriteLine("not added"); //Если тикер не найден, то вызываем исключение
                }
                finally
                {
                    semaphore.Release();                        //Освобождаем поток
                    await MiddleCounter(code, semaphore);       //Вычисляем среднее
                }
            }
        }
    }
}
