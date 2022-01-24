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
 
        public Dialer()
        {
            
        }

        // only need to go as deep as phone number but could change in the future
        // starting at i == 2 as we cannot start phone numbers with 0 or 1
        // string array of omit items will allow user omit numbers of other items if the grid gets expanded.
        public long DialCounts(int _keyedInputCount , ChessPiece piece, string[] omitItems)
        {
            return GenerateCount(_keyedInputCount, piece.MovementMatrix);
        }        

        //_keyedInputCount equals how many digits we are needing to dive down too.
        private long GenerateCount(int _keyedInputCount, Dictionary<int,int[]> map)
        {
            long[,] matrix = new long[map.Count, map.Count];

            //fill matrix 
            for (int i = 0; i < map.Count; i++)
            {
                matrix[1, i] = 1;
            }

            //start at j==2 as problem indicates cannot start with 0 or 1 so we only want to return the result for numbers starting
            //from 2-9
            for (int j = 2; j < _keyedInputCount + 1; j++)
            {
                for (int x = 0; x < map.Count; x++)
                {
                    foreach (var item in map[x])
                    {
                        matrix[j,x] += matrix[j - 1,item];
                    }
                }
                
            }

            long result = 0;
            
            for (int z = 0; z < map.Count; z++)
            {
                result += matrix[_keyedInputCount,z];
            }
            return result;
        }

        #region non-functioning recursive method
        //recurseive method to generate a 7 digit number where n == depth of recursion. This method is not working at this time.
        //private long GenerateNumber(int start, int pos, int n, Dictionary<int,int[]> map, string[] omitItems)
        //{
        //    if (pos == n)
        //    {
        //        return 1;
        //    }            

        //    foreach(var path in map[start])
        //    {
        //        _dialCount = (_dialCount + GenerateNumber(path, pos + 1, n, map, omitItems));               
        //    }
        //    return map[start][pos];
        //}
        #endregion

    }
}
