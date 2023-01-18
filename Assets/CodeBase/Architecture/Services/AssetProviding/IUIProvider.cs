using UnityEngine;

namespace Architecture.Services.AssetProviding {
    public interface IUIProvider {
        GameObject CampUI { get; }
        GameObject ArsenalItem { get; }
        GameObject MergeWeapon { get; }
    }
}