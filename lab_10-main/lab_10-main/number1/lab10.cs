using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TicketsLibrary;

class lab10
{
    static async Task Main()
    {
        async Task FeelDB()
        {
            using (StreamReader datar = new StreamReader("C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\lab_10-main\\lab_10-main\\ticker.txt"))
            {
                List<string> data = new List<string>();
                while (!datar.EndOfStream)
                {
                    string? line1 = datar.ReadLine();
                    data.Add(line1);
                }

                await Task.WhenAll(data.ConvertAll(PrintAsync));
            }

            async Task PrintAsync(string data)
            {
                string sURL =
                    $"https://query1.finance.yahoo.com/v7/finance/download/{data}?period1=1629072000&period2=1660608000&interval=1d&events=history&includeAdjustedClose=true";

                using (HttpClient httpClient = new HttpClient())
                using (HttpResponseMessage response = await httpClient.GetAsync(sURL))
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader sr1 = new StreamReader(stream))
                {
                    string data1 = sr1.ReadToEnd();

                    if (!data1.Contains("404 Not Found:"))
                    {
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            Ticker A = new Ticker() { name = data };
                            await db.Tickers.AddAsync(A);
                            List<string> days = new List<string>(data1.Split('\n'));
                            days.RemoveAt(0);
                            List<Price> temp = new List<Price>();
                            foreach (var VARIABLE in days)
                            {
                                string[] main = VARIABLE.Split(',');
                                decimal CurrentPrice;
                                decimal t1, t2;
                                if (decimal.TryParse(main[2], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out t1) && decimal.TryParse(main[3], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out t2))
                                {
                                    CurrentPrice = (t1 + t2) / 2;
                                }
                                else
                                {
                                    CurrentPrice = 0;
                                }
                                DateTime CurrentDate = DateTime.Parse(main[0]);
                                Price B = new Price() { price = CurrentPrice, date = CurrentDate, ticker = A };
                                temp.Add(B);
                            }

                            List<string> day1 = new List<string>(days[days.Count - 1].Split(','));
                            decimal day1price;
                            if (decimal.TryParse(day1[2], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out decimal t11) && decimal.TryParse(day1[3], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out decimal t22))
                            {
                                day1price = (t11 + t22) / 2;
                            }
                            else
                            {
                                day1price = 0;
                            }
                            List<string> day2 = new List<string>(days[days.Count - 2].Split(','));
                            decimal day2price;
                            if (decimal.TryParse(day2[2], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out decimal t111) && decimal.TryParse(day2[3], NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out decimal t222))
                            {
                                day2price = (t111 + t222) / 2;
                            }
                            else
                            {
                                day2price = 0;
                            }
                            decimal Change = day1price - day2price;
                            TodaysCondition C = new TodaysCondition() { ticker = A, state = Change };
                            await db.TodaysConditions.AddAsync(C);
                            await db.Prices.AddRangeAsync(temp);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
        }
  
        FeelDB(); 
        Console.WriteLine("Enter ticker: ");
        string ans = Console.ReadLine();

        using (ApplicationContext db = new ApplicationContext())
        {
            Ticker? ticker = db.Tickers.FirstOrDefault(p => p.name == ans);
            if (ticker != null)
            {
                Console.WriteLine($"Жестко нашли {ans} тикер");
                db.TodaysConditions.Where(u => u.tickerId == ticker.id).Load();
                Console.WriteLine($"\n{ticker.name}:");
                if (ticker.Condition.state > 0)
                {
                    Console.WriteLine($"Stock rose by {ticker.Condition.state}" + '$');
                }
                else if (ticker.Condition.state < 0)
                {
                    Console.WriteLine($"Stock fell by {Math.Abs(ticker.Condition.state)}" + '$');
                }
                else
                {
                    Console.WriteLine("Share price has not changed");
                }
            }
        }
    }
}

