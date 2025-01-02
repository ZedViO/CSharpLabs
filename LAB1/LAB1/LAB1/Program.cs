using System;
using System.Runtime.CompilerServices;

namespace LAB1
{
    class CtsInfo
    {
        public void cts_info()
        {
            Console.WriteLine("byte, min = " + byte.MinValue + ", max = " + byte.MaxValue);
            Console.WriteLine("sbyte, min = " + sbyte.MinValue + ", max = " + sbyte.MaxValue);
            Console.WriteLine("short, min = " + short.MinValue + ", max = " + short.MaxValue);
            Console.WriteLine("ushort, min = " + ushort.MinValue + ", max = " + ushort.MaxValue);
            Console.WriteLine("int, min = " + int.MinValue + ", max = " + int.MaxValue);
            Console.WriteLine("uint, min = " + uint.MinValue + ", max = " + uint.MaxValue);
            Console.WriteLine("long, min = " + long.MinValue + ", max = " + long.MaxValue);
            Console.WriteLine("ulong, min = " + ulong.MinValue + ", max = " + ulong.MaxValue);
            Console.WriteLine("float, min = " + float.MinValue + ", max = " + float.MaxValue);
            Console.WriteLine("double, min = " + double.MinValue + ", max = " + double.MaxValue);
            Console.WriteLine("decimal, min = " + decimal.MinValue + ", max = " + decimal.MaxValue);
            Console.WriteLine("char, min = " + char.MinValue + ", max = " + char.MaxValue);
        }
    }

   public class Rectangle
    {
        private double side1, side2;


        public Rectangle(double side1, double side2)
        {
            if (side1 > 0 && side2 > 0)
            {
                this.side1 = side1;
                this.side2 = side2;
            }
            else
            {
                Console.WriteLine("Введите положительное значение");
            }
        }

        private double CalculateArea()
        {
            return this.side1 * this.side2;
        }

        private double CalculatePerimeter()
        {
            return 2 * (this.side1 + this.side2);
        }

        public double Area
        {
            get { return CalculateArea(); }
        }

        public double Perimeter
        {
            get { return CalculatePerimeter(); }
        }
        
    }

    public class Point
    {
        private int xCoord, yCoord;

        public int XCoord
        {
            get { return xCoord; }
            set { xCoord = value; }
        }

        public int YCoord
        {
            get { return yCoord; }
            set { yCoord = value; }
        }

        public Point(int xCoord, int yCoord)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }
    }

    public class Figure
    {
        private Point point1, point2, point3, point4, point5;
        private string name;

        public Figure(Point point1, Point point2, Point point3)
        {
            this.name = "Triangle";
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }

        public Figure(Point point1, Point point2, Point point3, Point point4) : this(point1, point2, point3)
        {
            this.name = "Quadrangle";
            this.point4 = point4;
        }

        public Figure(Point point1, Point point2, Point point3, Point point4, Point point5) : this(point1, point2, point3, point4)
        {
            this.name = "Pentagon";
            this.point5 = point5;
        }

        private double LengthSide(Point point1, Point point2) 
        {
            double lengthX = Math.Abs(point2.XCoord - point1.XCoord);
            double lengthY = Math.Abs(point2.YCoord - point1.YCoord);
            return Math.Sqrt(Math.Pow(lengthX, 2) + Math.Pow(lengthY, 2));
        }

        public double PerimeterCalculator()
        {
            double result;
            if (name.Equals("Triangle"))
            {
                result = LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point1);
                Console.WriteLine(result);
            }
            else if (name.Equals("Quadrangle"))
            {
                result = LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point4) + LengthSide(point4, point1);
                Console.WriteLine(result);
            }
            else
            {
                result = LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point4) + LengthSide(point4, point5) +
                                LengthSide(point5, point1);
                Console.WriteLine(result);
            }
            Console.WriteLine(name);
            return result;
        }
    }

    class Executable
    {
        public static void Main(string[] args)
        {   //Выполнение первой задачи
            CtsInfo obj = new CtsInfo();
            obj.cts_info();              
            Console.WriteLine();

            //Выполнение второй задачи
            Rectangle rect = new Rectangle(5.5, 3.2);
            Console.WriteLine("Площадь равна: " + rect.Area);
            Console.WriteLine("Периметр равен: " + rect.Perimeter);
            Console.WriteLine();

            //Выполнение третей задачи
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 5);
            Point p3 = new Point(5, 5);
            Point p4 = new Point(5, 0);
            Figure tri = new Figure(p1, p2, p3, p4);
            tri.PerimeterCalculator();
        }
    }
}