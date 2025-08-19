using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDameModel
{
    public int maxHealth = 100;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public bool IsDead { get; private set; }

    public System.Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    private void Update()
    {
        // Test bằng phím bấm
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20); // Mỗi lần nhấn T mất 20 HP
            // Debug.Log($"Zombie HP: {currentHealth}/{maxHealth}");
        }
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;

        // Chỉ giật nếu còn sống
        if (!IsDead)
        {
            StopCoroutine(nameof(HitShake)); // đảm bảo không chồng nhiều shake
            StartCoroutine(HitShake(0.1f, 0.1f));
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    private IEnumerator HitShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Random lệch nhẹ
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset vị trí
        transform.localPosition = originalPos;
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    private void Die()
    {
        IsDead = true;

        // Gọi animation Dead (nếu có ZombieModel)
        ICharacterModel model = GetComponent<ICharacterModel>();
        if (model != null)
        {
            model.PlayDeath();
        }

        // Tắt AI
        ZombieAI ai = GetComponent<ZombieAI>();
        if (ai != null)
        {
            ai.agent.isStopped = true;
            ai.enabled = false;
        }

        OnDeath?.Invoke();

        // Xóa zombie sau 3 giây (hoặc tuỳ bạn)
        Destroy(gameObject, 3f);
    }
    
    public void SetupHealthMultiplier(float multiplier)
    {
        maxHealth = Mathf.RoundToInt(maxHealth * multiplier);
        currentHealth = maxHealth;
    }

}
