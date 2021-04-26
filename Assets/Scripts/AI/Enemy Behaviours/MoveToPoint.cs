using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : EnemyBehaviour
{
    private Vector2 point;
    public override EnemyBehaviours type { get { return EnemyBehaviours.MoveToPoint; } }
    public override void Init(EnemyController ec)
    {
        ec.Speed = ec.enemyData.Speed_Default;
        ec.TimeBetweenActions = ec.enemyData.TimeBetweenAction_Default;
        point = ec.InitPoint;
    }
    public override void BehaviorAction(EnemyController ec)
    {
        Vector2 currentPosition = ec.transform.position;
        float weight = ec.enemyData.MovementWeight_Default;
        ec.CurrentDirection =  (point - currentPosition).normalized * weight;
        ec.CurrentSpeed = ec.Speed;
    }
}
