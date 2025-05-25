using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class PlayerMoveControl : MonoBehaviour
    {
        // TODO: sử dụng ScriptableObject để dễ xử lý việc thay đổi giá trị và input thay vì gán trực tiếp (26/5/2025)(trung)

        [Header("Movement")] public float moveSpeed;
        public float groundDrag;
        [Header("Jumping")] public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;
        [Header("Keybindings")] public KeyCode jumpKey = KeyCode.Space;
        [Header("Ground Checking")] public float playerHeight;
        public LayerMask groundLayer;
        bool isGrounded;
        [Header("Input Data")] public Transform orientation;
        float horizontalInput;
        float verticalInput;
        private Vector3 moveDirection;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
        }

        void Update()
        {
            // check ground bằng raycast hướng xuống 
            isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundLayer);
            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f), Color.red);

            ReadRawInput();
            SpeedControl();
            if (isGrounded)
            {
                rb.drag = groundDrag;
                //Debug.Log("Player is ground");
            }
            else
            {
                rb.drag = 0f;
            }
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        // GetAxisRaw vì InputSystem gặp 1 vài bug khi tích hợp mirror-networking (dm unity)
        private void ReadRawInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            // jump logic
            if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
            {
                readyToJump = false; 
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void MovePlayer()
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (isGrounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
            }
            else if (!isGrounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
            }
        }

        // đảm bảo speed của nhân vật ko vượt quá giá trị của var moveSpeed 
        private void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitVelocity.x, rb.velocity.y, limitVelocity.z);
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}