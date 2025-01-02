using LAB7;
using System.Xml.Serialization;
namespace LAB8
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Процесс создания объекта и его сериализации
            Animal lion = new Lion("Russia", true, "Luzius", "Lion");
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            FileStream stream = new FileStream("C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\LAB8\\Lion.xml", FileMode.Create);
            serializer.Serialize(stream, lion);
            stream.Close();

            //Процесс десериализации и выведения объекта на консоль
            stream = new FileStream("C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\LAB8\\Lion.xml", FileMode.Open);
            Animal deserializedLion = (Animal)serializer.Deserialize(stream);
            stream.Close();

            Console.WriteLine($"DLion's name = {deserializedLion.Name}\n" +
                              $"DLion's country = {deserializedLion.Country}\n" +
                              $"IsHidingFromAnimals? = {deserializedLion.HideFromOtherAnimals}\n" +
                              $"What type this animal is? = {deserializedLion.WhatAnimal}");
        }
    }
}
