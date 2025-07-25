using System;
using UnityEngine;

namespace _Scripts._GunScripts
{
    public class GunHolderControl : MonoBehaviour
    {
        [Header("Gun")] 
        public GameObject pistol;
        public GameObject rifle;
        private GameObject currentWeapon;
        private int weaponIndex = 1; // 0 = pistol, 1 = rifle

        public Transform target;
        [Header("Right Hand Bone")] 
        public Transform rightHandBone;

        [Header("Input Handling")] 
        public KeyCode pistolKey = KeyCode.Alpha1;
        public KeyCode rifleKey = KeyCode.Alpha2;

        private void Start()
        {
            EquipWeapon(weaponIndex);
        }

        void Update()
        {
            // Nhấn phím để chọn súng
            if (Input.GetKeyDown(pistolKey))
            {
                weaponIndex = 0;
                EquipWeapon(weaponIndex);
            }
            else if (Input.GetKeyDown(rifleKey))
            {
                weaponIndex = 1;
                EquipWeapon(weaponIndex);
            }

            // Lăn chuột để thay đổi súng
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                weaponIndex = (weaponIndex + 1) % 2; // Cuộn lên
                EquipWeapon(weaponIndex);
            }
            else if (scroll < 0f)
            {
                weaponIndex = (weaponIndex - 1 + 2) % 2; // Cuộn xuống
                EquipWeapon(weaponIndex);
            }

            // Cập nhật vị trí / hướng của holder
            transform.LookAt(target);
            transform.position = rightHandBone.position;
        }

        private void EquipWeapon(int index)
        {
            if (index == 0)
            {
                pistol.SetActive(true);
                rifle.SetActive(false);
                currentWeapon = pistol;
            }
            else
            {
                pistol.SetActive(false);
                rifle.SetActive(true);
                currentWeapon = rifle;
            }
        }

        public GameObject GetCurrentWeapon()
        {
            return currentWeapon;
        }
    }
}
