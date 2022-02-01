using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessDialer.Classes
{
    public class Dialer
    {
        private int _keyedInputCount { get; set; } //nodes represent how deep the tree we need is
        private long _dialCount { get; set; }

        private HashSet<string> phoneNumbers = new HashSet<string>();

        private int _gridRows { get; set; }
        private int _gridColumns { get; set; }

        private string[] _gridValues { get; set; }

        public string[,] PhoneGrid { get; set; }
 
        public Dialer(int rows, int columns, string[] gridValues)
        {
            _gridRows = rows;
            _gridColumns = columns;
            _gridValues = gridValues;
            GeneratePhoneGrid();
        }

        private void GeneratePhoneGrid()
        {
            if (_gridValues.Length != _gridRows * _gridColumns)
            {
                throw new Exception("There is too many or not enough values for the size of the grid created");
            }

            PhoneGrid = new string[_gridRows,_gridColumns];

            int currentGridValue = 0;

            for (int j = 0; j < _gridRows; j++)
            {
                for (int k = 0; k < _gridColumns; k++)
                {
                    PhoneGrid[j, k] = _gridValues[currentGridValue++];
                }
            }
            
        }


        // only need to go as deep as phone number but could change in the future
        // starting at i == 2 as we cannot start phone numbers with 0 or 1
        // string array of omit items will allow user omit numbers of other items if the grid gets expanded.
        public long DialCounts(int _keyedInputCount , ChessPiece piece, string[] omitItems)
        {
            return GenerateCount(_keyedInputCount,piece.PlayGrid, piece.MovementMatrix, omitItems);
        }        

        //_keyedInputCount equals how many digits we are needing to dive down too.
        private long GenerateCount(int _keyedInputCount,string[,] phoneGrid, Dictionary<string,HashSet<string>> map, string[] omitItems)
        {
            long[,] matrix = new long[_keyedInputCount, phoneGrid.Length];
            //we flatten the 2D array so that we can correlate it to the postion on the matrix
            //have thought about refactoring the movement map so that we do not have to flatten the array, but wanting to get out some code for review
            string[] flattendphoneGrid = Flatten2DArray(phoneGrid);

            long count = 0;
            for (int i = 2; i < matrix.GetLength(1); i++)
            {
                if (omitItems.Contains(flattendphoneGrid[i]))
                {
                    continue;
                }
                count = (count = GenerateNumber(_keyedInputCount - 1, flattendphoneGrid[i], map, matrix, omitItems, flattendphoneGrid));
            }
          
            long result = 0;
            
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    result += matrix[x, y];
                }
            }
            return result;
        }


        static string[] Flatten2DArray(string[,] input)
        {
            
            int size = input.Length;
            string[] result = new string[size];

            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            
            return result;
        }

        #region non-functioning recursive method
        //recurseive method to generate a 7 digit number where n == depth of recursion. This method is not working at this time.
        private long GenerateNumber(int n, string startValue, Dictionary<string, HashSet<string>> map, long[,] matrix, string[] omitItems, string[] flattenedPhoneGrid)
        {
            if (n == 0)
            {
                return 1;
            }
            int position = Array.FindIndex(flattenedPhoneGrid, w => w == startValue);
            if (matrix[n, position] != 0 || !map.ContainsKey(startValue))
            {
                return matrix[n, position];
            }

            long count = 0;

            foreach (var path in map[startValue])
            {
                if (omitItems.Contains(path))
                {
                    continue;
                }
                count = (count + GenerateNumber(n - 1, path, map,matrix, omitItems, flattenedPhoneGrid));
            }
            matrix[n, position] = count;
            return count;
        }
        #endregion

    }
}
