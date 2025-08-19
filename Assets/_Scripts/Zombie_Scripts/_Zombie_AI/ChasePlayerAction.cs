using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Action/ChasePlayer")]
public class ChasePlayerAction : ZombieAction
{
    public override float CalculateUtility(ZombieAI ai)
    {
       if (ai.targetPlayer == null || !ai.targetPlayer.gameObject.activeSelf)
        return 0f;

        // ❌ Nếu zombie đã ở trong vùng base thì không chase player nữa
        if (ai.isInBaseZone)
            return 0f;

        float distToPlayer = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);
        return Mathf.Clamp01(1f - (distToPlayer / ai.viewRange));
        }

     public override void Execute(ZombieAI ai)
    {
       if (ai.targetPlayer == null || !ai.targetPlayer.gameObject.activeSelf)
        return;

        // Nếu đang di chuyển để đuổi player thì chắc chắn cần hủy animation attack
        ai.model.ResetAttack(); // <<< STOP animation tấn công
        ai.model.SetRunning(true); // <<< Chạy animation chạy

        ai.agent.isStopped = false;
        ai.agent.SetDestination(ai.targetPlayer.position);

        // Đồng bộ tốc độ với animation
        float speed = ai.agent.velocity.magnitude;
        ai.model.SetFloat("Speed", speed);
    }
}
