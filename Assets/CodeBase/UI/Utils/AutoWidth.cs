using UnityEngine;
using UnityEngine.UI;

namespace UI.Utils {
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class AutoWidth:MonoBehaviour {
        private void Start() => Calculate();

        public void Calculate() {
            float width = 0;
            for (int i = 0; i < transform.childCount; i++) {
                width += ((RectTransform) transform.GetChild(i)).sizeDelta.x;
            }

            width += GetComponent<HorizontalLayoutGroup>().spacing * transform.childCount - 1;
            ((RectTransform) transform).sizeDelta = new Vector2(width, ((RectTransform) transform).sizeDelta.y);
        }
    }
}