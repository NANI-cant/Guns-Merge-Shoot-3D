using UnityEngine;

namespace Architecture.Services.General.Impl {
    public class UnityInstantiateProvider : IInstantiateProvider {
        public TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation) where TObject : Object 
            => GameObject.Instantiate(template, position, rotation);

        public TObject Instantiate<TObject>(TObject template, Vector3 position, Quaternion rotation, Transform parent) where TObject : Object 
            => GameObject.Instantiate(template, position, rotation, parent);

        public GameObject InstantiateUI(GameObject template, Vector3 anchoredPosition, Transform parent) {
            GameObject element = GameObject.Instantiate(template, Vector3.zero, Quaternion.identity);
            RectTransform rectTransform = element.GetComponent<RectTransform>();
            rectTransform.SetParent(parent, false);
            rectTransform.anchoredPosition = anchoredPosition;

            return element;
        }
    }
}