using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishBrain", menuName = "ScriptableObjects/FishBrain")]
public class FishBrain : EnemyBrain
{
    public float distanceToDetectPlayer = 50f;
    public float distanceToEscapeFromPlayer = 100f;

    public override void CheckConditionsForNewBehaviours(EnemyController ec)
    {
        float squareDistanceToPlayer = (ec.Player.transform.position - ec.transform.position).sqrMagnitude;
        float squareDistanceToDetectPlayer = distanceToDetectPlayer * distanceToDetectPlayer;
        float squareDistanceToEscapeFromPlayer = distanceToEscapeFromPlayer * distanceToEscapeFromPlayer;
        float squareDistancePointToHold = (ec.InitPoint - (Vector2) ec.transform.position).sqrMagnitude;
        float squareRangeToHoldPoint = 10f * 10f;
        //Idle: 
        //  * Player too close -> Escape
        if(ec.currentBehaviour.type == EnemyBehaviours.Idle)
        {
            if(squareDistanceToPlayer < squareDistanceToDetectPlayer)
                EndBehaviour(ec, EnemyBehaviours.Escape);
        }
        //Escape:
        //  * Player too far -> MoveToPoint
        else if(ec.currentBehaviour.type == EnemyBehaviours.Escape)
        {
            if(squareDistanceToPlayer > squareDistanceToEscapeFromPlayer)
                EndBehaviour(ec, EnemyBehaviours.MoveToPoint);
        }
        //MoveToPoint:
        // * Point to close -> Idle
        //  * Player too close -> Escape
        else if(ec.currentBehaviour.type == EnemyBehaviours.MoveToPoint)
        {
            if(squareDistancePointToHold < squareRangeToHoldPoint)
                EndBehaviour(ec, EnemyBehaviours.Idle);
            else if(squareDistanceToPlayer < squareDistanceToDetectPlayer)
                EndBehaviour(ec, EnemyBehaviours.Escape);
        }
    }

    public override void EndBehaviour(EnemyController ec, EnemyBehaviours forceNewBehaviour = EnemyBehaviours.None)
    {
        //If new behaviour is forced
        if(forceNewBehaviour != EnemyBehaviours.None)
            ec.ChangeBehaviour(forceNewBehaviour);
        //Pass next behaviour defined
        else
        {
        }
    }
}
