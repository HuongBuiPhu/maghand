using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Mediapipe.HandTracking.ARCore {
    public class ARCoreDepthSetting : DepthSetting {

        [SerializeField]
        private GameObject des_ray_on_plane = null;
        [SerializeField]
        private ARRaycastManager raycast_manager = null;

        private List<ARRaycastHit> out_hits = new List<ARRaycastHit>();
        private Pose current_pose = default;
        private float current_distane = default;

        private new void Awake() {
            // cài đặt phương pháp ước lượng độ sâu
            depth_estimate = new DepthEstimate();
            base.Awake();
        }

        public void FixedUpdate() {
            if (raycast_manager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), out_hits, TrackableType.PlaneWithinPolygon)) {
                current_pose = out_hits[0].pose;
                current_distane = out_hits[0].distance;
                des_ray_on_plane.SetActive(true);
                des_ray_on_plane.transform.position = current_pose.position + Vector3.up * 0.01f;
                des_ray_on_plane.transform.rotation = current_pose.rotation;
            } else {
                des_ray_on_plane.SetActive(false);
            }
        }

        public void SetDepth() {
            if (current_distane == default) return;
            depth_estimate.default_depth = current_distane;
            base.EnableProcess();
        }
    }
}