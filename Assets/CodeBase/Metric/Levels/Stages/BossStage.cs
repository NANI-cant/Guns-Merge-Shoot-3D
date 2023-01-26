using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Metric.Levels.Stages {
    [CreateAssetMenu(fileName = "BossStage", menuName = "Levels/Stages/Boss")]
    public class BossStage : Stage {
        public override StageData GetStageData(EnemyId[] availableEnemies, int level, IRandomService service, IMetricProvider provider) {
            return new StageData(new EnemyId[] {EnemyId.Boss}, Image);
        }
    }
}