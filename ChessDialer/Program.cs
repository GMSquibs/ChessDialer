using ChessDialer.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDialer
{
    class Program
    {
        static void Main(string[] args)
        {
            //create pieces for testing
            ChessPiece[] pieces = new ChessPiece[]
            {
                new ChessPiece(PieceType.Knight),
                new ChessPiece(PieceType.Rook),
                new ChessPiece(PieceType.Pawn),
                new ChessPiece(PieceType.Queen),
                new ChessPiece(PieceType.King),
                new ChessPiece(PieceType.Bishop)
            };
            

            int keyLength = 7; //looking for 7 numbers deep

            Dialer dialer = new Dialer();

            Dictionary<PieceType, long> counts = new Dictionary<PieceType, long>();
            string[] keysToOmit = new string[0];

            foreach (ChessPiece piece in pieces)
            {
                if (!counts.ContainsKey(piece.PieceType))
                { 
                    counts[piece.PieceType] = dialer.DialCounts(keyLength, piece, keysToOmit);
                } 
            }

            using (StreamWriter sw = new StreamWriter($@"..\ChessDialerResults.txt",true, Encoding.ASCII))
            {
                sw.WriteLine("PieceType, Count");
                foreach (var kvp in counts)
                {
                    sw.WriteLine($@"{kvp.Key}, {kvp.Value} ");
                }
            }
        }
    }
}
