using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Action/Roam")]
public class RoamAction : ZombieAction
{
    public float roamRadius = 10f;

    public override float CalculateUtility(ZombieAI ai)
    {
       return ai.targetPlayer == null ? 0.1f : 0f;
    }

    public override void Execute(ZombieAI ai)
    {
        if (!ai.agent.hasPath || ai.agent.remainingDistance < 1f)
        {
            Vector3 randomDir = Random.insideUnitSphere * roamRadius + ai.transform.position;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDir, out UnityEngine.AI.NavMeshHit hit, roamRadius, UnityEngine.AI.NavMesh.AllAreas))
                ai.agent.SetDestination(hit.position);
        }
    }
}
