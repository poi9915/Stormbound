using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieAction : ScriptableObject
{
    public string actionName;
    public abstract float CalculateUtility(ZombieAI ai);
    public abstract void Execute(ZombieAI ai);
}
