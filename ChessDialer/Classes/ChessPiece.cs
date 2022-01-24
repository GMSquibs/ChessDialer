using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDialer.Classes
{
    public class ChessPiece: IChessPiece
    {
        public Dictionary<int, int[]> MovementMatrix { get; set; }
        public PieceType PieceType { get; set; }

        public ChessPiece(PieceType pType)
        {
            PieceType = pType;
            GenerateMovementMatrix(PieceType);
        }

        private void GenerateMovementMatrix(PieceType pieceType)
        {
            //did not include * and # in the move matrix as these were considered invalid movements.
            //generate movement matrix based on the piece.
            //Dictionary key is the "pressed" or "dialed" number, value is array of possible next moves
            switch (pieceType)
            {
                case PieceType.Rook:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {2,5,8} },
                        {1, new int[] {2,3,4,7} },
                        {2, new int[] {1,3,5,8,0} },
                        {3, new int[] {1,2,6,9} },
                        {4, new int[] {1,5,6,7} },
                        {5, new int[] {2,4,6,8,0} },
                        {6, new int[] {3,4,5,9} },
                        {7, new int[] {1,4,8,9} },
                        {8, new int[] {2,5,7,9} },
                        {9, new int[] {3,6,7,8} }
                    };
                    break;
                case PieceType.Knight:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {4,6} },
                        {1, new int[] {6,8} },
                        {2, new int[] {7,9} },
                        {3, new int[] {4,8} },
                        {4, new int[] {0,3,9} },
                        {5, new int[] {} },
                        {6, new int[] {0,1,7} },
                        {7, new int[] {2,6} },
                        {8, new int[] {1,3} },
                        {9, new int[] {2,4} }
                    };
                    break;
                case PieceType.Pawn:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {8} },
                        {1, new int[] {} },
                        {2, new int[] {} },
                        {3, new int[] {} },
                        {4, new int[] {1} },
                        {5, new int[] {2} },
                        {6, new int[] {3} },
                        {7, new int[] {4} },
                        {8, new int[] {5} },
                        {9, new int[] {6} }
                    };
                    break;
                case PieceType.Bishop:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {7,9} },
                        {1, new int[] {5,9} },
                        {2, new int[] {4,6} },
                        {3, new int[] {5,7} },
                        {4, new int[] {2,8} },
                        {5, new int[] {1,3,7,9} },
                        {6, new int[] {2,8} },
                        {7, new int[] {3,5,0} },
                        {8, new int[] {4,6} },
                        {9, new int[] {1,5,0} }
                    };
                    break;
                case PieceType.Queen:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {2,5,7,8,9} },
                        {1, new int[] {2,3,4,5,7} },
                        {2, new int[] {1,3,4,5,6,8,0} },
                        {3, new int[] {1,2,5,6,7,9} },
                        {4, new int[] {1,2,5,6,7,8} },
                        {5, new int[] {1,2,3,4,6,7,8,9,0} },
                        {6, new int[] {3,4,5,8,9} },
                        {7, new int[] {1,3,4,5,8,9,0} },
                        {8, new int[] {2,4,5,6,7,9,0} },
                        {9, new int[] {1,3,5,6,7,8,0} }
                    };
                    break;
                case PieceType.King:
                    MovementMatrix = new Dictionary<int, int[]>()
                    {
                        {0, new int[] {7,8,9} },
                        {1, new int[] {2,4,5} },
                        {2, new int[] {1,3,4,5,6} },
                        {3, new int[] {2,5,6} },
                        {4, new int[] {1,2,5,7,8} },
                        {5, new int[] {1,2,3,4,6,7,8,9} },
                        {6, new int[] {2,3,5,8,9} },
                        {7, new int[] {4,5,8,0} },
                        {8, new int[] {4,5,6,7,9,0} },
                        {9, new int[] {5,6,8,0} }
                    };
                    break;
                default:
                    throw new NotImplementedException("Unknown Piece Type. Implment functionality for new piece by adding new type to PieceType.cs and developing the MovementMatrix");
            }
        }
    }
}
