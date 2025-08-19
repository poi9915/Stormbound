using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f;
    public float hp;
    private void Start()
    {
        hp = maxHP; // Khởi tạo HP
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    private void Die()
    {
        // Xử lý khi player chết
        Debug.Log($"{gameObject.name} đã chết!");
        // Có thể thêm hiệu ứng, âm thanh, hoặc logic khác ở đây
        gameObject.SetActive(false); // Tắt đối tượng
    }
}
