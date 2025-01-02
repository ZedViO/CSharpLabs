using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LabEleven {
    public class ApplicationContext : DbContext
    {
        public DbSet<Ticker> Tickers { get; set; } = null!;
        public DbSet<Price> Prices { get; set; } = null!;
        public DbSet<TodaysCondition> TodaysConditions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TestDB;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Ticker>().HasKey(u => new {u.id, u.name });
        modelBuilder.Entity<Price>().HasOne(p => p.ticker).WithMany(q => q.Prices)
            .HasForeignKey(r => new { r.tickerId, r.tickerName });
        modelBuilder.Entity<TodaysCondition>().HasOne(p => p.ticker).WithOne(q => q.Condition)
            .HasForeignKey<TodaysCondition>(e => new { e.tickerId, e.tickerName });
        }
    }

    public class Ticker
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Price> Prices { get; set; } = new();
        public TodaysCondition Condition { get; set; } = new();
    }

    public class Price
    {
        public int id { get; set; }
        public int tickerId { get; set; }
        public string tickerName { get; set; }
        public Ticker? ticker { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
    }

    public class TodaysCondition
    {
        public int id { get; set; }
        public int tickerId { get; set; }
        public string tickerName { get; set; }
        public Ticker ticker { get; set; }
        public decimal state { get; set; }

        public static double GetPriceByTicker(string ticker)
        {
            using (var context = new ApplicationContext())
            {
                var tickerName = context.Tickers
                    .Where(t => t.name == ticker)
                    .Select(t => t.name)
                    .FirstOrDefault();

            
                if (tickerName == ticker)
                {
                    Console.WriteLine($"Тикер Name: {tickerName}");

                    var pricesForTicker = context.Prices
                        .Where(p => p.tickerName == tickerName)
                        .OrderBy(p => p.date)
                        .Select(p => new { p.date, p.price })
                        .ToList();

                    Console.WriteLine($"Найдено {pricesForTicker.Count} цен для тикера {ticker}");


                    if (pricesForTicker.Any())
                    {
                        Console.WriteLine($"Цены для тикера {ticker}:");
                        foreach (var price in pricesForTicker)
                        {
                            Console.WriteLine($"Дата: {price.date}, Цена: {price.price}");
                            return Convert.ToDouble(price.price);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Для тикера {ticker} нет цен в базе данных.");
                    }
                }
                else
                {
                    Console.WriteLine($"Тикер {ticker} не найден в базе данных.");
                }
            }

            return -1;
        }
    }


    class Program
    {
        static async Task Main(string[] args) //асинхронно вызываем
        {
            _ = StartServerAsync(); //вызов сервера
            await StartClientAsync(); //вызов клиента 
        }

        static async Task StartServerAsync()
        {
            TcpListener server = null; //создаем прослушку сервер
            try
            {
                server = new TcpListener(IPAddress.Any, 12345); //слушаем на текущем айпи и порте
                server.Start(); //старт прослушки
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true) //бесконечно принимаем сигнал
                { 
                    TcpClient client = await server.AcceptTcpClientAsync(); 
                    _ = HandleClientAsync(client); //управление клиентом
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сервера: {ex.Message}");
            }
            finally
            {
                server?.Stop();
            }
        }

        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024]; //буфер для потока данных
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0) //пока идут данные, читаем
                    {
                        string tickerRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead); //принимаем байты
                        Console.WriteLine($"Получено от клиента: {tickerRequest}");

                        double stockPrice = TodaysCondition.GetPriceByTicker(tickerRequest); //для тикера ищем цену

                        string response = $"Цена акции ({tickerRequest}): {stockPrice}"; //кидаем ответ
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        await stream.WriteAsync(responseData, 0, responseData.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки клиента: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Клиент отключен.");
            }
        }

        static async Task StartClientAsync()
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 12345);
                Console.WriteLine("Подключено к серверу.");

                using (NetworkStream stream = client.GetStream())
                {
                    Console.Write("Введите тикер акции: ");
                    string tickerToSend = Console.ReadLine();

                    byte[] request = Encoding.UTF8.GetBytes(tickerToSend); //кидаем ответ
                    await stream.WriteAsync(request, 0, request.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead); //получаем ответ

                    Console.WriteLine($"Ответ от сервера: {response}");
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка клиента: {ex.Message}");
            }
        }
    }
}


