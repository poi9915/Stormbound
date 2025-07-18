// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(menuName = "Zombie/Action/AttackPlayer")]
// public class AttackPlayerAction : ZombieAction
// {
//     public override float CalculateUtility(ZombieAI ai)
//     {
//         if (ai.targetPlayer == null)
//         {
//             return 0f; // No target to attack
//         }

//         float distance = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);
//         return distance <= ai.attackRange ? 1f : 0f; 
//     }

//     public override void Execute(ZombieAI ai)
//     {
//         ai.agent.SetDestination(ai.transform.position);
//         ai.transform.LookAt(ai.targetPlayer);

//         ai.attackTimer -= Time.deltaTime;
//         if (ai.attackTimer <= 0f)
//         {
//             var health = ai.targetPlayer.GetComponent<PlayerHealth>();
//             if (health != null)
//                 health.TakeDamage(10);

//             ai.attackTimer = ai.attackCooldown;
//         }
//     }
// }
