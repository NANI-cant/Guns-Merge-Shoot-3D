using System;
using Metric.Levels;
using Metric.Levels.Stages;

namespace Architecture.Services.Gameplay {
    public interface ILevelProgressService {
        event Action Modified;
        
        StageData ActiveStage { get; }
        StageData[] Stages { get; }
        float LevelProgress { get; }
        bool IsLevelOver { get; }
        
        void ResetLevel();
        void NextLevel();
        void NextStage();
        void TranslateToNextStage(Action callback);
    }
}