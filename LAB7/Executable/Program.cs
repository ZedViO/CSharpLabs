using LAB7;
using System.Xml.Serialization;

namespace Executable
{
    public class Program
    {
        static void Main(string[] args)
        {
            Lion lion = new Lion("Canada", false, "Ehrardt", "Lion");
            Pig pig = new Pig("Poland", true, "Hedwig", "Pig");
            Cow cow = new Cow("Russia", true, "Zor'ka", "Cow");

            List<Animal> animals = new List<Animal>();
            animals.Add(pig);
            animals.Add(cow);
            animals.Add(lion);


            XmlSerializer serializer = new XmlSerializer(typeof(List<Animal>));
            FileStream stream = new FileStream("C:\\Users\\rybal\\OneDrive\\Рабочий стол\\mein beliebtes Uni\\CBeer\\labscsharp\\LAB7\\Animals.xml", FileMode.Create);
            serializer.Serialize(stream, animals);

        }
    }
}
