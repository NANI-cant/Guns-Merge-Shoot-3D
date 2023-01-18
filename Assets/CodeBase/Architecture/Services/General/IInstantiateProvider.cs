using UnityEngine;

namespace Architecture.Services.General {
    public interface IInstantiateProvider {
        TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation) where TObject : Object;
        TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation, Transform parent) where TObject : Object;
        GameObject InstantiateUI(GameObject template, Vector3 anchoredPosition, Transform parent);
    }
}