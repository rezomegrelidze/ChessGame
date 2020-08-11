using System;
using System.Linq;

namespace ChessGame.Core
{
    //TODO: Complete the availableMoves logic for each piece. 
    public class Game
    {
        public Board Board { get; set; }
        public Player PlayerWhite { get; set; }
        public Player PlayerBlack { get; set; }


        public Piece ChosenPiece { get; set; }
        public Player CurrentPlayer { get; set; }

        public Game(string whiteName = "white",string blackName = "black")
        {
            Board = new Board();
            PlayerWhite = new Player()
            {
                Side = PieceColor.White
            };
            PlayerBlack = new Player()
            {
                Side = PieceColor.Black
            };
            CurrentPlayer = PlayerWhite;
        }

        

        public void PrintGame()
        {
            
            for (int rank = 7; rank >= 0; rank--)
            {
                Console.Write((char)('A' + rank) + "\t");
                for (int file = 7; file >= 0; file--)
                {
                    if (Board.Pieces.Any(p => p.CurrentSquare == (rank, file)))
                    {
                        Console.Write($"[{Board.Pieces.Single(p => p.CurrentSquare == (rank, file))}]\t");
                    }
                    else
                    {
                        Console.Write("[ ]\t");
                    }
                }

                Console.WriteLine();
            }
            Console.Write("\t");
            for(int i = 1;i <= 8;i++)
                Console.Write(i + "\t");

        }

        public void MakeMove(Piece chosenPiece, Move move)
        {
            chosenPiece.CurrentSquare = move.DestinationSquare;
        }

        public void MakeCaptureMove(Piece chosenPiece, Piece capturePiece)
        {
            chosenPiece.CurrentSquare = capturePiece.CurrentSquare;
            Board.Pieces.Remove(capturePiece);
            // TODO: Maybe add logic to show how many pieces given player has captured..
        }
    }
}