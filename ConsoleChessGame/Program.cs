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
            Board = new Board();
        }

        

        public void PrintGame()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    
                }

                Console.WriteLine();
            }

        }
    }

    public class Pawn : Piece
    {

        public override IEnumerable<Move> AvailableMoves( )
        {
            throw new NotImplementedException();
        }

        public Pawn(Square square, PieceColor color) : base(square, color)
        {
        }
    }

    public class Rook : Piece
    {
        public Rook(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves()
        {
            throw new NotImplementedException();
        }
    }

    public class Bishop : Piece
    {
        public Bishop(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves()
        {
            throw new NotImplementedException();
        }
    }

    public class Knight : Piece
    {
        public Knight(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves()
        {
            throw new NotImplementedException();
        }
    }

    public class King : Piece
    {
        public King(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves()
        {
            throw new NotImplementedException();
        }
    }

    public class Queen : Piece
    {
        public Queen(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Piece
    {
        protected Piece(Square square, PieceColor color)
        {
            CurrentSquare = square;
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
        public Square(int rank, int file,SquareColor color) => (Rank, File,Color) = (rank, file,color);


        public SquareColor Color { get; }

        public int Rank { get; }
        public int File { get; }

        public static bool operator ==(Square square1, Square square2)
        {
            return square1.Rank == square2.Rank && square1.File == square2.File;
        }

        public static bool operator !=(Square square1, Square square2)
        {
            return !(square1 == square2);
        }
    }

    public enum SquareColor
    {
        White,Black
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
            var squareColor = SquareColor.Black;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares[i, j] = new Square(i, j, squareColor);
                    squareColor = squareColor == SquareColor.Black ? SquareColor.White : SquareColor.Black;
                }
            }
        }

        private void AddPieces()
        {
            Pieces = new List<Piece>();

            // Pawns6,
            for (int i = 0; i < 8; i++)
            {
                Pieces.AddRange(new[] { new Pawn(Squares[1, i], PieceColor.White), new Pawn(Squares[6, i], PieceColor.Black) });
            }
            //  Pieces
            var rooks = new[]   {new Rook(Squares[0, 0], PieceColor.White), new Rook(Squares[0, 7], PieceColor.White)
                ,   new Rook(Squares[7, 0], PieceColor.Black), new Rook(Squares[7, 7], PieceColor.Black) };
            var knights = new[] {new Knight(Squares[0, 1], PieceColor.White), new Knight(Squares[0, 6], PieceColor.White),
                new Knight(Squares[7, 1], PieceColor.Black), new Knight(Squares[7, 6], PieceColor.Black) };
            var bishops = new[]
            {
                new Bishop(Squares[0, 2], PieceColor.White), new Bishop(Squares[0, 5], PieceColor.White),
                new Bishop(Squares[7, 2], PieceColor.Black), new Bishop(Squares[7, 5], PieceColor.Black)
            };

            var kings = new[] { new King(Squares[0, 4], PieceColor.White), new King(Squares[7, 4], PieceColor.Black) };
            var queens = new[] { new Queen(Squares[0, 3], PieceColor.White), new Queen(Squares[7, 3], PieceColor.Black) };

            Pieces.AddRange((new Piece[][] { rooks, knights, bishops, kings, queens }).SelectMany(l => l));
        }

        public List<Piece> Pieces { get; set; }

        public Square this[int rank, int file] => Squares[rank, file];
    }

    public class Player
    {
        public string Name { get; set; }
        public PieceColor Side { get; set; }
    }
}
