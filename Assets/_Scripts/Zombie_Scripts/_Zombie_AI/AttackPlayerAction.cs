using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Action/AttackPlayer")]
public class AttackPlayerAction : ZombieAction
{
    public override float CalculateUtility(ZombieAI ai)
    {
        if (ai.targetPlayer == null || !ai.targetPlayer.gameObject.activeSelf)
            return 0f;

        // ❌ Nếu đã vào vùng base → không cho tấn công player nữa
        if (ai.isInBaseZone)
            return 0f;

        float distance = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);
        // Debug.Log($"[AttackPlayerAction] isInBaseZone: {ai.isInBaseZone}, distanceToBase: {Vector3.Distance(ai.transform.position, ai.baseTarget.position)}");

        return distance <= ai.attackRange ? 0.95f : 0f;
        
    }

    public override void Execute(ZombieAI ai)
    {
        // Nếu Player đã chết hoặc bị xóa => reset
        if (ai.targetPlayer == null || !ai.targetPlayer.gameObject.activeSelf)
        {
            ai.targetPlayer = null;
            ai.agent.isStopped = false;
            ai.model.ResetAttack();
            ai.model.SetFloat("Speed", 0f);
            return;
        }

        float distance = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);

        // Nếu Player ra khỏi tầm đánh => đuổi theo
        if (distance > ai.attackRange)
        {
            ai.agent.isStopped = false;
            ai.agent.SetDestination(ai.targetPlayer.position);
            return;
        }

        // Trong tầm đánh => đứng yên và Attack
        ai.agent.ResetPath();
        ai.agent.isStopped = true;

        // Xoay theo trục Y
        Vector3 dir = ai.targetPlayer.position - ai.transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.001f)
            ai.transform.rotation = Quaternion.LookRotation(dir);

        // Nếu cooldown đã xong thì Play Attack animation
        ai.attackTimer -= Time.deltaTime;
        if (ai.attackTimer <= 0f)
        {
            ai.PlayAttack();            // <-- chỉ gọi animation
            ai.attackTimer = ai.attackCooldown; // Reset timer cho lần attack tiếp theo
        }
    }
}
