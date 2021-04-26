using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBrain : ScriptableObject
{
    public abstract void CheckConditionsForNewBehaviours(EnemyController ec);
    public abstract void EndBehaviour(EnemyController ec, EnemyBehaviours forceNewBehaviour = EnemyBehaviours.None);
}
