using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChessGame
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class ChessGame
    {
        public Board Board { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public ChessGame()
        {
            AddPieces();
        }

        private void AddPieces()
        {
            Pieces = new List<Piece>();

            // White Pawns
            for (int i = 0; i < 8; i++)
            {
                Pieces.Add(new Pawn(i, i,PieceColor.White));
            }
        }

        public List<Piece> Pieces { get; set; }
    }

    public class Pawn : Piece
    {

        public override IEnumerable<Move> AvailableMoves( ){
            throw new NotImplementedException();
        }

        public Pawn(int rank, int file, PieceColor color) : base(rank, file, color)
        {
        }
    }

    public abstract class Piece
    {

        public Piece(int rank, int file, PieceColor color)
        {
            CurrentSquare = new Square(rank, file);
            Color = color;
        }
        public Square CurrentSquare { get; set; }
        public PieceColor Color { get; }
        public abstract IEnumerable<Move> AvailableMoves();
    }

    public class Move
    {
        public Square InitialSquare { get; set; }
        public Square DestinationSquare { get; set; }

        public bool IsCaptureMove { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCastling { get; set; }
    }

    public struct Square
    {
        public Square(int rank, int file) => (Rank, File) = (rank, file);

        public int Rank { get; }
        public int File { get; }
    }

    public enum PieceColor
    {
        White,Black
    }

    public class Board
    {
        public Square[,] Squares { get; set; }

        public Board()
        {
            BuildBoard();
        }

        private void BuildBoard()
        {
            for(int i = 0;i < 8;i++)
                for(int j = 0;j < 8;j++)
                    Squares[i,j] = new Square(i,j);
        }

        public Square this[int rank, int file] => Squares[rank, file];
    }

    public class Player
    {
        public string Name { get; set; }
        public PieceColor Side { get; set; }
    }
}
