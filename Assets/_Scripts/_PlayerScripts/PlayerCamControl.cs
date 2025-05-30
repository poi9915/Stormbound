using UnityEngine;

namespace _Scripts._PlayerScripts
{
    public class PlayerCamControl : MonoBehaviour
    {
        // TODO : Tích hợp vào trong PlayerMoveControl để dễ xử lý khi tích hợp mirror-networking(25/5/2025)(trung)
        // 30/5/25 : ko cần phải tích hợp nữa 
        public float sensX;
        public float sensY;
        public Transform orientation;
        public Transform body;
        float xRotation, yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float moveX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float moveY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += moveX;
            xRotation -= moveY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
            body.rotation = orientation.rotation;
        }
    }
}