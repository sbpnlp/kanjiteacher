using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kanji.DesktopApp.Interfaces;

namespace Kanji.DesktopApp.LogicLayer
{
    /// <summary>
    /// Implements the Dynamic Time Warping algorithm
    /// </summary>
    public class TimeWarping
    {
        #region Public fields
        /// <summary>
        /// Gets or sets the cummulative distances of the matrix.
        /// </summary>
        /// <value>The cummulative distances.</value>
        public double[,] CummulativeDistances { get; set; }

        /// <summary>
        /// Gets or sets the distances of the matrix.
        /// </summary>
        /// <value>The distances.</value>
        public double[,] Distances { get; set; }
        #endregion
        #region Private fields
        /// <summary>
        /// Constant to define an empty field, because 0 is a value
        /// </summary>
        const double EMPTY = 0;

        /// <summary>
        /// Constant to define a value OUTSIDE the matrix
        /// </summary>
        const double OUTSIDE = Double.MaxValue;
        /// <summary>
        /// Gets or sets the original point sequence.
        /// </summary>
        /// <value>The original sequence.</value>
        List<Point> OriginalSequence { get; set; }
        /// <summary>
        /// Gets or sets the other sequence.
        /// </summary>
        /// <value>The other sequence.</value>
        List<Point> OtherSequence { get; set; }

        /// <summary>
        /// Gets or sets the minimal path through the point sequences.
        /// Here the Points mean coordinates in the actual CummulativeDistances matrix.
        /// In order to get the minimal path, one would have to use these coordinates to
        /// walk throught the cummulative distances.
        /// Alternatively, it's enough to read the field CummulativeDistances[i][j],
        /// which will yield the sum of the path.
        /// </summary>
        /// <value>The minimal path.</value>
        private Point[,] MinimalPath { get; set; }

        private List<Point> _warpingPath = null;

        /// <summary>
        /// Gets the warping path, which represents the minimal path through the matrix.
        /// </summary>
        /// <value>The warping path.</value>
        public List<Point> WarpingPath
        { 
            get 
            {
                if (_warpingPath == null) 
                {
                    _warpingPath = GetWarpingPath(OriginalSequence.Count - 1, OtherSequence.Count - 1);
                }
                return _warpingPath;
            } 
        }

        public double WarpingDistance
        {
            get
            {
                if (_warpingPath == null)
                {
                    _warpingPath = GetWarpingPath(OriginalSequence.Count - 1, OtherSequence.Count - 1);
                }
                return CalculateCumulativeDistance() / _warpingPath.Count;
            }
        }

        public List<Point> GetWarpingPath(int i, int j)
        {
            List<Point> bSeq = new List<Point>();

            Point p = new Point(i, j);

            while ((!p.Equals(Point.Empty)) && (!p.Equals(new Point(0,0))))
            {
                bSeq.Insert(0, p);
                p = MinimalPath[(int)p.X, (int)p.Y];
                //when reaching end, stop procedure
                if (p.Equals(new Point(0,0)))
                {
                    //insert point <0,0>
                    bSeq.Insert(0, p);
                    break;
                }
            }

            return bSeq;
        }
        #endregion

        #region Setters, Getters, Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeWarping"/> class.
        /// </summary>
        /// <param name="originalSequence">The original sequence.</param>
        /// <param name="otherSequence">The other sequence.</param>
        public TimeWarping(List<Point> originalSequence, List<Point> otherSequence)
        {
            OriginalSequence = originalSequence;
            OtherSequence = otherSequence;

            InitialiseLists();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeWarping"/> class.
        /// </summary>
        /// <param name="originalSequence">The original sequence.</param>
        /// <param name="otherSequence">The other sequence.</param>
        public TimeWarping(Stroke originalSequence, Stroke otherSequence)
        {
            OriginalSequence = originalSequence.AllPoints;
            OtherSequence = otherSequence.AllPoints;

            InitialiseLists();
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Initialises the lists.
        /// </summary>
        private void InitialiseLists()
        {
            CummulativeDistances = new double[OriginalSequence.Count, OtherSequence.Count];
            Distances = new double[OriginalSequence.Count, OtherSequence.Count];
            MinimalPath = new Point[OriginalSequence.Count, OtherSequence.Count];
            MinimalPath[0, 0] = new Point(0, 0);

            // This is only necessary for List<> not for array
            ////fill the lists with zeros
            //for (int i = 0; i < OriginalSequence.Count; i++)
            //{
            //    //adding lists for the columns
            //    CummulativeDistances.Add(new List<double>());
            //    Distances.Add(new List<double>());
            //    MinimalPath.Add(new List<Point>());

            //    //adding items for the rows
            //    for (int j = 0; j < OtherSequence.Count; j++)
            //    {
            //        CummulativeDistances[i].Add(EMPTY);
            //        Distances[i].Add(EMPTY);
            //        MinimalPath[i].Add(Point.Empty);
            //    }
            //}
        }

        /// <summary>
        /// Finds the smallest element of a list and returns the index of that element.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>The index of the smallest element.</returns>
        private int FindMinimum(List<double> list)
        {
            int minimum = 0;
            for(int i = 1; i < list.Count; i++)
            {
                if (list[minimum] > list[i]) 
                    minimum = i;
            }
            return minimum;
        }
        #endregion

        #region Public methods
        public delegate double DistanceCalculatorDelegate(Point p, Point q);

        /// <summary>
        /// Calculates the distances of the points.
        /// </summary>
        /// <returns></returns>
        public double[,] CalculateDistances(DistanceCalculatorDelegate MeasureDistance)
        {
            for (int i = 0; i < OriginalSequence.Count; i++)
            {
                //calculate distance OriginalSequence to OtherSequnce
                for (int j = 0; j < OtherSequence.Count; j++)
                {
                    //Distances[i,j] = OtherSequence[j].Distance(OriginalSequence[i]);
                    Distances[i, j] = MeasureDistance(OriginalSequence[i], OtherSequence[j]);

                }
            }

            return Distances;
        }

        /// <summary>
        /// Calculates the cumulative distance at the end point
        /// </summary>
        /// <returns></returns>
        public double CalculateCumulativeDistance()
        {
            return CalculateCumulativeDistanceOf(OriginalSequence.Count-1, OtherSequence.Count-1);
        }
        /// <summary>
        /// Calculates the minimal path.
        /// </summary>
        /// <param name="i">i corresponds to the index of the OriginalSequence</param>
        /// <param name="j">j corresponds to the index of the OtherSequence</param>
        /// <returns></returns>
        public double CalculateCumulativeDistanceOf(int i, int j)
        {
            //if walking over boundaries of matrix return OUTSIDE const
            if ((i < 0) || (j < 0)) return OUTSIDE;

            //if this cummulative distance has already been calculated
            //return it from list
            if (CummulativeDistances[i,j] != EMPTY)
                return CummulativeDistances[i,j];

            //finalise recursive function at index <0,0>,
            //where cummulative distance is equal to distance
            if ((i == 0) && (j == 0))
            {
                CummulativeDistances[i,j] = Distances[0,0];
            }
            else if (i == 0) //walk only upwards
            {
                CummulativeDistances[i,j] =
                    Distances[i,j] + CalculateCumulativeDistanceOf(i, j - 1);
                MinimalPath[i,j] = new Point(i, j - 1);
            }
            else if (j == 0) //walk only to the left
            {
                CummulativeDistances[i,j] =
                    Distances[i,j] + CalculateCumulativeDistanceOf(i - 1, j);
                MinimalPath[i,j] = new Point(i - 1, j);
            }
            else
            {
                //compute minimum of cummulative distances
                // - one up
                // - one left
                // - one left, one up

                List<double> temp = new List<double>();
                temp.Add(CalculateCumulativeDistanceOf(i, j - 1)); //0 == up
                temp.Add(CalculateCumulativeDistanceOf(i - 1, j)); //1 == left
                temp.Add(CalculateCumulativeDistanceOf(i - 1, j - 1)); //2 == upleft


                int minimum = FindMinimum(temp);

                if (minimum == 0) //left
                {
                    CummulativeDistances[i,j] =
                        Distances[i,j] + CummulativeDistances[i,j - 1];
                    MinimalPath[i,j] = new Point(i, j - 1);
                }
                else if (minimum == 1) //up
                {
                    CummulativeDistances[i,j] =
                        Distances[i,j] + CummulativeDistances[i - 1,j];
                    MinimalPath[i,j] = new Point(i - 1, j);
                }
                else
                {
                    CummulativeDistances[i,j] =
                        Distances[i,j] + CummulativeDistances[i - 1,j - 1]; //upleft
                    MinimalPath[i,j] = new Point(i - 1, j - 1);
                }
            }
            return CummulativeDistances[i,j];
        }
        #endregion

    }

    #region Helper classes
    /// <summary>
    /// A class for printing matrices
    /// </summary>
    public class MatrixPrinter
    {
        #region Private fields
        /// <summary>
        /// The Matrix to print
        /// </summary>
        List<List<double>> _matrix;
        double[,] _matrixArray = null;
        string _newLineChar = "\n";
        string _blankChar = " ";
        #endregion

        #region Setters, Getters, Constructors
        public string NewlineChar { get { return _newLineChar; } set { _newLineChar = value; } }
        public string BlankChar { get { return _blankChar; } set { _blankChar = value; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixPrinter(List<List<double>> matrix)
        {
            _matrix = matrix;
            _matrixArray = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixPrinter(double[,] matrix)
        {
            _matrixArray = matrix;
            _matrix = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="newlinechar">The newlinechar.</param>
        /// <param name="blankchar">The blankchar.</param>
        public MatrixPrinter(List<List<double>> matrix, string newlinechar, string blankchar)
        {
            _matrix = matrix;
            _matrixArray = null;
            _newLineChar = newlinechar;
            _blankChar = blankchar;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="newlinechar">The newlinechar.</param>
        /// <param name="blankchar">The blankchar.</param>
        public MatrixPrinter(double[,] matrix, string newlinechar, string blankchar)
        {
            _matrix = null;
            _matrixArray = matrix;
            _newLineChar = newlinechar;
            _blankChar = blankchar;
        }

        #endregion

        #region Public methods

        public string print()
        {
            if (_matrixArray == null) return printMatrixList();
            else return printMatrixArray();
        }

        private string printMatrixArray()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Rows and columns are switched in this print view");
            //array is supposed to be two-dimensional,
            //therefore: 
            if (_matrixArray.Rank == 2)
            {
                int m = _matrixArray.GetLength(0);
                int n = _matrixArray.GetLength(1);

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        output.AppendFormat(string.Format("{0:0.000}", _matrixArray[i, j]));

                        if (j + 1 < n)
                        {
                            output.Append("," + _blankChar);
                        }
                    }
                    output.Append(_newLineChar);
                }
            }
            return output.ToString();
        }

        /// <summary>
        /// Prints this instance.
        /// </summary>
        /// <returns></returns>
        private string printMatrixList() 
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Rows and columns are switched in this print view");

            for (int i=0;i<_matrix.Count;i++) 
            {   
                int localLen = _matrix[i].Count;

                for(int j=0;j<localLen;j++) 
                {
                    output.AppendFormat(string.Format("{0:0.000}", _matrix[i][j]));

                    if (j+1<localLen) 
                    {
                        output.Append("," + _blankChar);
                    }
                }
                output.Append(_newLineChar);
            }
            return output.ToString();
        }

        /// <summary>
        /// Prints a new matrix with current configuration.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns></returns>
        public string printNewMatrix(List<List<double>> matrix)
        {
            _matrix = matrix;
            _matrixArray = null;
            return print();
        }

        /// <summary>
        /// Prints a new matrix with current configuration.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns></returns>
        public string printNewMatrix(double[,] matrix)
        {
            _matrix = null;
            _matrixArray = matrix;
            return print();
        }
        #endregion
    }
    #endregion
}

