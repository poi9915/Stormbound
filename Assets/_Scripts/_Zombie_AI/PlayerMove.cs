// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMove : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     public float gravity = -9.81f;
//     public float jumpHeight = 2f;

//     private CharacterController controller;
//     private Vector3 velocity;
//     private bool isGrounded;

//     public Transform groundCheck;
//     public float groundDistance = 0.4f;
//     public LayerMask groundMask;

//     public Transform camTransform; // 👈 Gán Camera.main.transform ở đây

//     void Start()
//     {
//         controller = GetComponent<CharacterController>();
//     }

//     void Update()
//     {
//         // Kiểm tra chạm đất
//         isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
//         if (isGrounded && velocity.y < 0)
//         {
//             velocity.y = -2f;
//         }

//         // Lấy input
//         float x = Input.GetAxis("Horizontal");
//         float z = Input.GetAxis("Vertical");

//         // 👇 Tính hướng theo camera
//         Vector3 forward = camTransform.forward;
//         Vector3 right = camTransform.right;
//         forward.y = 0f;
//         right.y = 0f;
//         forward.Normalize();
//         right.Normalize();

//         Vector3 move = forward * z + right * x;
//         controller.Move(move * moveSpeed * Time.deltaTime);

//         // Nhảy
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//         }

//         // Trọng lực
//         velocity.y += gravity * Time.deltaTime;
//         controller.Move(velocity * Time.deltaTime);
//     }
// }
