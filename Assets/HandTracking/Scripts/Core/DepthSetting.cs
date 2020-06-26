using UnityEngine;

namespace Mediapipe.HandTracking {
    public abstract class DepthSetting : MonoBehaviour {

        [SerializeField]
        private GameObject process = null, drawing = null, input = null, animation_loading = null;

        protected static DepthEstimate depth_estimate = null;

        public static DepthEstimate GetDepthEstimate() => depth_estimate;

        protected void Awake() {
            process.SetActive(false);
            drawing.SetActive(false);
            input.SetActive(false);
        }

        protected void EnableProcess() {
            process.SetActive(true);
            drawing.SetActive(true);
            input.SetActive(true);
            animation_loading.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}