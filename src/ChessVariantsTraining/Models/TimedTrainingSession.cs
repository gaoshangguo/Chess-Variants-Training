using ChessDotNet;
using ChessVariantsTraining.Services;
using System;

namespace ChessVariantsTraining.Models
{
    public class TimedTrainingSession
    {
        public string SessionID { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime EndsAt { get; private set; }
        public string CurrentFen { get; set; }
        public ChessGame AssociatedGame { get; set; }
        TrainingPosition currentPosition = null;
        public bool Ended
        { 
            get
            {
                return DateTime.UtcNow >= EndsAt;
            }
        }
        public bool RecordedInDb { get; set; }
        public TimedTrainingScore Score { get; set; }
        public bool AutoAcknowledegable
        {
            get
            {
                return DateTime.UtcNow >= EndsAt + new TimeSpan(0, 1, 0);
            }
        }
        public string Variant { get; set; }
        public string[] CurrentLastMoveToDisplay { get; set; }
        public string Type { get; set; }

        IGameConstructor gameConstructor;

        public TimedTrainingSession(string sessionId, DateTime startedAt, DateTime endsAt, int? owner, string type, string variant, IGameConstructor _gameConstructor)
        {
            SessionID = sessionId;
            StartedAt = startedAt;
            EndsAt = endsAt;
            RecordedInDb = false;
            Score = new TimedTrainingScore(0, type, owner, DateTime.UtcNow, Variant);
            Variant = variant;
            Type = type;

            gameConstructor = _gameConstructor;
        }

        public bool VerifyMove(string origin, string destination, string promotion)
        {
            bool correctMove = false;
            if (promotion != null && promotion.Length != 1)
            {
                return false;
            }
            MoveType moveType = AssociatedGame.MakeMove(new Move(origin, destination, AssociatedGame.WhoseTurn, promotion?[0]), false);
            if (moveType != MoveType.Invalid)
            {
                if (Type == "forcedCaptureAntichess")
                {
                    correctMove = true;
                }
                else
                {
                    correctMove = AssociatedGame.IsWinner(ChessUtilities.GetOpponentOf(AssociatedGame.WhoseTurn));
                }
            }
            else
            {
                correctMove = false;
            }
            if (correctMove)
            {
                Score.Score++;
            }
            return correctMove;
        }

        public void SetPosition(TrainingPosition position)
        {
            currentPosition = position;
            AssociatedGame = gameConstructor.Construct(Variant, position.FEN);
            CurrentFen = position.FEN;
            CurrentLastMoveToDisplay = position.LastMove;
        }

        public void RetryCurrentPosition()
        {
            SetPosition(currentPosition);
        }
    }
}