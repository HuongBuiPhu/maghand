using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxScript : MonoBehaviour {

    private const float DEFAULT_LENGTH_TIME = 1.0f;

    public const string TAG = "Box";

    private Material material = null;

    public float speed = 1.0f;
    public Action boom_action = null, bonus_score_action = null, minus_life_action = null;

    private void Awake() {
        this.material = GetComponent<Renderer>().material;
    }

    private void FixedUpdate() {
        transform.Translate(0, 0, -Time.deltaTime * speed, Space.Self);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Hand") {
            if (null != bonus_score_action) bonus_score_action();
            if (null != boom_action) boom_action();
        }
        if (other.gameObject.tag == "Reuse") {
            if (null != minus_life_action) minus_life_action();
            if (null != boom_action) boom_action();
        }
    }

    public void RandomState() {
        float x, y, z;
        x = Random.Range(-0.7f, 0.7f);
        y = Random.Range(0.0f, 0.2f);
        z = Random.Range(-1.0f, 1.0f);
        Color color = Color.HSVToRGB(Random.Range(0, 1.0f / 3.0f), 1, 1, true);
        material.color = color;
        material.SetColor("_EmissionColor", color);
        transform.localPosition = new Vector3(x, y, 5);
    }

    public void Clear() {
        speed = 0;
        boom_action = null;
        bonus_score_action = null;
        minus_life_action = null;
    }
}