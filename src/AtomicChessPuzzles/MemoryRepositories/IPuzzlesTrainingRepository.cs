﻿using AtomicChessPuzzles.Models;

namespace AtomicChessPuzzles.MemoryRepositories
{
    public interface IPuzzlesTrainingRepository
    {
        void Add(PuzzleDuringTraining puzzle);

        PuzzleDuringTraining Get(string id, string trainingSessionId);

        void Remove(string id, string trainingSessionId);

        bool Contains(string id, string trainingSessionId);

        bool ContainsTrainingSessionId(string trainingSessionId);
    }
}
