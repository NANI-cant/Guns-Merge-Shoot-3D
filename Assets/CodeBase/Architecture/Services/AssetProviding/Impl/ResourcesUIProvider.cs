using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    class ResourcesUIProvider : IUIProvider {
        private const string CampUIPath = "UI/CampUI";
        private const string ArsenalItemPath = "UI/ArsenalItem";
        private const string MergeWeaponPath = "UI/MergeWeapon";

        public GameObject CampUI => Resources.Load<GameObject>(CampUIPath);
        public GameObject ArsenalItem => Resources.Load<GameObject>(ArsenalItemPath);
        public GameObject MergeWeapon => Resources.Load<GameObject>(MergeWeaponPath);
    }
}