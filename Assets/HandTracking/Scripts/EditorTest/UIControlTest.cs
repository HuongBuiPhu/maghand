using UnityEngine;

namespace Mediapipe.HandTracking {
    public class UIControlTest : MonoBehaviour {

        public void Prev() {
            if (null != HandTrackingRaw.INSTANCE) {
                HandTrackingRaw.INSTANCE.StopAllCoroutines();
                HandTrackingRaw.INSTANCE.PrevSample();
            }
        }

        public void Next() {
            if (null != HandTrackingRaw.INSTANCE) {
                HandTrackingRaw.INSTANCE.StopAllCoroutines();
                HandTrackingRaw.INSTANCE.NextSample();
            }
        }

        public void Auto() {
            if (null != HandTrackingRaw.INSTANCE) {
                HandTrackingRaw.INSTANCE.StopAllCoroutines();
                HandTrackingRaw.INSTANCE.StartCoroutine(HandTrackingRaw.INSTANCE.AutoSample());
            }
        }
    }

}
