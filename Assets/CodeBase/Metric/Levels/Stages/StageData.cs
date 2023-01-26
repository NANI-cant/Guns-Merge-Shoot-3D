using System;
using UnityEngine;

namespace Metric.Levels.Stages {
    public class StageData {
        private float _progress;

        public event Action ProgressModified;
        
        public EnemyId[] EnemyIds { get; }
        public Sprite Image { get; }

        public float Progress {
            get => _progress; 
            set {
                _progress = Mathf.Clamp01(value);
                ProgressModified?.Invoke();
            }
        }

        public StageData(EnemyId[] enemyIds, Sprite image) {
            EnemyIds = enemyIds;
            Image = image;
            Progress = 0;
        }
    }
}