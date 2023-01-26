using Architecture.Services.AssetProviding;
using Architecture.Services.General;

namespace Metric.Levels {
    public abstract class WayPointData {
        public abstract StageData[] GetStages(IRandomService randomService, IMetricProvider metricProvider);
    }
}