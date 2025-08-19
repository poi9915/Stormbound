using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float maxhp = 300f;
    public float hp;
    private void Start()
    {
        hp = maxhp; // Khởi tạo HP  
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
        // Xử lý khi base bị phá hủy
        Debug.Log("Base đã bị phá hủy!");
        // Có thể thêm hiệu ứng, âm thanh, hoặc logic khác ở đây
        gameObject.SetActive(false); // Tắt đối tượng
    }
}
