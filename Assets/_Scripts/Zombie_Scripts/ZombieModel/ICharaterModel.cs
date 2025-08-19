using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel
{
    void SetRunning(bool isRunning);
    void PlayAttack();
    void PlayDeath();
    void SetSpeed(float speed);  // <-- thêm dòng này
    void SetFloat(string parameter, float value); // Thêm phương thức này để hỗ trợ Animator
    void ResetAttack(); // Thêm phương thức này để reset trạng thái Attack
    void OnAttackHit();
    
}
