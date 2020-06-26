using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mediapipe.HandTracking.Events {
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(CanvasRenderer))]
    public class EventUIHandInteraction : MonoBehaviour {

        private BoxCollider box_collider = null;
        private Rigidbody obj_rigidbody = null;
        private Text text = null;

        [SerializeField]
        private Color color_on_touch_enter = Color.white, color_on_touch_exit = Color.white;
        [SerializeField]
        private UnityEvent on_touch = null;

        private void Awake() {
            box_collider = GetComponent<BoxCollider>();
            obj_rigidbody = GetComponent<Rigidbody>();
            text = GetComponent<Text>();
            box_collider.isTrigger = true;
            obj_rigidbody.useGravity = false;
        }

        private void OnTriggerEnter(Collider other) {
            text.color = color_on_touch_enter;
            on_touch.Invoke();
        }

        private void OnTriggerExit(Collider other) {
            text.color = color_on_touch_exit;
        }
    }
}
