using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.LogicLayer.Helpers
{
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
        Dictionary<byte[], Dictionary<byte[], double>> _matrixDict = null;
        string _newLineChar = "\n";
        string _blankChar = ((char) 0x20).ToString();
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
            _matrixDict = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixPrinter(double[,] matrix)
        {
            _matrixArray = matrix;
            _matrix = null;
            _matrixDict = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public MatrixPrinter(Dictionary<byte[], Dictionary<byte[], double>> matrix)
        {
            _matrixArray = null;
            _matrix = null;
            _matrixDict = matrix;
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
            _matrixDict = null;
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
            _matrixDict = null;
            _newLineChar = newlinechar;
            _blankChar = blankchar;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixPrinter"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="newlinechar">The newlinechar.</param>
        /// <param name="blankchar">The blankchar.</param>
        public MatrixPrinter(Dictionary<byte[], Dictionary<byte[], double>> matrix, string newlinechar, string blankchar)
        {
            _matrix = null;
            _matrixArray = null;
            _matrixDict = matrix;
            _newLineChar = newlinechar;
            _blankChar = blankchar;
        }
        #endregion

        #region Public methods

        public string print()
        {
            if (_matrixArray != null) return printMatrixArray();
            if (_matrix != null) return printMatrixList();
            if (_matrixDict != null) return printMatrixDictionary();

            return string.Empty;
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
            _matrixDict = null;
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
            _matrixDict = null;
            return print();
        }

        /// <summary>
        /// Prints a new matrix with current configuration.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns></returns>
        public string printNewMatrix(Dictionary<byte[], Dictionary<byte[], double>> matrix)
        {
            _matrix = null;
            _matrixArray = null;
            _matrixDict = matrix; 
            return print();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Prints this instance.
        /// </summary>
        /// <returns></returns>
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
        private string printMatrixDictionary()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Rows and columns are switched in this print view");

            int i = 0;

            foreach (KeyValuePair<byte[], Dictionary<byte[],double>> kv in _matrixDict)
            {
                int j = 0;
                int localLen = kv.Value.Count;
                foreach (KeyValuePair<byte[], double> row in kv.Value)
                {
                    output.AppendFormat(string.Format("{0:0.000}", row.Value));

                    if (j + 1 < localLen)
                    {
                        output.Append("," + _blankChar);
                    }
                    j++;
                }
                output.Append(_newLineChar);
                i++;
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

            for (int i = 0; i < _matrix.Count; i++)
            {
                int localLen = _matrix[i].Count;

                for (int j = 0; j < localLen; j++)
                {
                    output.AppendFormat(string.Format("{0:0.000}", _matrix[i][j]));

                    if (j + 1 < localLen)
                    {
                        output.Append("," + _blankChar);
                    }
                }
                output.Append(_newLineChar);
            }
            return output.ToString();
        }

        #endregion
    }
}
