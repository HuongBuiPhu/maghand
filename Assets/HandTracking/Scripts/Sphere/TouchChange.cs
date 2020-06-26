using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChange : MonoBehaviour {

    private Color enter_color = Color.red, exit_color = Color.green;

    private Material material;

    private void Awake() {
        material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Hand")) material.SetColor("_EmissionColor", enter_color);
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Hand")) material.SetColor("_EmissionColor", exit_color);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag.Equals("Hand")) material.SetColor("_EmissionColor", enter_color);

    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag.Equals("Hand")) material.SetColor("_EmissionColor", exit_color);
    }
}
