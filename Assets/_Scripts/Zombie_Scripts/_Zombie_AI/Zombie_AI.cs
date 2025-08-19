using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombieAI : MonoBehaviour
{
    public int baseDamage = 10;
    public bool isInBaseZone = false;
    public float baseZoneRadius = 5f;
    private bool hasDealtDamage;
    [HideInInspector] public ICharacterModel model;
    public NavMeshAgent agent;
    public Transform targetPlayer;
    public Transform baseTarget;
    public Transform[] allPlayers;

    public float viewRange = 15f;
    public float viewAngle = 90f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    public float attackTimer;

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    [Header("Utility AI Actions")]
    public List<ZombieAction> actions;

    void Start()
    {
        model = GetComponent<ZombieModel>();
    agent = GetComponent<NavMeshAgent>();

    // Tìm tất cả player theo tag
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    allPlayers = new Transform[players.Length];
    for (int i = 0; i < players.Length; i++)
        allPlayers[i] = players[i].transform;

    // Tự tìm base target theo tag "Base"
    GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
    if (baseObj != null)
    {
        baseTarget = baseObj.transform;
    }

    // Gọi lại DetectPlayer khi bắt đầu
    DetectPlayer();

    agent.stoppingDistance = 0.2f;
    }

    void Update()
    {
        DetectPlayer();

        float distToBase = 0f;
        if (baseTarget != null)
        {
            Collider baseCollider = baseTarget.GetComponent<Collider>();
            if (baseCollider != null)
            {
                Vector3 closest = baseCollider.ClosestPoint(transform.position);
                distToBase = Vector3.Distance(transform.position, closest);
            }
            else
            {
                distToBase = Vector3.Distance(transform.position, baseTarget.position);
            }
        }

        isInBaseZone = distToBase <= baseZoneRadius;

        ZombieAction best = null;
        float maxScore = 0f;
        foreach (var a in actions)
        {
            float score = a.CalculateUtility(this);
            if (score > maxScore)
            {
                maxScore = score;
                best = a;
            }
        }

        if (best != null)
        {
            best.Execute(this);
        }

        if (model != null)
        {
            float speed = agent.isStopped || (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                ? 0f
                : agent.velocity.magnitude;
            model.SetSpeed(speed);
        }
    }

    public void DetectPlayer()
    {
        Transform closest = null;
        float closestDist = float.MaxValue;

        foreach (Transform player in allPlayers)
        {
            if (player == null || !player.gameObject.activeSelf) continue;

            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health == null || health.hp <= 0) continue;

            Vector3 dir = (player.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, player.position);
            float angle = Vector3.Angle(transform.forward, dir);

            if (distance < viewRange && angle < viewAngle / 2)
            {
                if (!Physics.Linecast(transform.position, player.position, obstacleLayer))
                {
                    if (distance < closestDist)
                    {
                        closest = player;
                        closestDist = distance;
                    }
                }
            }
        }

        targetPlayer = closest;
    }

    public void PlayAttack()
    {
        hasDealtDamage = false;
        model.PlayAttack();
    }

    public void DoAttackDamage()
    {
        if (targetPlayer != null)
        {
            var health = targetPlayer.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(baseDamage);
        }
        else if (baseTarget != null)
        {
            var baseHealth = baseTarget.GetComponent<BaseHealth>();
            if (baseHealth != null)
                baseHealth.TakeDamage(baseDamage);
        }
    }

    public void SetupDamageMultiplier(float multiplier)
    {
        baseDamage = Mathf.RoundToInt(baseDamage * multiplier);
    }

    
}
