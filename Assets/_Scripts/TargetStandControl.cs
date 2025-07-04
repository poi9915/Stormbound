using _Scripts._Core;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class TargetStandControl : MonoBehaviour, IDamageable
    {
        public TextMeshPro healthText;

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; set; }
        public bool IsDead { get; }
        public float totalDamageTaken = 0;

        public void TakeDamage(float damage)
        {
            totalDamageTaken += damage;
            healthText.SetText(totalDamageTaken.ToString());
        }

        public void Heal(float amount)
        {
            throw new System.NotImplementedException();
        }
    }
}