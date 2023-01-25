using UnityEngine;

namespace UI{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicAspect : MonoBehaviour {
        [SerializeField] private float _width;
        [SerializeField] private float _height;

        private RectTransform _transform;
        private RectTransform _container;

        private float Aspect => _width / _height;
        
        private void Awake() {
            _transform = GetComponent<RectTransform>();
            _container = _transform.parent.GetComponent<RectTransform>();

            Vector3[] containerCorners = new Vector3[4];
            _container.GetWorldCorners(containerCorners);

            var containerWidth = Vector3.Distance(containerCorners[1], containerCorners[2]);
            var containerHeight = Vector3.Distance(containerCorners[0], containerCorners[1]);

            var aspectWidth = containerHeight * Aspect;
            var aspectHeight = containerWidth / Aspect;

            bool shouldMatchWidth = aspectHeight <= containerHeight;

            if (shouldMatchWidth) {
                _transform.sizeDelta = new Vector2(containerWidth, aspectHeight);
            }
            else {
                _transform.sizeDelta = new Vector2(aspectWidth, containerHeight);
            }
        }
    }
}
