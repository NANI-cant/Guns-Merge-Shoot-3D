using System;
using Metric.Levels;

namespace Architecture.Services.Gameplay {
    public interface ILevelProgressService {
        event Action Modified;
        
        WayPointData WayPointData { get; }
        bool IsLevelOver { get; }
        int PointsCount { get; }
        float LevelProgress { get; }
        float PointProgress { get; }
        int CurrentPoint { get; }
        void NextLevel();
        void NextPoint();
        void ResetLevel();
    }
}