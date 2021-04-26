using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour
{
    public abstract EnemyBehaviours type { get; }
    public abstract void Init(EnemyController ec);
    public abstract void BehaviorAction(EnemyController ec);
}
