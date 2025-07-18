// using UnityEngine;
// using UnityEngine.AI;
// using System.Collections.Generic;

// public class ZombieAI : MonoBehaviour
// {
//     public NavMeshAgent agent;
//     public Transform targetPlayer;
//     public Transform baseTarget;
//     public Transform[] allPlayers;

//     public float viewRange = 15f;
//     public float viewAngle = 90f;
//     public float attackRange = 2f;
//     public float attackCooldown = 1.5f;
//     public float attackTimer;

//     public LayerMask playerLayer;
//     public LayerMask obstacleLayer;

//     [Header("Utility AI Actions")]
//     public List<ZombieAction> actions;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
//         allPlayers = new Transform[players.Length];
//         for (int i = 0; i < players.Length; i++)
//             allPlayers[i] = players[i].transform;
//     }

//     void Update()
//     {
//         DetectPlayer();

//         ZombieAction bestAction = null;
//         float highest = 0f;

//         foreach (var action in actions)
//         {
//             float score = action.CalculateUtility(this);
//             if (score > highest)
//             {
//                 highest = score;
//                 bestAction = action;
//             }
//         }

//         if (bestAction != null)
//             bestAction.Execute(this);
//     }

//    public void DetectPlayer()
// {
//     foreach (Transform player in allPlayers)
//     {
//         if (player == null || !player.gameObject.activeSelf) continue;

//         Vector3 dir = (player.position - transform.position).normalized;
//         float distance = Vector3.Distance(transform.position, player.position);
//         float angle = Vector3.Angle(transform.forward, dir);

//         if (distance < viewRange && angle < viewAngle / 2)
//         {
//             if (!Physics.Linecast(transform.position, player.position, obstacleLayer))
//             {
//                 PlayerHealth health = player.GetComponent<PlayerHealth>();
//                 if (health != null && health.hp > 0)
//                 {
//                     targetPlayer = player;
//                     return;
//                 }
//             }
//         }
//     }

//     // Không thấy ai → xoá target
//     targetPlayer = null;
// }

// }
