// using UnityEngine;

// [CreateAssetMenu(menuName = "Zombie/Action/GoToBase")]
// public class GoToBaseAction : ZombieAction
// {
//     public override float CalculateUtility(ZombieAI ai)
//     {
//         if (ai.targetPlayer != null) return 0f;

//         float distance = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
//         float score = Mathf.Clamp01(1f - (distance / 50f));

//     return Mathf.Max(score, 0.4f); // Luôn ưu tiên cao hơn roam
//     }

//     public override void Execute(ZombieAI ai)
//     {
//         ai.agent.SetDestination(ai.baseTarget.position);
//     }
// }
