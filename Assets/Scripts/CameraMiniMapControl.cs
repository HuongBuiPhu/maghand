using UnityEngine;

public class CameraMiniMapControl : MonoBehaviour {

    [SerializeField]
    private Transform player_tranform = null;

    private void Start() {
        if (null != player_tranform) this.transform.parent = player_tranform.parent;
    }

    // Update is called once per frame
    void Update() {
        if (null != player_tranform) {
            this.transform.position = new Vector3(player_tranform.position.x, 0, player_tranform.position.z);
            this.transform.rotation = Quaternion.Euler(0, player_tranform.rotation.eulerAngles.y, 0);
        }
    }
}
