using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metric.Levels {
    [Serializable]
    public class WaveData {
        [field: SerializeField] public List<Pack> Packs { get; private set; }
    }
}