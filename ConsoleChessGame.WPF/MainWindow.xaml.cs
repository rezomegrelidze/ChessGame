using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChessGame.Core;

namespace ChessGame.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Game chessGame;
        Rectangle[,] Squares = new Rectangle[8,8];

        public MainWindow()
        {
            InitializeComponent();
                
            chessGame = new Game();
            DrawBoard();
        }

        private void DrawBoard()
        {
            DrawFileRankBorder();
            DrawSquares();
            DrawPieces();
        }

        private void DrawFileRankBorder()
        {
            DrawRanks();
            DrawFiles();
        }

        private void DrawFiles()
        {
            foreach(var c in Enumerable.Range('a',8).Select(x => (char)x))
            {
                var fileText = new TextBlock
                {
                    Text = c.ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 24,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(fileText, (c -96) - 1);
                Files.Children.Add(fileText);
            }
        }

        private void DrawRanks()
        {
            for (int i = 1; i <= 8; i++)
            {
                var rankText = new TextBlock
                {
                    Text = i.ToString(),Foreground = Brushes.Black,
                    FontSize = 24,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetRow(rankText, i-1);
                Ranks.Children.Add(rankText);
            }
        }

        private void DrawPieces()
        {
            foreach (var piece in chessGame.Board.Pieces)
            {
                var imagePath = piece.ImagePath();
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath)),
                    Tag = piece
                };
                Grid.SetRow(image, piece.CurrentSquare.File);
                Grid.SetColumn(image, piece.CurrentSquare.Rank);
                Panel.SetZIndex(image,0);
                image.MouseLeftButtonDown += PiecePressed;
                ChessBoard.Children.Add(image);
            }
        }

        private void PiecePressed(dynamic sender, MouseButtonEventArgs e)
        {
            if (chessGame.ChosenPiece != null)
            {
                if (sender.Tag is Piece capturePiece && capturePiece.Color != chessGame.ChosenPiece.Color)
                {
                    // make capture move if it's a legal move for a given piece
                    if (chessGame.ChosenPiece.AvailableMoves(chessGame.Board).Contains(new Move()
                    {
                        InitialSquare = chessGame.ChosenPiece.CurrentSquare, DestinationSquare =
                            capturePiece.CurrentSquare
                    }))
                    {
                        chessGame.MakeCaptureMove(chessGame.ChosenPiece,capturePiece);
                        chessGame.ChosenPiece = null;
                    }
                }
            }
            else
            {
                chessGame.ChosenPiece = sender.Tag as Piece;
                HighlightAvailableMoves();
            }
        }

        private void HighlightAvailableMoves()
        {
            Squares[chessGame.ChosenPiece.CurrentSquare.File,chessGame.ChosenPiece.CurrentSquare.Rank].Fill = Brushes.Yellow;
            foreach (var move in chessGame.ChosenPiece.AvailableMoves(chessGame.Board).ToList())
            {
                Squares[move.DestinationSquare.File, move.DestinationSquare.Rank].Fill = Brushes.Yellow;
            }
        }

        private void DrawSquares()
        {
            SquareColor squareColor = SquareColor.White;
            for(int file = 0; file < 8;file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    var square = chessGame.Board.Squares[file, rank];
                    var rectangle = new Rectangle()
                    {
                        Fill = squareColor == SquareColor.White ? Brushes.Bisque : Brushes.Red,
                        Tag = square
                    };
                    Squares[square.File, square.Rank] = rectangle;
                    Grid.SetRow(rectangle, square.File);
                    Grid.SetColumn(rectangle, square.Rank);
                    ChessBoard.Children.Add(rectangle);
                    rectangle.MouseLeftButtonDown += MakeMoveToGivenSquare;
                    squareColor = squareColor == SquareColor.Black ? SquareColor.White : SquareColor.Black;
                }
                squareColor = squareColor == SquareColor.Black ? SquareColor.White : SquareColor.Black;
            }
        }

        private void MakeMoveToGivenSquare(dynamic sender, MouseButtonEventArgs e)
        {
            if (chessGame.ChosenPiece != null)
            {

                var destinationSquare = (Square)sender.Tag;
                var move = new Move()
                {
                    InitialSquare = chessGame.ChosenPiece.CurrentSquare,
                    DestinationSquare = destinationSquare
                };
                if(!chessGame.ChosenPiece.AvailableMoves(chessGame.Board).Contains(move)) return;
                chessGame.MakeMove(chessGame.ChosenPiece,move);
            }

            chessGame.ChosenPiece = null;
            DrawBoard();
        }
    }

    public static class PieceExtensions
    {
        public static string ImagePath(this Piece piece)
        {
            var path = $@"pack://application:,,,/Images/{(piece.Color == PieceColor.White ? "white" : "black")}_";
            switch (piece)
            {
                case Pawn _: return path + "pawn.png";
                case Rook r: return path + "rook.png";
                case King kg: return path + "king.png";
                case Knight kn: return path + "knight.png";
                case Bishop _: return path + "bishop.png";
                case Queen q: return path + "queen.png";
            }

            return "";
        }
    }
}
