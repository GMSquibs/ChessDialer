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
            

            int gridRows = 4;
            int gridColumns = 3;
            string[] gridValues = new string[] { "1", "2", "3",
                                                 "4", "5", "6",
                                                 "7", "8", "9",
                                                 "*", "0", "#",
                                                };
            

            int keyLength = 7; //looking for 7 numbers deep

            //create dialer first to create the phone grid
            Dialer dialer = new Dialer(gridRows, gridColumns, gridValues);
            
            Dictionary<PieceType, long> counts = new Dictionary<PieceType, long>();
            string[] keysToOmit = new string[] { "*", "#" };

            ChessPiece[] pieces = new ChessPiece[]
            {
                new ChessPiece(PieceType.Knight, dialer.PhoneGrid),
                new ChessPiece(PieceType.Rook, dialer.PhoneGrid),
                new ChessPiece(PieceType.Pawn, dialer.PhoneGrid),
                new ChessPiece(PieceType.Queen, dialer.PhoneGrid),
                new ChessPiece(PieceType.King, dialer.PhoneGrid),
                new ChessPiece(PieceType.Bishop, dialer.PhoneGrid)
            };
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
