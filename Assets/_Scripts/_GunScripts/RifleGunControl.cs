using System;
using System.Collections;
using _Scripts._Core;
using UnityEngine;

namespace _Scripts._GunScripts
{
    public class RifleGunControl : MonoBehaviour, IGun
    {
        [Header("Rifle Settings")] [SerializeField]
        private int maxAmmo = 30;

        [SerializeField] private float fireRate = 0.1f;
        [SerializeField] private float reloadTime = 1.5f;
        [SerializeField] private float damage = 15f;
        [SerializeField] private int shootRange = 100;

        [Header("References")] [SerializeField]
        private Transform orientation;

        public KeyCode shootKey = KeyCode.Mouse0;
        public KeyCode reloadKey = KeyCode.R;
        [SerializeField] public int CurrentAmmo { get; private set; }
        public int MaxAmmo => maxAmmo;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;
        public float Damage => damage;

        private bool isReloading = false;
        private float nextTimeToFire = 0f;


        private void Start()
        {
            CurrentAmmo = maxAmmo;
        }

        private void Update()
        {
            if (isReloading) return;

            if (CurrentAmmo <= 0 || (Input.GetKeyDown(reloadKey) && CurrentAmmo < maxAmmo))
            {
                Reload();
                return;
            }

            if (Input.GetKeyDown(shootKey) && Time.time >= nextTimeToFire)
            {
                Shoot();
                nextTimeToFire = Time.time + fireRate;
            }
        }

        public void Shoot()
        {
            if (CurrentAmmo <= 0) return;
            CurrentAmmo--;
            Ray rayCam = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            Vector3 targetPoint;
            if (Physics.Raycast(rayCam, out RaycastHit hit, shootRange))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = rayCam.GetPoint(shootRange);
            }

            Vector3 shootDir = (targetPoint - orientation.position).normalized;

            if (Physics.Raycast(orientation.position, shootDir, out RaycastHit hitInfo, shootRange))
            {
                Debug.DrawRay(orientation.position, shootDir * shootRange, Color.red, 1f);
                Debug.Log($" Rifle Hit: {hitInfo.collider.name}");
                IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damage);
                    Debug.Log($"Rifle dealt {damage} damage to {hitInfo.collider.name}");
                }
            }
        }

        public void Reload()
        {
            if (!isReloading)
            {
                StartCoroutine(ReloadCoroutine());
            }
        }

        private IEnumerator ReloadCoroutine()
        {
            isReloading = true;
            Debug.Log("Rifle Reloading...");
            yield return new WaitForSeconds(reloadTime);
            CurrentAmmo = maxAmmo;
            isReloading = false;
            Debug.Log("Rifle Reload done");
        }
    }
}