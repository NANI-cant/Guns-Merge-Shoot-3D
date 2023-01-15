using Architecture.Services.AssetProviding;
using Architecture.Services.General;
using UnityEngine;

namespace Architecture.Services.Factories.Impl {
    public class UIFactory : IUIFactory {
        private readonly IUIProvider _uiProvider;
        private readonly IInstantiateProvider _instantiateProvider;

        public UIFactory(
            IUIProvider uiProvider,
            IInstantiateProvider instantiateProvider
        ) {
            _uiProvider = uiProvider;
            _instantiateProvider = instantiateProvider;
        }
        
        public GameObject CreateCampUI() => _instantiateProvider.Instantiate(_uiProvider.CampUI, Vector3.zero, Quaternion.identity);
    }
}