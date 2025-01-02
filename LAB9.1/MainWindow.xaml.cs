using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using lab09._2;
using System.Windows.Media;
using Newtonsoft.Json.Linq;

namespace LAB9._1
{
    public partial class MainWindow : Window //Реализация бэкенда
    {
        public MainWindow()
        {
            InitializeComponent();
            GetStrings();
        }
        
        public struct City //Структура города для парсинга
        {
            public City(string name, string fst, string scnd) { Name = name; FirstNum = fst; SecondNum = scnd; }
            public string Name { get; set; }
            public string FirstNum { get; set; }
            public string SecondNum { get; set; }
            public override string ToString() //Это на случай, если мы хотим распарсить конкретный город
            {
                return $"{Name} {FirstNum} {SecondNum}";
            }
        }
        public struct Weather //Описание погоды и ее парсинг через ToString
        {
            private string _country, _name, _description;
            private double _temp;
            public Weather(string country, string name, double temp, string description)
            {
                _country = country;
                _name = name;
                _temp = temp;
                _description = description;
            }
            public string Country { get { return _country; } }
            public double Temp { get { return _temp; } }
            public string Name { get { return _name; } }
            public string Description { get { return _description; } }
            public override string ToString()
            {
                return $"{Country} {Name} {Temp} {Description}";
            }
        }
        async Task<string> GetHtmlContent(string par1, string par2) //Асинхронное получение ответа от API
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={par1}&lon={par2}&appid=d4d22197d1d2a362c3858afb344920ad");
            return await client.GetStringAsync(client.BaseAddress);
        }
        async Task<List<Weather>> GetStrings() //Получаем строки городов, потом пихаем их в массив строк, парсим и потом пихаем в myListBox для обзора в GUI
        {
            string city = "C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\LAB9.1\\city.txt";
            string[] cities = File.ReadAllLines(city);
            string list = "";
            List<Weather> result = new List<Weather>();
            string name;
            foreach (string c in cities)
            {
                string[] line = c.Split('\t');
                name = line[0];
                string[] nums = line[1].Split(',');
                City town = new City(name, nums[0].Replace(" ", ""), nums[1].Replace(" ", ""));
                list = town.ToString();
                ListBoxItem item = new ListBoxItem();
                item.Content = list;
                myListBox.Items.Add(item);
            }
            return result;
        }
        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e) //Реализация кнопки получения погоды
        {
            if (myListBox.SelectedItem == null) { MessageBox.Show("Выберите город"); return; }
            string selectedCityName = myListBox.SelectedItem.ToString();
            string[] parts = selectedCityName.Split(' ');
            string html = await GetHtmlContent(parts[2], parts[3]);
            JObject jObject = JObject.Parse(html);
            Weather weather = new Weather(jObject["sys"]["country"].ToString(), jObject["name"].ToString(), Convert.ToDouble(jObject["main"]["temp"]), jObject["weather"][0]["description"].ToString());
            MessageBox.Show($"Текущая погода в городе {parts[1]}: Темпуратура = {weather.Temp - 274}; Описание = {weather.Description}");

        }
    }
}

