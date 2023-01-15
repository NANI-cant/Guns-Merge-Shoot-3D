using Metric;
using UnityEngine;

namespace Gameplay.Utils {
    public class TeamHolder: MonoBehaviour {
        [field: SerializeField] public Team Team { get; private set; }
    }
}