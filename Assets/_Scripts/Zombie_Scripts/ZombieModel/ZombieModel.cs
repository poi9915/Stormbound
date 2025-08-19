using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieModel : MonoBehaviour, ICharacterModel
{
    private Animator animator;
    private ZombieAI ai;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<ZombieAI>();
    }


    public void SetRunning(bool isRunning) => animator.SetBool("isRunning", isRunning);
    public void PlayAttack() => animator.SetTrigger("Attack");
    public void PlayDeath() => animator.SetTrigger("Dead");
    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed); // blend tree
        float animSpeedMultiplier = speed / 0.5f; // 0.5f là tốc độ gốc của animation
        animator.SetFloat("Animation", animSpeedMultiplier);
    }
    public void SetFloat(string parameter, float value)
    {
        animator.SetFloat(parameter, value);
    }
    public void ResetAttack()
    {
        animator.ResetTrigger("Attack");  // reset trigger để không bị kẹt ở animation cũ

    }
    public void OnAttackHit()
    {
        ai.DoAttackDamage();
    }
    
    

}
