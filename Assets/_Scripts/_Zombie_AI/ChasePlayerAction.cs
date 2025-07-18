// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(menuName = "Zombie/Action/ChasePlayer")]
// public class ChasePlayerAction : ZombieAction
// {
//     public override float CalculateUtility(ZombieAI ai)
//     {
//         if (ai.targetPlayer == null)
//         {
//             return 0f; // No target to chase
//         }

//         float distance = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);
//         return Mathf.Clamp01(1f - (distance / ai.viewRange));
//     }

//      public override void Execute(ZombieAI ai)
//     {
//         ai.agent.SetDestination(ai.targetPlayer.position);
//     }
// }
