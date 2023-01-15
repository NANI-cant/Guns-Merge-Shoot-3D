using UnityEngine;
using Object = UnityEngine.Object;

namespace Architecture.Services.General.Impl {
    public class UnityDestroyProvider : IDestroyProvider {
        public void Destroy(GameObject gameObject) => Object.Destroy(gameObject);
    }
}