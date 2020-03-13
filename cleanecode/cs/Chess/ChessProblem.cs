using System;
using System.Linq;

namespace Chess
{
    public class ChessProblem
    {
        private Board board;
        public ChessStatus ChessStatus;

        public void LoadFrom(Board board)
        {
            this.board = board;
        }

        public void CalculateChessStatus(PieceColor color)
        {
            var isCheck = IsCheck(color); 
            var hasMoves = HasMoves(color);
            if (isCheck)
                ChessStatus = hasMoves ? ChessStatus.Check : ChessStatus.Mate;
            else 
                ChessStatus = hasMoves ? ChessStatus.Ok : ChessStatus.Stalemate;
        }

        private bool HasMoves(PieceColor color)
        {
            return board.
                GetPieces(color)
                .Any(HasMovesFrom);
        }

        private bool HasMovesFrom(Location locFrom)
        {
            return board
                .GetPiece(locFrom)
                .GetMoves(locFrom, board)
                .Any(locTo => CanMoveTo(locTo, locFrom));
        }

        private bool CanMoveTo(Location locTo, Location locFrom)
        {
            var color = board.GetPiece(locFrom).Color;
            using (var move = board.PerformTemporaryMove(locFrom, locTo))
            {
                return !IsCheck(color);
            }
        }

        private bool IsCheck(PieceColor color)
        {
            var enemyColor = GetEnemyColor(color);
            return board.GetPieces(enemyColor).Any(CanAttackKing);
        }

        private PieceColor GetEnemyColor(PieceColor color)
        {
            return color == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        private bool CanAttackKing(Location loc)
        {
            var piece = board.GetPiece(loc);
            var color = GetEnemyColor(piece);
            var moves = piece.GetMoves(loc, board);
            return moves.Any(destination => 
                Piece.Is(board.GetPiece(destination), color, PieceType.King));
        }

        private PieceColor GetEnemyColor(Piece piece)
        {
            return piece.Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }


    }

    class Dick : IDisposable
    {
        public static Dick MaxValue()
        {
            return new Dick();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SendPhoto(string to)
        {
            
        }
    }

    class Prog
    {
        void Main()
        {
            using (var dick = Dick.MaxValue())
            {
                dick.SendPhoto("Lera");
            }
        }
    }


}