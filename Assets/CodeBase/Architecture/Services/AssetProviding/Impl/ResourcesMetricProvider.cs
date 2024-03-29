﻿using System.IO;
using System.Linq;
using Metric;
using Metric.Levels;
using Metric.Weapons;
using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    public class ResourcesMetricProvider : IMetricProvider {
        private const string PlayerPath = "Metric/Player";
        private const string EnemiesFolderPath = "Metric/Enemies/";
        private const string LevelPath = "Metric/Levels/Level";
        private const string WeaponPath = "Metric/Weapons";
        private const string DifficultPath = "Metric/Difficult";

        private readonly WeaponData[] _sortedWeapons;

        public PlayerMetric PlayerMetric => Resources.Load<PlayerMetric>(PlayerPath);
        public WeaponData[] WeaponData => _sortedWeapons;
        public DifficultMetric Difficult => Resources.Load<DifficultMetric>(DifficultPath); 
        
        public EnemyMetric EnemyMetric(EnemyId enemyId) => Resources.Load<EnemyMetric>(EnemiesFolderPath + enemyId);

        public ResourcesMetricProvider() {
            _sortedWeapons = Resources.LoadAll<WeaponData>(WeaponPath);
            _sortedWeapons = _sortedWeapons.OrderBy(wd => wd.Level).ToArray();
        }
        
        public Level LevelData(int number) {
            var level = Resources.Load<Level>(LevelPath + number);
            if(level == null) throw new InvalidDataException($"Level {number} does not exist");
            return level;
        }
    }
}