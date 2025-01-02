using System.Xml.Linq;

struct Vector
{
    private double x;
    private double y; 
    private double z;

    public Vector (double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    public static Vector operator *(Vector v1, Vector v2)
    {
        return new Vector(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
    public static Vector operator *(Vector v, double c)
    {
        return new Vector(v.x * c, v.y * c, v.z * c);
    }
    public static bool operator ==(Vector v1, Vector v2)
    {
        return Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z) == Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z);
    }
    public static bool operator !=(Vector v1, Vector v2)
    {
        return !(v1 == v2);
    }
    public void PrintVector()
    {
        Console.WriteLine("x = " + x + " y = " + y + " z = " + z);
    }
}

class Car : IEquatable<Car>
{
    private string name, engine;
    private int maxSpeed;
    public Car(string name, string engine, int maxSpeed)
    {
        this.name = name;
        this.engine = engine;
        this.maxSpeed = maxSpeed;
    }
    public override string ToString()
    {
        return name;
    }

    public bool Equals(Car car)
    {
        if (car == null) return false;

        return name == car.name && engine == car.engine && maxSpeed == car.maxSpeed;
    }
}

class CarsCatalog
{
    private Car[] cars = { };

    public CarsCatalog(params Car[] car)
    {
        Array.Resize(ref cars, car.Length);
        for (int i = 0; i < car.Length; ++i)
        {
            cars[i] = car[i];
        }
    }

    public Car this[int index]
    {
        get { return cars[index]; }
        set { cars[index] = value; }
    }
}

class Currency
{
    protected double value;
    protected static double rub_usd, usd_rub, rub_eur, eur_rub, usd_eur, eur_usd;
    public Currency(double val)
    {
        value = val;
    }
    public void PrintCurr()
    {
        Console.WriteLine(value);
    }
}
class CurrencyUSD : Currency
{

    public CurrencyUSD(double val) : base(val) { }

    public static void SetCourse(string curr, double course)
    {
        if (curr == "RUB")
        {
            usd_rub = course;
            rub_usd = Math.Pow(course, -1);
        }

        else if (curr == "EUR")
        {
            usd_eur = course;
            eur_usd = Math.Pow(course, -1);
        }
    }

    public static implicit operator CurrencyEUR(CurrencyUSD usd)
    {
        double rate = GetExchangeRate("USD", "EUR");
        double eurValue = usd.value * rate;
        return new CurrencyEUR(eurValue);
    }

    public static implicit operator CurrencyRUB(CurrencyUSD usd)
    {
        double rate = GetExchangeRate("USD", "RUB");
        double rubValue = usd.value * rate;
        return new CurrencyRUB(rubValue);
    }

    private static double GetExchangeRate(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == "USD" && toCurrency == "EUR") return usd_eur;
        else if (fromCurrency == "USD" && toCurrency == "RUB") return usd_rub;
        else return 0;
    }
}

class CurrencyEUR : Currency
{
    public CurrencyEUR(double val) : base(val) { }

    public static void SetCourse(string curr, double course)
    {
        if (curr == "RUB")
        {
            eur_rub = course;
            rub_eur = Math.Pow(course, -1);
        }

        else if (curr == "USD")
        {
            usd_rub = course;
            rub_usd = Math.Pow(course, -1);
        }
    }

    public static implicit operator CurrencyUSD(CurrencyEUR eur)
    {
        double rate = GetExchangeRate("EUR", "USD");
        double usdValue = eur.value * rate;
        return new CurrencyUSD(usdValue);
    }

    public static implicit operator CurrencyRUB(CurrencyEUR eur)
    {
        double rate = GetExchangeRate("EUR", "RUB");
        double rubValue = eur.value * rate;
        return new CurrencyRUB(rubValue);
    }

    private static double GetExchangeRate(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == "EUR" && toCurrency == "USD") return eur_usd;
        else if (fromCurrency == "EUR" && toCurrency == "RUB") return eur_rub;
        return 0;
    }
}

class CurrencyRUB : Currency
{
    public CurrencyRUB(double val) : base(val) { }

    public static void SetCourse(string curr, double course)
    {
        if (curr == "EUR")
        {
            rub_eur = course;
            eur_rub = Math.Pow(course, -1);
        }
        else if (curr == "USD")
        {
            rub_usd = course;
            usd_rub = Math.Pow(course, -1);
        }
    }

    public static implicit operator CurrencyUSD(CurrencyRUB rub)
    {
        double rate = GetExchangeRate("RUB", "USD");
        double usdValue = rub.value * rate;
        return new CurrencyUSD(usdValue);
    }

    public static implicit operator CurrencyEUR(CurrencyRUB rub)
    {
        double rate = GetExchangeRate("RUB", "EUR");
        double eurValue = rub.value * rate;
        return new CurrencyEUR(eurValue);
    }

    private static double GetExchangeRate(string fromCurrency, string toCurrency)
    {
        if (fromCurrency == "RUB" && toCurrency == "USD") return rub_usd;
        else if (fromCurrency == "RUB" && toCurrency == "EUR") return rub_eur;
        return 0;
    }
}

class Executable
{
    public static void Main(string[] args)
    {
        //Выполнение первого задания
        Vector vec1 = new Vector(5, 10, 15);
        Vector vec2 = new Vector(-2, 0, 0.53);
        Vector vec3 = vec1 + vec2;

        vec1.PrintVector();
        vec2.PrintVector();
        vec3.PrintVector();

        Vector vec4 = vec3 * vec2;
        vec4.PrintVector();
        vec4 = vec4 * -0.124;
        vec4.PrintVector();
        Console.WriteLine();

        //Выполнение второго задания
        CarsCatalog catalog;

        Car car1 = new Car("Toyota", "V6", 200);
        Car car2 = new Car("Honda", "V8", 220);
        Car car3 = new Car("Ford", "V6", 180);
        catalog = new CarsCatalog(car1, car2, car3);

        Console.WriteLine(catalog[0]);
        Console.WriteLine(catalog[1]);
        Console.WriteLine(catalog[2]);
        Console.WriteLine();

        //Выполнение третьего задания
        CurrencyUSD.SetCourse("RUB", 96.63);
        CurrencyEUR.SetCourse("RUB", 103.04);
        CurrencyRUB rub = new CurrencyRUB(2000);
        CurrencyEUR eur = rub;
        CurrencyUSD usd = rub;

        rub.PrintCurr();
        eur.PrintCurr();
        usd.PrintCurr();
    }
}
