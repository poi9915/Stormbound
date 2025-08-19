using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDameModel
{
    void TakeDamage(int amount);
    void Heal(int amount);
    bool IsDead { get; }
}
