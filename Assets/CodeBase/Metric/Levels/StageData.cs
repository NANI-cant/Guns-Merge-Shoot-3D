namespace Metric.Levels {
    public readonly struct StageData {
        public EnemyId[] Enemies { get; }
        public float Delay { get; }

        public StageData(float delay, EnemyId[] enemies) {
            Delay = delay;
            Enemies = enemies;
        }
    }
}