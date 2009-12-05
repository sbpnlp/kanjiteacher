using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.LogicLayer;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTimeWarping();
            //TestBoundingBox();
            Console.ReadLine();
        }

        private static void TestBoundingBox()
        {
            List<Point> p = new List<Point>();
            p.Add(new Point(1, 1));
            p.Add(new Point(2, 2));
            p.Add(new Point(4, 2));
            p.Add(new Point(5, 3));
            p.Add(new Point(6, 2));

            BoundingBox bb = new BoundingBox(p);
            
        }

        private static void TestTimeWarping()
        {
            List<Point> p = new List<Point>();
            p.Add(new Point(1, 1));
            p.Add(new Point(2, 2));
            p.Add(new Point(4, 2));
            p.Add(new Point(5, 3));
            p.Add(new Point(6, 2));

            List<Point> q = new List<Point>();
            q.Add(new Point(2, 4));
            q.Add(new Point(3, 5));
            q.Add(new Point(5, 5));
            q.Add(new Point(6, 6));
            q.Add(new Point(7, 5));

            List<Point> r = new List<Point>();
            r.Add(new Point(2, 6));
            r.Add(new Point(3, 7));
            r.Add(new Point(5, 7));
            r.Add(new Point(6, 8));
            r.Add(new Point(7, 7));

            List<Point> s = new List<Point>();
            s.Add(new Point(0.5, 4.5));
            s.Add(new Point(2, 6));
            s.Add(new Point(5.5, 5));
            s.Add(new Point(6.5, 6.5));
            s.Add(new Point(7.5, 7));
            s.Add(new Point(8.5, 5.5));
            s.Add(new Point(9.5, 5));

            TimeWarping tw = new TimeWarping(r, q);
            tw.CalculateDistances();
            MatrixPrinter m = new MatrixPrinter(tw.Distances);
            m.NewlineChar = "\r\n";
            m.BlankChar = " ";

            Console.WriteLine(m.print());

            Console.WriteLine("\n\n\n");

            Point myStop = new Point(r.Count - 1, q.Count - 1);

            Console.WriteLine("Cummulative distance of <{0},{1}>", myStop.X, myStop.Y);
            Console.WriteLine(
                tw.CalculateCumulativeDistanceOf((int)myStop.X, (int)myStop.Y));
            foreach (Point mp in tw.GetWarpingPath((int)myStop.X, (int) myStop.Y))
            {
                Console.WriteLine(mp);
            }

            BoundingBox bp = new BoundingBox(p);
            BoundingBox bq = new BoundingBox(q);
            BoundingBox br = new BoundingBox(r);
            br.Stretch(2);
            BoundingBox bs = new BoundingBox(s);
            //resize bs in a way that it is similar to p
            if (bs.Width > bp.Width)
            {
                bs.Stretch(bp.Width / bs.Width);
            }
            else
            {
                bs.Stretch(bs.Width / bp.Width);
            }

            List<Point> newP = Vector2.CreatePointList(bp.VectorsFromAnchor);
            List<Point> newQ = Vector2.CreatePointList(bq.VectorsFromAnchor);
            List<Point> newR = Vector2.CreatePointList(br.VectorsFromAnchor);
            List<Point> newS = Vector2.CreatePointList(bs.VectorsFromAnchor);

            TimeWarping tw2 = new TimeWarping(newR, newQ);
            tw2.CalculateDistances();

            Console.WriteLine(m.printNewMatrix(tw2.Distances));

            Console.WriteLine("\n\n\n");

            Console.WriteLine("Cummulative distance of <{0},{1}>", myStop.X, myStop.Y);
            Console.WriteLine(tw2.CalculateCumulativeDistance());
            foreach (Point mp in tw2.WarpingPath)
            {
                Console.WriteLine(mp);
            }


            TimeWarping tw3 = new TimeWarping(newP, newS);
            tw3.CalculateDistances();

            Console.WriteLine(m.printNewMatrix(tw3.Distances));

            Console.WriteLine("\n\n\n");

            Console.WriteLine("Cummulative distance of <{0},{1}>", newP.Count-1, newS.Count-1);
            Console.WriteLine(tw3.CalculateCumulativeDistance());
            foreach (Point mp in tw3.WarpingPath)
            {
                Console.WriteLine(mp);
            }



        }

 

    }
}
