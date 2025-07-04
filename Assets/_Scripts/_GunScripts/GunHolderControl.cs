using System;
using UnityEngine;

namespace _Scripts._GunScripts
{
    public class GunHolderControl : MonoBehaviour
    {
        [Header("Gun")] public GameObject pistol;
        public GameObject rifle;
        private GameObject currentWeapon;
        public Transform target;
        [Header("Right Hand Bone")] public Transform rightHandBone;
        [Header("Input Handling")] public KeyCode pistolKey = KeyCode.Alpha1;
        public KeyCode rifleKey = KeyCode.Alpha2;

        private void Start()
        {
            currentWeapon = rifle;
        }

        void Update()
        {
            if (Input.GetKeyDown(pistolKey))
            {
                EquipPistol();
            }
            else if (Input.GetKey(rifleKey))
            {
                EquipRifle();
            }

            transform.LookAt(target);
            transform.position = rightHandBone.position;
        }

        private void EquipRifle()
        {
            pistol.SetActive(false);
            rifle.SetActive(true);
            currentWeapon = rifle;
        }

        private void EquipPistol()
        {
            pistol.SetActive(true);
            rifle.SetActive(false);
            currentWeapon = pistol;
        }

        public GameObject GetCurrentWeapon()
        {
            return currentWeapon;
        }
    }
}