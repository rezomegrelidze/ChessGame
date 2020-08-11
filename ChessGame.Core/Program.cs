using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessGame.Core
{

    public class Pawn : Piece
    {

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            if (Color == PieceColor.White)
            {
                if (CurrentSquare.File == 6)
                {
                    yield return new Move()
                    {
                        InitialSquare = CurrentSquare, DestinationSquare = new Square(CurrentSquare.File - 1,
                            CurrentSquare.Rank)
                    };
                    yield return new Move()
                    {
                        InitialSquare = CurrentSquare,
                        DestinationSquare = new Square(CurrentSquare.File -2,
                            CurrentSquare.Rank)
                    };
                }
                else
                {
                    yield return new Move()
                    {
                        InitialSquare = CurrentSquare,
                        DestinationSquare = new Square(CurrentSquare.File - 1,
                            CurrentSquare.Rank)
                    };
                }

            }
        }

        public Pawn(Square square, PieceColor color) : base(square, color)
        {
        }

        public override string ToString() => $"P{(Color == PieceColor.Black ? "B" : "W")}";
    }

    public class Rook : Piece
    {
        public Rook(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"R{(Color == PieceColor.Black ? "B" : "W")}";
    }

    public class Bishop : Piece
    {
        public Bishop(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"B{(Color == PieceColor.Black ? "B" : "W")}";
    }

    public class Knight : Piece
    {
        public Knight(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"K{(Color == PieceColor.Black ? "B" : "W")}";
    }

    public class King : Piece
    {
        public King(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"Kg{(Color == PieceColor.Black ? "B" : "W")}";
    }

    public class Queen : Piece
    {
        public Queen(Square square, PieceColor color) : base(square, color)
        {
        }

        public override IEnumerable<Move> AvailableMoves(Board board)
        {
            throw new NotImplementedException();
        }

        public override string ToString() => $"Q{(Color == PieceColor.Black ? "B" : "W")}";
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
        public abstract IEnumerable<Move> AvailableMoves(Board board);
    }

    public struct Move
    {
        public Square InitialSquare { get; set; }
        public Square DestinationSquare { get; set; }

        public bool IsCaptureMove { get; set; }
        public bool IsCheck { get; set; }
        public bool IsCastling { get; set; }
    }

    public struct Square
    {
        public bool Equals(Square other)
        {
            return Rank == other.Rank && File == other.File;
        }

        public override bool Equals(object obj)
        {
            return obj is Square other && Equals(other);
        }


        public Square(int file, int rank) => (File, Rank) = (file, rank);

        public Square(string chessPosition)
        {
            Rank = char.Parse(chessPosition[0].ToString().ToUpper()) - 65;
            File = int.Parse(chessPosition[1].ToString()) -1;
        }

        public int Rank { get; }
        public int File { get; }


        public static bool operator ==(Square square1, Square square2)
        {
            return square1.Rank == square2.Rank && square1.File == square2.File;
        }

        public static bool operator ==(Square square1, (int rank,int file) square2)
        {
            return square1.Rank == square2.rank && square1.File == square2.file;
        }

        public static bool operator !=(Square square1, (int rank, int file) square2)
        {
            return !(square1 == square2);
        }

        public static bool operator !=(Square square1, Square square2)
        {
            return !(square1 == square2);
        }

        public override string ToString() => $"{('A' + Rank)}{File + 1}";
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
            AddPieces();
        }

        private void BuildBoard()
        {
            Squares = new Square[8,8];
            
            var squareColor = SquareColor.White;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares[i, j] = new Square(i, j);
                    squareColor = squareColor == SquareColor.Black ? SquareColor.White : SquareColor.Black;
                }
                squareColor = squareColor == SquareColor.Black ? SquareColor.White : SquareColor.Black;
            }
        }

        private void AddPieces()
        {
            Pieces = new List<Piece>();

            // Pawns6,
            for (int i = 0; i < 8; i++)
            {
                Pieces.AddRange(new[] { new Pawn(Squares[1, i], PieceColor.Black), new Pawn(Squares[6, i], PieceColor.White) });
            }
            //  Pieces
            var rooks = new[]   {new Rook(Squares[7, 7], PieceColor.White), new Rook(Squares[7, 0], PieceColor.White)
                ,   new Rook(Squares[0, 0], PieceColor.Black), new Rook(Squares[0, 7], PieceColor.Black) };
            var knights = new[] {new Knight(Squares[0, 1], PieceColor.Black), new Knight(Squares[0, 6], PieceColor.Black),
                new Knight(Squares[7, 1], PieceColor.White), new Knight(Squares[7, 6], PieceColor.White) };
            var bishops = new[]
            {
                new Bishop(Squares[0, 2], PieceColor.Black), new Bishop(Squares[0, 5], PieceColor.Black),
                new Bishop(Squares[7, 2], PieceColor.White), new Bishop(Squares[7, 5], PieceColor.White)
            };

            var kings = new[] { new King(Squares[0, 4], PieceColor.Black), new King(Squares[7, 4], PieceColor.White) };
            var queens = new[] { new Queen(Squares[0, 3], PieceColor.Black), new Queen(Squares[7, 3], PieceColor.White) };

            Pieces.AddRange((new Piece[][] { rooks, knights, bishops, kings, queens }).SelectMany(l => l));
        }

        public List<Piece> Pieces { get; set; }

        public Square this[int file, int rank] => Squares[file, rank];
    }

    public class Player
    {
        public string Name { get; set; }
        public PieceColor Side { get; set; }
    }
}
