
using UnityEngine;

public class SmoothTranslate : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
    }
}