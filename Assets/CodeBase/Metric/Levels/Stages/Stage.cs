using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Metric.Levels.Stages {
    public abstract class Stage: ScriptableObject {
        [SerializeField] private Sprite _image;

        public Sprite Image => _image;
        
        public abstract StageData GetStageData(EnemyId[] availableEnemies, int level, IRandomService service, IMetricProvider provider);
    }
}