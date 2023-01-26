using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud {
    [RequireComponent(typeof(RectTransform))]
    public class WayPointProgressView: MonoBehaviour {
        [SerializeField] private Image _progressImage;

        public float Progress {
            get => _progressImage.fillAmount;
            set => _progressImage.fillAmount = value;
        }
    }
}