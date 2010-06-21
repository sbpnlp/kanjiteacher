using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.LogicLayer;
using System.IO;
using Kanji.DesktopApp.LogicLayer.Helpers;
using System.IO.IsolatedStorage;
using System.Xml;
using Kanji.StrokeMirrorer;
using KSvc = Kanji.KanjiService;
using System.Threading;
using F = System.Windows.Forms;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTimeWarping();
            //TestStrokeHashing();
            //TestRadicalHashing();
            //TestCharacterHashing();
            //TestIsolatedStorage();
            //TestHashTable();
            TestInkMLReading();
            //TestPointHashing();
            //RunConverter();
            //TestAddZeros();
            //TestBoundingBox();
            Console.ReadLine();
        }

        private static void TestInkMLReading()
        {
            KSvc.Service serv = new KSvc.Service();

            //don't show metadata
            serv.ShowMetaData = false;

            //starting service
            PointLoadObserver plso = new PointLoadObserver();
            ThreadStart tStart = delegate { serv.Run(plso); };
            Thread t = new Thread(tStart);
            t.Start();

            //starting mirror GUI
            F.Application.EnableVisualStyles();
            F.Application.SetCompatibleTextRenderingDefault(false);

            //initialise viewing area
            MirrorArea ma = new MirrorArea(plso);
            List<Stroke> strokes = new List<Stroke>();
            //strokes = InkMLReader.ReadInkMLFile("char02211.inkml");
            //strokes = InkMLReader.ReadInkMLFile("char00846.inkml");
            //strokes = InkMLReader.ReadInkMLFile("char00555.inkml");
            //strokes = InkMLReader.ReadInkMLFile("char00117.inkml");
            //strokes = InkMLReader.ReadInkMLFile("char00935.inkml");
            strokes = InkMLReader.ReadInkMLFile("char00117vs935hybrid.inkml");
            
            
            //go through all the strokes in the file
            //fill viewing area
            foreach (Stroke dbStroke in strokes)
            {
                plso.ReveivePoints(dbStroke.AllPoints);
            }

            //show viewing area
            ma.ShowDialog();
            ma.Hide();
            ma.Close();
            ma.Dispose();
            t.Abort();
        }

        private static void TestHashTable()
        {
            /* Concept:
             * The "database" dictionary holds all the strokes from the DB (once)
             * stored under the name of their md5-hash
             * 
             * The "inputlist" List<Stroke> contains all the 
             * strokes that come in from the user.
             * 
             * The value is a dictionary with keys: md5hash of input
             * and value: input stroke
             * 
             * Open question:
             * where are the real strokes stored?
             * proposal: in a second dictionary
             * Dictionary<byte[], Stroke> database = new Dictionary<byte[], Stroke>();
             */

            Dictionary<byte[], Dictionary<byte[], double>> matchingscores =
                new Dictionary<byte[], Dictionary<byte[], double>>();

            List<Character> characterdatabase =
                UPXReader.ParseUPXFile(
                    File.Open("C:\\Diplom\\kanjiteacher\\data\\exampleFormat.upx", FileMode.Open));

            //Dictionary<byte[], Stroke> database = new Dictionary<byte[], Stroke>();

            List<Stroke> inputlist = null;

            //take random strokes as inputlist
            inputlist = new List<Stroke>() { GetAlmostRandomStroke(0), GetAlmostRandomStroke(1) };
            //takes strokes of the same character from inputlist
            inputlist = InkMLReader.ReadInkMLFile("char00255.inkml");

            ////fill the stroke database with the strokes from all the characters
            ////stored under their hash codes
            //foreach (Character c in characterdatabase)
            //    foreach (Stroke s in c.StrokeList)
            //        database.Add(s.Hash(false), s);

            //go through all the strokes in the database
            foreach (Character c in characterdatabase)
            {
                //go through all the strokes in the list of input strokes
                foreach (Stroke s in inputlist)
                {
                    foreach (Stroke dbStroke in c.StrokeList)
                    {
                        //calculate the matching score
                        double score = dbStroke.MatchingScore(s, new TWStrokeMatcher());

                        //store the score in big matrix of stroke match values
                        if (!matchingscores.Keys.Contains(dbStroke.Hash()))
                        {
                            //add the score under the correct key in a new dictionary 
                            //under the current strokes key
                            matchingscores.Add(
                                dbStroke.Hash(), 
                                new Dictionary<byte[], double>() { {s.Hash(false), score }});
                        }
                        else
                        {
                            //add new score for the input stroke to the
                            //existing matching dict at the entry of the current database stroke
                            matchingscores[dbStroke.Hash()].Add(s.Hash(), score);
                        }
                    }
                }
            }

            MatrixPrinter m = new MatrixPrinter(matchingscores);
            Console.WriteLine(m.print());

            Console.WriteLine(
                "Now find total minimum of these scores" +
"i.e. go ahead and do some matrix operation in order to find" +
"the minimum of each row." +
"if stroke number is identical, consider position within matrix" +
"each row can only have one best matching column" +
"each column can only have one best matching row" +
"find total minimum and return it"
                );
        }

        private static void TestIsolatedStorage()
        {
            WriteIsolatedStorage();
            ReadIsolatedStorage();
        }

        private static void ReadIsolatedStorage()
        {
            // create an isolated storage stream...
            IsolatedStorageFileStream userDataFile =
                new IsolatedStorageFileStream("thepath", FileMode.Open);

            // create a reader to the stream...
            StreamReader readStream = new StreamReader(userDataFile);
            // write strings to the Isolated Storage file...
            Console.WriteLine(readStream.ReadToEnd());
            // Tidy up by closing the streams...
            readStream.Close();
            userDataFile.Close();
        }

        private static void WriteIsolatedStorage()
        {
            IsolatedStorageFileStream fs =
                new IsolatedStorageFileStream(
                    "thepath",
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.None);

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("hallo");
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();
        }

        private static void TestPointHashing()
        {
            Point p = new Point(1, 2, new DateTime(0));
            Point q = new Point(3, 4, DateTime.MinValue);
            Point r = new Point(500, 600, DateTime.Now);
            BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create, FileAccess.Write, FileShare.None));
            bw.Write(p.Hash(true));
            //bw.Write((byte)255);
            bw.Write(q.Hash(true));
            //bw.Write((byte)255);
            bw.Write(r.Hash(true));
            bw.Close();
        }

        /// <summary>
        /// Gets the almost random stroke.
        /// </summary>
        /// <param name="i">The i. Legal values 0 or 1 for one or the other stroke</param>
        /// <returns></returns>
        private static Stroke GetAlmostRandomStroke(int i)
        {
            Random rand = new Random();
            List<Point> p = new List<Point>();

            DateTime now = DateTime.Now;

            int randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(1, 1, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(2, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(4, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(5, 3, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 3, new DateTime(now.Ticks - randNext)));

            List<Point> q = new List<Point>();
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(2, 4, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(3, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(5, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(6, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 6, new DateTime(now.Ticks - randNext)));


            Stroke r = null;

            switch (i)
            {
                case 0: r = new Stroke(p);
                    break;
                case 1: r = new Stroke(q);
                    break;
            }

            return r;
        }
        private static void TestStrokeHashing()
        {
            Random rand = new Random();
            List<Point> p = new List<Point>();

            DateTime now = DateTime.Now;

            int randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(1, 1, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(2, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(4, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(5, 3, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(6, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            p.Add(new Point(6, 3, new DateTime(now.Ticks - randNext)));

            List<Point> q = new List<Point>();
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(2, 4, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(3, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(5, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(6, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(7, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10,4), (int)Math.Pow(10,5));
            q.Add(new Point(7, 6, new DateTime(now.Ticks - randNext)));

            Stroke s = new Stroke(p);
            Stroke s2 = new Stroke(p);
            Stroke r = new Stroke(q);

            BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create, FileAccess.Write, FileShare.None));
            bw.Write(s.Hash(false));
            //bw.Write((byte)255);
            bw.Write(s2.Hash(false));
            //bw.Write((byte)255);
            bw.Write(r.Hash(false));
            bw.Close();
        }

        private static void TestRadicalHashing()
        {
            Random rand = new Random();
            List<Point> p = new List<Point>();

            DateTime now = DateTime.Now;

            int randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(1, 1, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(2, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(4, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(5, 3, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 3, new DateTime(now.Ticks - randNext)));

            List<Point> q = new List<Point>();
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(2, 4, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(3, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(5, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(6, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(8, 7, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(9, 8, new DateTime(now.Ticks - randNext)));

            Stroke s = new Stroke(p);
            Stroke s2 = new Stroke(p);
            Stroke r = new Stroke(q);

            List<Stroke> strokelist = new List<Stroke>();
            strokelist.Add(s);
            strokelist.Add(s2);
            strokelist.Add(r);

            Radical rad = new Radical(strokelist);

            BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create, FileAccess.Write, FileShare.None));
            bw.Write(rad.Hash(true));
            bw.Close();
        }

        private static void TestCharacterHashing()
        {
            Random rand = new Random();
            List<Point> p = new List<Point>();

            DateTime now = DateTime.Now;

            int randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(1, 1, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(2, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(4, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(5, 3, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 2, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            p.Add(new Point(6, 3, new DateTime(now.Ticks - randNext)));

            List<Point> q = new List<Point>();
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(2, 4, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(3, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(5, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(6, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 5, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(7, 6, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(8, 7, new DateTime(now.Ticks - randNext)));
            randNext = rand.Next((int)Math.Pow(10, 4), (int)Math.Pow(10, 5));
            q.Add(new Point(9, 8, new DateTime(now.Ticks - randNext)));

            Stroke s = new Stroke(p);
            Stroke s2 = new Stroke(p);
            Stroke r = new Stroke(q);

            List<Stroke> strokelist = new List<Stroke>();
            strokelist.Add(s);
            strokelist.Add(s2);
            strokelist.Add(r);

            Radical rad1 = new Radical(strokelist);
            Radical rad2 = new Radical(strokelist);
            List<Radical> rl = new List<Radical>(2);
            rl.Add(rad1);
            rl.Add(rad2);
            Character ch = new Character(rl);

            BinaryWriter bw = new BinaryWriter(new FileStream("test", FileMode.Create, FileAccess.Write, FileShare.None));
            bw.Write(ch.Hash(false));
            bw.Close();
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

            List<Character> cList = 
                UPXReader.ParseUPXFile(
                new FileStream("C:\\Diplom\\kanjiteacher\\data\\exampleFormat.upx", FileMode.Open));

            DirectoryInfo di = Directory.CreateDirectory("C:\\Diplom\\kanjiteacher\\data");

            foreach (Character c in cList)
            {
                XmlDocument doc = UPXReader.CreateXMLDocumentFromCharacter(c);
                string filename = "char" + c.SHKK + ".INOUT.inkml";                
                UPXReader.WriteXMLDocumentToFile(doc, Path.Combine(di.FullName, filename));
            }
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
