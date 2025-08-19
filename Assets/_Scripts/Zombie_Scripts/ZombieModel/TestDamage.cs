using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Zombie"))
    {
        ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(20); // Zombie mất 20 HP
        }
    }
}

}
