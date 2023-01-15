using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    class ResourcesUIProvider : IUIProvider {
        private const string CampUIPath = "UI/CampUI";

        public GameObject CampUI => Resources.Load<GameObject>(CampUIPath);
    }
}