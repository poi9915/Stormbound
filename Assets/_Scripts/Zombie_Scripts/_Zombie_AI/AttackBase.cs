// AttackBaseAction.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Action/AttackBase")]
public class AttackBaseAction : ZombieAction
{
    public override float CalculateUtility(ZombieAI ai)
    {
    float distance = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
    // Debug.Log($"[AttackBaseAction] Distance to base: {distance:F2}, Attack Range: {ai.attackRange:F2}");

    // Chỉ chọn AttackBase khi ở trong attackRange (thêm tolerance để tránh bị lỡ)
    float tolerance = 0.2f; // Sai số cho NavMeshAgent
    if (distance <= ai.attackRange + tolerance)
    {
        return 1f; // Ưu tiên cao nhất khi đã vào range
    }

    // Nếu chưa vào range thì không chọn AttackBase
    return 0f;
    }

    public override void Execute(ZombieAI ai)
{
    float distance = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
    float tolerance = 0.2f;

    if (distance <= ai.attackRange + tolerance)
    {
        ai.agent.ResetPath();
        ai.agent.isStopped = true;
        ai.model.SetFloat("Speed", 0f);

       // Chỉ xoay theo Y, giữ nguyên X và Z để zombie không bị nghiêng
        Vector3 targetPos = ai.baseTarget.position;
        Vector3 direction = targetPos - ai.transform.position;
        direction.y = 0f; // bỏ độ cao

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            ai.transform.rotation = rot;
        }

        ai.attackTimer -= Time.deltaTime;
        if (ai.attackTimer <= 0f)
        {
            var baseHealth = ai.baseTarget.GetComponent<BaseHealth>();
            if (baseHealth != null)
                baseHealth.TakeDamage(10);

            ai.model.PlayAttack();
            ai.attackTimer = ai.attackCooldown;
        }
    }
    else
    {
        // Nếu chưa vào range thì tiếp tục di chuyển
        ai.agent.isStopped = false;
        ai.agent.SetDestination(ai.baseTarget.position);

        float moveSpeed = ai.agent.velocity.magnitude;
        ai.model.SetFloat("Speed", moveSpeed);
    }
}

}
