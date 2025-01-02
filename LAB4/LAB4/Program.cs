using System.Collections;

class MyMatrix
{
    private int m, n;
    private double[][] matrix;
   
    public MyMatrix(int _m, int _n, int rand_begin, int rand_end)
    {
        Random random = new Random();

        m = _m;                                                                 //Столбцы
        n = _n;                                                                 //Строки
        double[][] matr = new double[n][];                                          //Создаем каркас матрицы

        for (int i = 0; i < n; i++)
        {
            double[] line = new double[m];                                            //Создаем строку
            for (int j = 0; j < m; j++)                                         //Заполняем строку
            {
                line[j] = random.Next(rand_begin, rand_end);
            }
            matr[i] = line;                                                   //Добавляем строку в матрицу
        }
        matrix = matr;
    }

    public static MyMatrix operator+(MyMatrix a, MyMatrix b)
    {
        MyMatrix matrix = new MyMatrix(a.m, a.n, 0, 0);

        if (a.m == b.m && a.n == b.n)
        {

            for (int i = 0; i < a.n; ++i)
            {
                for (int j = 0; j < a.m; ++j)
                {
                    matrix.matrix[i][j] = a.matrix[i][j] + b.matrix[i][j];
                }
            }
        }
        return matrix;
    }
    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        MyMatrix matrix = new MyMatrix(a.m, a.n, 0, 0);

        if (a.m == b.m && a.n == b.n)
        {
            for (int i = 0; i < a.n; ++i)
            {
                for (int j = 0; j < a.m; ++j)
                {
                    matrix.matrix[i][j] = a.matrix[i][j] - b.matrix[i][j];
                }
            }
        }
        return matrix;
    }


    /*     M
     *    a b c d e f         f t
     *  N a b c f e r         e r
     *    f r r t w t         h w
     *                        w r
     *                        h h
     */
    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        MyMatrix matrix = new MyMatrix(a.m, b.n, 0, 0);

        if (a.m == b.n)
        {
            for (int i = 0; i < a.n; ++i)
            {
                for (int j = 0; j < b.m; ++j)
                {
                    for(int k = 0; k < a.m; ++k)
                    {
                        matrix.matrix[i][j] += Math.Round(a.matrix[i][k] * b.matrix[k][j]);
                    }
                }
            }
        }
        return matrix;
    }
    public static MyMatrix operator *(MyMatrix a, double scalar)
    {
        MyMatrix matrix = new MyMatrix(a.m, a.n, 0, 0);
        for (int i = 0; i < a.n; ++i)
        {
            for (int j = 0; j < a.m; ++j)
            {
                matrix.matrix[i][j] = Math.Round(a.matrix[i][j] * scalar);
            }
        }
        return matrix;
    }
    public static MyMatrix operator /(MyMatrix a, double scalar)
    {
        MyMatrix matrix = new MyMatrix(a.m, a.n, 0, 0);
        for (int i = 0; i < a.n; ++i)
        {
            for (int j = 0; j < a.m; ++j)
            {
                matrix.matrix[i][j] = Math.Round(a.matrix[i][j] / scalar, 2);
            }
        }
        return matrix;
    }
    public double this[int indexRow, int indexCol]
    {
        get => matrix[indexCol][indexRow];
        set => matrix[indexCol][indexRow] = value;
    }
    public void Print()
    {
        foreach (double[] line in matrix)
        {
            foreach (double elem in line)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
class Car
{
    public int ProductionYear { get; set; }
    public string Name { get; set; }
    public int MaxSpeed { get; set; }
    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }
}
class CarComparer : IComparer<Car>
{
    private string option;
    public CarComparer(string _option)
    {
        option = _option;
    }
    public int Compare(Car? car1, Car? car2)
    {

        if (car1 == null || car2 == null) 
        {
            throw new ArgumentException("Некорректное значение параметра");
        }
        else
        {
            switch(option)
            {
                case "Name":
                    return car1.Name.Length - car2.Name.Length;
                case "ProductionYear":
                    return car1.ProductionYear - car2.ProductionYear;
                case "MaxSpeed":
                    return car1.MaxSpeed - car2.MaxSpeed;
                default:
                    throw new ArgumentException("Invalid sort by option");
            }
        }
    }
}

class CarCatalog : IEnumerable<Car>
{
    private List<Car> carArr = new List<Car>();
    public CarCatalog(List<Car> carArr)
    {
        this.carArr = carArr;
    }

    public IEnumerator<Car> GetEnumerator()
    {
        foreach (var car in carArr)
        {
            yield return car;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<Car> Reverse()
    {
        for (int i = carArr.Count - 1; i >= 0; i--)
        {
            yield return carArr[i];
        }
    }

    public IEnumerable<Car> FilterByYear(int year)
    {
        foreach (var car in carArr)
        {
            if (car.ProductionYear == year)
            {
                yield return car;
            }
        }
    }

    public IEnumerable<Car> FilterByMaxSpeed(int maxSpeed)
    {
        foreach (var car in carArr)
        {
            if (car.MaxSpeed == maxSpeed)
            {
                yield return car;
            }
        }
    }
}
public class Executable
{
    static void Main(string[] args)
    {
        //Задание 1
        MyMatrix m1 = new MyMatrix(4, 4, -12, 10);
        MyMatrix m2 = new MyMatrix(4, 4, -12, 10);
        MyMatrix m3 = m1 + m2;
        MyMatrix m4 = m1 - m2;
        MyMatrix m5 = m4 * m3;
        MyMatrix m6 = m5 * 1.4;
        MyMatrix m7 = m6 / 2;
        m1.Print();
        m2.Print();
        m3.Print();
        m4.Print();
        m5.Print();
        m6.Print();
        m7.Print();

        m7[0, 0] = m6[0, 0];
        m7.Print();

        //Задание 2
        Car[] cars = new Car[]
        {
            new Car("Toyota", 2010, 180),
            new Car("Honda", 2008, 170),
            new Car("Ford", 2015, 200),
            new Car("Chevrolet", 2012, 190)
        };

        Console.WriteLine("Sorting by Name:");
        Array.Sort(cars, new CarComparer("Name"));
        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Name} - {car.ProductionYear} - {car.MaxSpeed}");
        }

        Console.WriteLine("\nSorting by Production Year:");
        Array.Sort(cars, new CarComparer("ProductionYear"));
        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Name} - {car.ProductionYear} - {car.MaxSpeed}");
        }

        Console.WriteLine("\nSorting by Max Speed:");
        Array.Sort(cars, new CarComparer("MaxSpeed"));
        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Name} - {car.ProductionYear} - {car.MaxSpeed}");
        }
        Console.WriteLine();

        //Задание 3
        Car car1 = new Car("BMW", 2020, 200);
        Car car2 = new Car("Audi", 2019, 220);
        Car car3 = new Car("Mercedes", 2021, 190);
        List<Car> _cars = new List<Car>(){car1, car2, car3};
        CarCatalog catalog = new CarCatalog(_cars);

        Console.WriteLine("Прямой проход:");
        foreach (Car car in catalog)
        {
            Console.WriteLine($"{car.Name} ({car.ProductionYear}), Max Speed: {car.MaxSpeed}");
        }

        Console.WriteLine("\nОбратный проход:");
        foreach (Car car in catalog.Reverse())
        {
            Console.WriteLine($"{car.Name} ({car.ProductionYear}), Max Speed: {car.MaxSpeed}");
        }

        Console.WriteLine("\nПроход с фильтром по году выпуска:");
        foreach (Car car in catalog.FilterByYear(2020))
        {
            Console.WriteLine($"{car.Name} ({car.ProductionYear}), Max Speed: {car.MaxSpeed}");
        }

        Console.WriteLine("\nПроход с фильтром по максимальной скорости:");
        foreach (Car car in catalog.FilterByMaxSpeed(220))
        {
            Console.WriteLine($"{car.Name} ({car.ProductionYear}), Max Speed: {car.MaxSpeed}");
        }
    }
}