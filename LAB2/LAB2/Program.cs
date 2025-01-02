using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

class Classroom
{
    Pupil[] pupArray = new Pupil[0] {};
    public Classroom(params Pupil[] pupils)
    {
        Array.Resize<Pupil>(ref pupArray, pupils.Length);
        for (ushort i = 0; i < pupils.Length; ++i)
        {
            pupArray[i] = pupils[i];
        }
    }

    public void pupPrint()
    {
        for (ushort i = 0; i < pupArray.Length; ++i)
        {
            Console.WriteLine(pupArray[i].Name);
            pupArray[i].Study();
            pupArray[i].Read();
            pupArray[i].Write();
            pupArray[i].Relax();
            Console.WriteLine();
        }
    }
}

class Pupil
{
    private string name = "";

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public virtual void Study()
    {
        Console.WriteLine("Я учусь как обычный студент");
    }
    public virtual void Read()
    {
        Console.WriteLine("Я читаю как обычный студент");
    }
    public virtual void Write()
    {
        Console.WriteLine("Я пишу как обычный студент");
    }
    public virtual void Relax()
    {
        Console.WriteLine("Я отдыхаю как обычный студент");
    }
}

class BadPupil : Pupil
{
    
    public override void Study()
    {
        Console.WriteLine("Я учусь плохо");
    }
    public override void Read()
    {
        Console.WriteLine("Я читаю мало");
    }
    public override void Write()
    {
        Console.WriteLine("Я пишу ужасно");
    }
    public override void Relax()
    {
        Console.WriteLine("Я сплю беспробудным сном");
    }
}

class GoodPupil : Pupil
{
    public override void Study()
    {
        Console.WriteLine("Я учусь хорошо");
    }
    public override void Read()
    {
        Console.WriteLine("Я читаю много");
    }
    public override void Write()
    {
        Console.WriteLine("Я пишу красиво");
    }
    public override void Relax()
    {
        Console.WriteLine("Я отдыхаю в меру");
    }
}
class ExcellentPupil : Pupil
{
    public override void Study()
    {
        Console.WriteLine("Я учусь на пять");
    }
    public override void Read()
    {
        Console.WriteLine("Я читаю очень много");
    }
    public override void Write()
    {
        Console.WriteLine("Я пишу каллиграфическим почерком");
    }
    public override void Relax()
    {
        Console.WriteLine("Делу время - потехе час");
    }
}

class Vehicle
{
    protected int cost, year, speed, x, y, z;
    protected string name;
    
    public Vehicle(string _name, int _cost, int _year, int _speed, int _x, int _y, int _z)
    {
        name = _name;
        cost = _cost;
        year = _year;
        speed = _speed;
        x = _x;
        y = _y;
        z = _z;
    }

    public virtual void printInfo()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("X: " + x + " Y: " + y + " Z: " + z);
        Console.WriteLine("Year: " + year);
        Console.WriteLine("Cost: " + cost);
        Console.WriteLine("Speed: " + speed);
    }
}

class Plane : Vehicle
{
    private int height, amount;
    public Plane(int _amount, string _name, int _year, int _cost, int _speed, int _x, int _y, int _z) 
        : base(_name, _year, _cost, _speed, _x, _y, _z)
    {
        height = _z;
        amount = _amount;
    }

    public override void printInfo()
    {
        base.printInfo();
        Console.WriteLine("Height: " + height);
        Console.WriteLine("Amount of passagers: " + amount);
    }
}

class Car : Vehicle
{
    public Car(string _name, int _year, int _cost, int _speed, int _x, int _y, int _z)
        : base(_name, _year, _cost, _speed, _x, _y, _z)
    {

    }
}

class Ship : Vehicle
{
    private int amount;
    private string tag;
    public Ship(string _tag, int _amount, string _name, int _year, int _cost, int _speed, int _x, int _y, int _z)
        : base(_name, _year, _cost, _speed, _x, _y, _z)
    {
        tag = _tag;
        amount = _amount;
    }

    public override void printInfo()
    {
        base.printInfo();
        Console.WriteLine("Tag: " + tag);
        Console.WriteLine("Amount of passagers: " + amount);
    }
}

class DocumentWorker
{
    public virtual void OpenDocument()
    {
        Console.WriteLine("Документ открыт");
    }
    public virtual void EditDocument()
    {
        Console.WriteLine("Редактирование документа доступно в версии PRO");
    }
    public virtual void SaveDocument()
    {
        Console.WriteLine("Сохранение документа доступно в версии PRO");
    }
}

class ProDocumentWorker : DocumentWorker
{
    public override void EditDocument()
    {
        Console.WriteLine("Документ отредактирован");
    }
    public override void SaveDocument()
    {
        Console.WriteLine("Документ сохранен в старом формате, сохранение в остальных форматах доступно в версии Expert");
    }
}

class ExpertDocumentWorker : ProDocumentWorker
{
    public override void SaveDocument()
    {
        Console.WriteLine("Документ сохранен в новом формате");
    }
}

class Executable
{
    static void Main(string[] args)
    {
        //Выполнение первой задачи
        Pupil p1 = new Pupil();
        BadPupil p2 = new BadPupil();
        ExcellentPupil p3 = new ExcellentPupil();
        GoodPupil p4 = new GoodPupil();

        p1.Name = "Robert";
        p2.Name = "Stieve";
        p3.Name = "Dan";
        p4.Name = "Leyland";

        Classroom classr = new Classroom(p1, p2, p3, p4);
        classr.pupPrint();
        Console.WriteLine("#################################################");

        //Выполнение второй задачи
        Vehicle car = new Car("БМПТ Терминатор", 2009, 13000000, 65, 143, 45, 2);
        Vehicle plane = new Plane(1, "Су 57", 223600000, 2010, 1800, 423, 54, 200);
        Vehicle ship = new Ship("Феодосия", 30, "Атлант", 2003, 500000, 70, 30, 123, 10);

        car.printInfo();
        Console.WriteLine();
        plane.printInfo();
        Console.WriteLine();
        ship.printInfo();
        Console.WriteLine("\n#################################################");

        //Выполнение третьей задачи
        DocumentWorker worker;

        Console.WriteLine("Type:\n\tNothing for default version\n\tP for pro version\n\tE for expert version\n");
        string input = Console.ReadLine();
        switch(input)
        {
            case "P":
                Console.WriteLine("Создаем документ версии PRO");
                worker = new ProDocumentWorker();
                break;
            case "E":
                Console.WriteLine("Создаем документ версии Expert");
                worker = new ExpertDocumentWorker();
                break;
            default:
                Console.WriteLine("Создаем бесплатный документ");
                worker = new DocumentWorker();
                break;
        }

        worker.OpenDocument();
        worker.EditDocument();
        worker.SaveDocument();
        Console.WriteLine("#################################################");

    }
}