using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;         // Player
    public Vector3 offset = new Vector3(0f, 3f, -6f);
    public float smoothSpeed = 5f;

    public float rotateSpeed = 5f;
    private float mouseX, mouseY;

    public Transform camPivot;       // Empty object đặt ở vị trí xoay quanh player

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // Nhận input chuột để xoay quanh player
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        mouseY = Mathf.Clamp(mouseY, -30f, 60f); // Giới hạn góc nhìn lên xuống

        camPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        // Tính vị trí mới cho camera
        Vector3 desiredPosition = target.position + camPivot.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Luôn nhìn vào player
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
