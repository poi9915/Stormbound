// using UnityEngine;

// [CreateAssetMenu(menuName = "Zombie/Action/AttackBase")]
// public class AttackBaseAction : ZombieAction
// {
//     public override float CalculateUtility(ZombieAI ai)
//     {
//         float distance = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
//        return distance <= ai.attackRange ? 0.9f : 0f;
//     }

//     public override void Execute(ZombieAI ai)
//     {
//         ai.agent.SetDestination(ai.transform.position);
//         ai.transform.LookAt(ai.baseTarget);

//         ai.attackTimer -= Time.deltaTime;
//         if (ai.attackTimer <= 0f)
//         {
//             var baseHealth = ai.baseTarget.GetComponent<BaseHealth>();
//             if (baseHealth != null)
//                 baseHealth.TakeDamage(10);
//             ai.attackTimer = ai.attackCooldown;
//         }
//     }
// }
