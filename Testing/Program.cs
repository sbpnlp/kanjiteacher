using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.LogicLayer;
using System.IO;
using Kanji.DesktopApp.LogicLayer.Helpers;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTimeWarping();
//            TestStrokeHashing();
            TestPointHashing();
            //RunConverter();
            //TestAddZeros();
            //TestBoundingBox();
            Console.ReadLine();
        }

        private static void TestPointHashing()
        {
            Point p = new Point(1, 2);
            Point q = new Point(3, 4);
            Point r = new Point(500, 600, DateTime.Now);
            BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create, FileAccess.Write, FileShare.None));
            bw.Write(p.Hash(false));
            //bw.Write((byte)255);
            bw.Write(q.Hash(false));
            //bw.Write((byte)255);
            bw.Write(r.Hash(true));
            bw.Close();
        }

        private static void TestStrokeHashing()
        {
            List<Point> p = new List<Point>();
            p.Add(new Point(1, 1));
            p.Add(new Point(2, 2));
            p.Add(new Point(4, 2));
            p.Add(new Point(5, 3));
            p.Add(new Point(6, 2));
            p.Add(new Point(6, 3));

            List<Point> q = new List<Point>();
            q.Add(new Point(2, 4));
            q.Add(new Point(3, 5));
            q.Add(new Point(5, 5));
            q.Add(new Point(6, 6));
            q.Add(new Point(7, 5));
            q.Add(new Point(7, 6));

            Stroke s = new Stroke(p);
            Stroke s2 = new Stroke(p);
            Stroke r = new Stroke(q);

            int sHash = s.GetHashCode();
            int s2Hash = s2.GetHashCode();
            int rHash = r.GetHashCode();
            Console.WriteLine("Hash: s: {0}, s2: {1} r: {2}", sHash, s2Hash, rHash);
        }

        private static void TestAddZeros()
        {
            Console.WriteLine(StringTools.AddZeros(3, 4));
            Console.WriteLine(StringTools.AddZeros((long)3, 4));
            Console.WriteLine(StringTools.AddZeros(3, (long)4));
            Console.WriteLine(StringTools.AddZeros((long)3, (long)4));
            Console.WriteLine(StringTools.AddZeros(31, 5));
            Console.WriteLine(StringTools.AddZeros((long)31, 5));
            Console.WriteLine(StringTools.AddZeros(31, (long)5));
            Console.WriteLine(StringTools.AddZeros((long)31, (long)5));
            Console.WriteLine(StringTools.AddZeros(301, 6));
            Console.WriteLine(StringTools.AddZeros((long)301, 6));
            Console.WriteLine(StringTools.AddZeros(301, (long)6));
            Console.WriteLine(StringTools.AddZeros((long)301, (long)6));
            Console.WriteLine(StringTools.AddZeros(3003, 7));
            Console.WriteLine(StringTools.AddZeros((long)3003, 7));
            Console.WriteLine(StringTools.AddZeros(3003, (long)7));
            Console.WriteLine(StringTools.AddZeros((long)3003, (long)7));
            Console.WriteLine(StringTools.AddZeros(30303, 8));
            Console.WriteLine(StringTools.AddZeros((long)30303, 8));
            Console.WriteLine(StringTools.AddZeros(30303, (long)8));
            Console.WriteLine(StringTools.AddZeros((long)30303, (long)8));

            //stringtools test: add zeros.
            //get it right...
        }

        private static void RunConverter()
        {
            //Converter.ConvertInputToFinalFormat(new FileStream("C:\\Diplom\\kanjiteacher\\data\\strokes.txt", FileMode.Open));
            //Converter.ConvertInputToFinalFormat(new FileStream("C:\\Diplom\\kanjiteacher\\data\\char00255.notQuite.inkml", FileMode.Open));
            //Converter.ConvertInputToFinalFormat(new FileStream("C:\\Diplom\\kanjiteacher\\data\\strokes2.txt", FileMode.Open));

            UPXReader.ParseUPXFile(new FileStream("C:\\Diplom\\kanjiteacher\\data\\exampleFormat.upx", FileMode.Open));
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
            tw.CalculateDistances((p1, p2) => p1.Distance(p2));
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

            List<Point> newP = Vector2.CreatePointList(bp.VectorsFromAnchor[0]);
            List<Point> newQ = Vector2.CreatePointList(bq.VectorsFromAnchor[0]);
            List<Point> newR = Vector2.CreatePointList(br.VectorsFromAnchor[0]);
            List<Point> newS = Vector2.CreatePointList(bs.VectorsFromAnchor[0]);

            TimeWarping tw2 = new TimeWarping(newR, newQ);
            tw2.CalculateDistances((p1, p2) => p1.Distance(p2));

            myStop.X = newR.Count - 1;
            myStop.Y = newQ.Count - 1;

            Console.WriteLine(m.printNewMatrix(tw2.Distances));

            Console.WriteLine("\n\n\n");

            Console.WriteLine("Cummulative distance of <{0},{1}>", myStop.X, myStop.Y);
            Console.WriteLine(tw2.CalculateCumulativeDistance());
            foreach (Point mp in tw2.WarpingPath)
            {
                Console.WriteLine(mp);
            }


            TimeWarping tw3 = new TimeWarping(newP, newS);
            tw3.CalculateDistances(delegate(Point p1, Point p2) { return p1.Distance(p2); });
            Console.WriteLine("Euclidian distances:");
            Console.WriteLine(m.printNewMatrix(tw3.Distances));
            tw3.CalculateDistances(delegate(Point p1, Point p2) { return Math.Pow(p1.X-p2.X, 2) + Math.Pow(p1.Y-p2.Y, 2); });
            Console.WriteLine("Added squares, no square root:");
            Console.WriteLine(m.printNewMatrix(tw3.Distances));            




            Console.WriteLine("\n\n\n");

            Console.WriteLine("Cummulative distance of <{0},{1}>", newP.Count-1, newS.Count-1);
            Console.WriteLine(tw3.CalculateCumulativeDistance());
            foreach (Point mp in tw3.WarpingPath)
            {
                Console.WriteLine(mp);
            }

            Console.WriteLine("Warping distance r / q: " + tw.WarpingDistance.ToString());
            Console.WriteLine("Warping distance newR / newQ (newR is the same shape but double size): " + tw2.WarpingDistance.ToString());
            Console.WriteLine("Warping distance newP / newS (are resized to the same bounding box and then moved on top of each other): " + tw3.WarpingDistance.ToString());
        }
    }
}
