using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    class ResourcesUIProvider : IUIProvider {
        private const string CampUIPath = "UI/CampUI";
        private const string ArsenalItemPath = "UI/ArsenalItem";

        public GameObject CampUI => Resources.Load<GameObject>(CampUIPath);
        public GameObject ArsenalItem => Resources.Load<GameObject>(ArsenalItemPath);
    }
}