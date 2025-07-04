using UnityEngine;

namespace _Scripts._PlayerScripts {
    public class PlayerCamControl : MonoBehaviour {
        public float sensX = 100f, sensY = 100f;
        public float smoothing = 10f;
        public Transform orientation, body;
        float xRotation = 0f, yRotation = 0f;
        float smoothX = 0f, smoothY = 0f;

        void Start() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update() {
            float rawX = Input.GetAxisRaw("Mouse X") * sensX;
            float rawY = Input.GetAxisRaw("Mouse Y") * sensY;

            smoothX = Mathf.Lerp(smoothX, rawX, smoothing * Time.deltaTime);
            smoothY = Mathf.Lerp(smoothY, rawY, smoothing * Time.deltaTime);

            yRotation += smoothX;
            xRotation -= smoothY;
            xRotation = Mathf.Clamp(xRotation, -18f, 90f);
        }

        void LateUpdate() {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
            body.rotation = orientation.rotation;
        }
    }
}