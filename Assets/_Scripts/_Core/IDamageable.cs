namespace _Scripts._Core
{
    public interface IDamageable
    {
        float CurrentHealth { get; set; }
        float MaxHealth { get; set; }
        bool IsDead { get; }
        void TakeDamage(float damage);
        void Heal(float amount);
    }
}