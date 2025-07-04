namespace _Scripts._Core
{
    public interface IGun
    {
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
        float FireRate { get; }
        float ReloadTime { get; }
        float Damage { get; }
        void Shoot();
        void Reload();
    }
}