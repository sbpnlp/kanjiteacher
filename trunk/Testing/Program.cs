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
            Console.ReadLine();
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

            List<List<double>> pq = new List<List<double>>(q.Count);
            List<List<double>> rq = new List<List<double>>(q.Count);

            for (int i = 0; i < q.Count; i++)
            {
                pq.Add(new List<double>(p.Count));
                rq.Add(new List<double>(r.Count));

                //calculate distance q to p
                for (int j = 0; j < p.Count; j++)
                {
                    pq[i].Add(p[j].Distance(q[i]));
                }

                //calculate distance q to r
                for (int j = 0; j < r.Count; j++)
                {
                    rq[i].Add(r[j].Distance(q[i]));
                }
            }

            //MatrixPrinter m = new MatrixPrinter(pq);
            //m.NewlineChar = "\r\n";
            //m.BlankChar = " ";

            //Console.WriteLine(m.print());


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
                tw.CalculateCummulativeDistanceOf((int)myStop.X, (int)myStop.Y));
            foreach (Point mp in tw.BackwardsSequence((int)myStop.X, (int) myStop.Y))
            {
                Console.WriteLine(mp);
            }

        }

 

    }
}
