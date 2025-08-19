using UnityEngine;

[CreateAssetMenu(menuName = "Zombie/Action/GoToBase")]
public class GoToBaseAction : ZombieAction
{
    public override float CalculateUtility(ZombieAI ai)
{
    if (ai.isInBaseZone)
        return 1f;

    // Nếu không thấy player thì ưu tiên base
    if (ai.targetPlayer == null)
    {
        float distToBaseNow = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
        // Cho dù ở xa hơn 50f vẫn trả về một điểm tối thiểu để di chuyển
        return Mathf.Max(0.2f, Mathf.Clamp01(1f - (distToBaseNow / 50f)));
    }

    // Nếu thấy player và player gần hơn base → bỏ qua đi base
    float distToPlayer = Vector3.Distance(ai.transform.position, ai.targetPlayer.position);
    float distToBase = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
    if (distToPlayer < distToBase)
        return 0f;

    // Ngược lại thì vẫn tính điểm như bình thường
    float distToBaseCurrent = Vector3.Distance(ai.transform.position, ai.baseTarget.position);
    return Mathf.Clamp01(1f - (distToBaseCurrent / 50f));
}

    public override void Execute(ZombieAI ai)
    {
        ai.model.ResetAttack();
        if (ai.baseTarget == null) return;

        // Cho agent tiếp tục di chuyển về base
        ai.agent.isStopped = false;
        ai.agent.SetDestination(ai.baseTarget.position);

        // Cập nhật animation
        if (ai.model != null)
        {
            ai.model.SetRunning(true);

            // Tính tốc độ hiện tại dựa trên NavMeshAgent
            float speed = ai.agent.velocity.magnitude;
            ai.model.SetFloat("Speed", speed);
        }
    }
}
