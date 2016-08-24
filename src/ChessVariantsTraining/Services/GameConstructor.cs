﻿using ChessDotNet;
using ChessDotNet.Variants.Antichess;
using ChessDotNet.Variants.Atomic;
using ChessDotNet.Variants.KingOfTheHill;
using ChessDotNet.Variants.ThreeCheck;
using System;

namespace ChessVariantsTraining.Services
{
    public class GameConstructor : IGameConstructor
    {
        public ChessGame Construct(string variant)
        {
            switch (variant)
            {
                case "Antichess":
                    return new AntichessGame();
                case "Atomic":
                    return new AtomicChessGame();
                case "KingOfTheHill":
                    return new KingOfTheHillChessGame();
                case "ThreeCheck":
                    return new ThreeCheckChessGame();
                default:
                    throw new NotImplementedException("Variant not implemented: " + variant);
            }
        }

        public ChessGame Construct(string variant, string fen)
        {
            switch (variant)
            {
                case "Antichess":
                    return new AntichessGame(fen);
                case "Atomic":
                    return new AtomicChessGame(fen);
                case "KingOfTheHill":
                    return new KingOfTheHillChessGame(fen);
                case "ThreeCheck":
                    return new ThreeCheckChessGame(fen);
                default:
                    throw new NotImplementedException("Variant not implemented: " + variant);
            }
        }
    }
}
