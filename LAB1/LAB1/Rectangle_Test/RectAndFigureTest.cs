using Microsoft.VisualStudio.TestTools.UnitTesting;
using LAB1;

namespace RectAndFigureTest
{
    [TestClass]
    public class RectAndFigureTest
    {
        [TestMethod]
        public void Rect_55_32()
        {
            Rectangle rec = new Rectangle(5.5, 3.2);
            Assert.AreEqual(17.6, rec.Area);
            Assert.AreEqual(17.4, rec.Perimeter);
        }

        [TestMethod]
        public void Figure_Rect()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 5);
            Point p3 = new Point(5, 5);
            Point p4 = new Point(5, 0);
            Figure tri = new Figure(p1, p2, p3, p4);
            Assert.AreEqual(20, tri.PerimeterCalculator());
        }
    }
}