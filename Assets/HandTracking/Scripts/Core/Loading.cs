using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mediapipe.HandTracking {

    public class Loading : MonoBehaviour {

        [SerializeField]
        private GameObject text_loading = null;
        [SerializeField]
        private UnityEvent on_loading_done = null;

        private Text text = null;
        private string[] loading = { "Loading", "Loading.", "Loading..", "Loading..." };

        private void Awake() {
            text = text_loading.GetComponent<Text>();
        }

        private void Start() {
            StartCoroutine(LoadAnim());
        }

        private IEnumerator LoadAnim() {
            int temp = 0;
            while (true) {
                yield return new WaitForSeconds(0.5f);
                temp = (temp + 1) % 4;
                text.text = loading[temp];
            }
        }

        private void Update() {
            if (Process.INSTANCE == null) return;
            if (Process.INSTANCE.current_hand == null) return;
            on_loading_done.Invoke();
            gameObject.SetActive(false);
        }
    }
}