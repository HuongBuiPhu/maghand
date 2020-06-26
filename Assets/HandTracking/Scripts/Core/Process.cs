using System.Collections;
using UnityEngine;

namespace Mediapipe.HandTracking {
    public class Process : MonoBehaviour {

        private const int SIZE_RESOLUSTION = 256;

        public static Process INSTANCE { get; private set; }

        public Hand current_hand = null;
        private int hand_log_id, converter_log_id;

        [SerializeField]
        private Orientation orientation = Orientation.PORTRAIT;
        [SerializeField]
        private InputManager frame_input_manager = null;


#if UNITY_ANDROID
        private AndroidJavaObject hand_tracking;
        private HandRect current_hand_rect;
#endif

        private void Awake() {
            INSTANCE = this;
            LandmarkConverter.INSTANCE = LandmarkConverter.Create(orientation);
        }

        // Start is called before the first frame update
        private void Start() {
            hand_log_id = ScreenLog.INSTANCE.RegisterLogID();
            converter_log_id = ScreenLog.INSTANCE.RegisterLogID();
#if UNITY_ANDROID
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentUnityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            hand_tracking = new AndroidJavaObject("com.jackie.mediapipe.HandTracking", currentUnityActivity);
            hand_tracking.Call("setResolution", SIZE_RESOLUSTION);
#endif
            StartCoroutine(Quantization());
        }

        private void Update() {
            string text = "no hand position";
            if (null != current_hand) {
                Vector3 v3 = current_hand.Position;
                text = "hand position: (" + v3.x + ", " + v3.y + ", " + v3.z + ")";
            }
            ScreenLog.INSTANCE.Log(hand_log_id, text);
        }

        private void FixedUpdate() {
#if UNITY_ANDROID
            float[] palm_data = hand_tracking.Call<float[]>("getPalmRect");
            float[] hand_landmarks_data = hand_tracking.Call<float[]>("getLandmarks");

            if (null != palm_data) current_hand_rect = HandRect.ParseFrom(palm_data);

            if (null != hand_landmarks_data) {
                // Hand new_hand = Hand.MakeFrom(hand_landmarks_data, current_hand_rect);
                // if (null == current_hand) current_hand = new_hand;
                // else current_hand = Hand.DeVibrate(current_hand, new_hand);
                current_hand = Hand.MakeFrom(hand_landmarks_data, current_hand_rect);
            }
#endif
        }

        public IEnumerator Quantization() {
            while (true) {
                Debug.Log("Quantization");
                yield return new WaitForEndOfFrame();
                FrameInput image = frame_input_manager.GetFrameInput();
                if (null == image) continue;
                if (!LandmarkConverter.INSTANCE.Valid()) {
                    LandmarkConverter.INSTANCE.SetInput((float)image.width / (float)image.height);
                    LandmarkConverter.INSTANCE.SetOutput((float)Screen.width / (float)Screen.height);
                }
                hand_tracking.Call("setFrame", image.sbyte_array);
                yield return null;
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