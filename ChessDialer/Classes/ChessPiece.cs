using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDialer.Classes
{
    public class ChessPiece: IChessPiece
    {
        public Dictionary<string, HashSet<string>> MovementMatrix { get; set; }
        public PieceType PieceType { get; set; }
        public string[,] PlayGrid { get; set; }
        

        public ChessPiece(PieceType pType, string[,] playGrid)
        {
            PieceType = pType;
            PlayGrid = playGrid;
            GenerateMovementMatrix(PieceType, PlayGrid);
        }

        private void GenerateMovementMatrix(PieceType pieceType, string[,] playGrid)
        {
            //generate movement matrix based on the piece.
            //this will programatically generate all possible moves regardless of the size of the grid passed in

            int maxMovementRange = playGrid.GetLength(0) > playGrid.GetLength(1) ? playGrid.GetLength(0) : playGrid.GetLength(1);
            
            switch (pieceType)
            {
                //move maps are made up of x, y cordinates for movements from a singular position
                //for complex movements this would need to be looked at and modified to allow an array beoynd 2 indexes
                case PieceType.Rook:
                    int[][] moveMap = new int[][]
                        {
                            new int[] {0,1},
                            new int[] {1,0},
                            new int[] {0,-1},
                            new int[] {-1,0}
                        };
                    GetPieceMoves(playGrid, moveMap, maxMovementRange);
                    break;
                case PieceType.Knight:
                    moveMap = new int[][]
                        {
                            new int[] {2,1},
                            new int[] {1,2},
                            new int[] {-1,2},
                            new int[] {-2,1},
                            new int[] {-2,-1},
                            new int[] {-1,-2},
                            new int[] {1,-2},
                            new int[] {2,-1}
                        };
                    GetPieceMoves(playGrid, moveMap,1);
                    break;
                case PieceType.Pawn:
                    moveMap = new int[][]
                        {
                            new int[] {1,0}
                        };
                    GetPieceMoves(playGrid, moveMap,1);
                    break;
                case PieceType.Bishop:
                     moveMap = new int[][]
                         {
                            new int[] {1,1},
                            new int[] {-1,1},
                            new int[] {-1,-1},
                            new int[] {1,-1}
                         };
                    GetPieceMoves(playGrid, moveMap,maxMovementRange);
                    break;
                case PieceType.Queen:
                     moveMap = new int[][]
                         {
                            new int[] {0,1},
                            new int[] {1,0},
                            new int[] {0,-1},
                            new int[] {-1,0},
                            new int[] {1,1},
                            new int[] {-1,1},
                            new int[] {-1,-1},
                            new int[] {1,-1}
                         };
                    GetPieceMoves(playGrid, moveMap,maxMovementRange);
                    break;
                case PieceType.King:
                    moveMap = new int[][]
                         {
                            new int[] {0,1},
                            new int[] {1,0},
                            new int[] {0,-1},
                            new int[] {-1,0}
                         };
                    GetPieceMoves(playGrid, moveMap, 1);
                    break;
                default:
                    throw new NotImplementedException("Unknown Piece Type. Implment functionality for new piece by adding new type to PieceType.cs and developing the MovementMatrix");
            }

        }

        //steps indicate how many times the piece can perform the x,y corodinate movement
        internal void GetPieceMoves(string[,] playGrid, IEnumerable<int[]> movements, int maxMovementSteps)
        {
            int maxRows = playGrid.GetLength(0);
            int maxColumns = playGrid.GetLength(1);

            //Dictionary key is the "pressed" or "dialed" number, value is HashSet of possible next moves. Changed to hashset to accomodate larger grids
            //and modify to strings to allow for alphanumeric characters
            MovementMatrix = new Dictionary<string, HashSet<string>>();

            for (int x = 0; x < maxRows; x++)
            {
                for (int y = 0; y < maxColumns; y++)
                {
                    foreach (var move in movements)
                    {
                        //here, a positive x indicates an upwards movement. if x ==0, and the movement map indicate a move to spaces up,
                        //we obviously cannot do that as it would be outside the bounds of the map. A negative x, indicates downwards movement
                        //so we need to subtract from the current to give us a positive movement result (0 - (-2) = 2) which will increase to x coordinate appropriately
                        
                        var nextX = x - move[0];
                        var nextY = y - move[1];
                        int steps = 1;
                        while(IsValidMove(playGrid, nextX, nextY, maxRows, maxColumns, out string newLocationValue) && steps <= maxMovementSteps)
                        {
                            if (MovementMatrix.ContainsKey(playGrid[x, y]))
                            {
                                MovementMatrix[playGrid[x, y]].Add(newLocationValue);
                            }
                            else
                            {
                                MovementMatrix.Add(playGrid[x, y], new HashSet<string>() { newLocationValue });
                            }
                            //for pieces with more then one step in the movement, reperform movement map until it hits a boundary
                            
                            steps++;
                            nextX = nextX - move[0];
                            nextY = nextY - move[1];
                            
                        }    
                    }
                }
            }
            
        }
        private bool IsValidMove(string[,] board, int nextRow, int nextColumn, int maxRows, int maxColumns, out string locationValue)
        {
            
            locationValue = null;
            //var newRow = currentX - nextX;
            if ((nextRow < 0) || (nextRow >= maxRows)) return false;

            //var newCol = currentY - nextY;
            if ((nextColumn < 0) || (nextColumn >= maxColumns)) return false;

            locationValue = board[nextRow, nextColumn];
            return true;
        }

    }
}
