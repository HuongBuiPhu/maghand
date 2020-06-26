using System.Collections;
using UnityEngine;

namespace Mediapipe.HandTracking {
    public class HandTrackingRaw : MonoBehaviour {

        private int samples_size;
        private int current_index_sample = 0;

        protected Hand current_hand;

        public static HandTrackingRaw INSTANCE { get; private set; }

        private void Awake() {
            INSTANCE = this;
        }

        private void Start() {
            samples_size = FakeData.Samples.Length / 63;
            StartCoroutine(AutoSample());
        }

        private void FixedUpdate() {
            if (current_index_sample > samples_size - 1) current_index_sample = 0;
            if (current_index_sample < 0) current_index_sample = samples_size - 1;
        }

        public void NextSample() {
            current_index_sample++;
        }

        public void PrevSample() {
            current_index_sample++;
        }

        public IEnumerator AutoSample() {
            while (true) {
                yield return new WaitForSeconds(0.2f);
                current_index_sample++;
            }
        }

        public Vector3 GetPosition() {
            if (null == current_hand) return Vector3.zero;
            return current_hand.Position;
        }

        public Vector3 GetFingerLandmark(int index) {
            if (null == current_hand) return Vector3.zero;
            return current_hand.GetLandmark(index);
        }
    }
}