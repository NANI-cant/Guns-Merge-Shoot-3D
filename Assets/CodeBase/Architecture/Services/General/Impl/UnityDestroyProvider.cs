using Architecture.Services.PersistentProgress;
using PersistentProgress;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Architecture.Services.General.Impl {
    public class UnityDestroyProvider : IDestroyProvider {
        private readonly IPersistentProgressService _persistentProgressService;

        public UnityDestroyProvider(IPersistentProgressService persistentProgressService) {
            _persistentProgressService = persistentProgressService;
        }
        
        public void Destroy(GameObject gameObject) {
            UnSubscribeFromProgress(gameObject);
            Object.Destroy(gameObject);   
        }
        
        private void UnSubscribeFromProgress(GameObject gameObject) {
            foreach (var reader in gameObject.GetComponentsInChildren<IProgressReader>(true)) {
                _persistentProgressService.RemoveReader(reader);
            }
            
            foreach (var writer in gameObject.GetComponentsInChildren<IProgressWriter>(true)) {
                _persistentProgressService.RemoveWriter(writer);
            }
        }
    }
}