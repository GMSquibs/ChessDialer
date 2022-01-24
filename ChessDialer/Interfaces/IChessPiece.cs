using ChessDialer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDialer
{
    interface IChessPiece
    {
        Dictionary<int, int[]> MovementMatrix { get;}
        //int Nodes { get; set; } //how many nodes we need to run this process against
        
        PieceType PieceType { get; set; }//this will help with processing later

    }
}
