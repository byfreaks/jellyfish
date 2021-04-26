using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JellyFishBrain", menuName = "ScriptableObjects/JellyFishBrain")]
public class JellyFishBrain : EnemyBrain
{
    public float distanceToAttackPlayer = 20f;
    public float distanceToStartChasingPlayer = 10f;
    public float distanceToEndChasingPlayer = 100f;

    public override void CheckConditionsForNewBehaviours(EnemyController ec)
    {
        float squareDistanceToPlayer = (ec.Player.transform.position - ec.transform.position).sqrMagnitude;
        float squareDistanceToStartChasing = distanceToStartChasingPlayer * distanceToStartChasingPlayer;
        float squareDistanceToEndChasing = distanceToEndChasingPlayer * distanceToEndChasingPlayer;
        float squareDistanceToAttack = distanceToAttackPlayer * distanceToAttackPlayer;
        float squareDistancePointToHold = (ec.InitPoint - (Vector2) ec.transform.position).sqrMagnitude;
        float squareRangeToHoldPoint = 10f * 10f;
        
        //Idle:
        //  * Player too close -> FollowPlayer
        if(ec.currentBehaviour.type == EnemyBehaviours.Idle)
        {
            if(squareDistanceToPlayer < squareDistanceToStartChasing)
                EndBehaviour(ec, EnemyBehaviours.FollowPlayer);
        }
        //FollowPlayer:
        //  * Player too close -> Attack
        //  * Player too far -> MoveToPoint
        else if(ec.currentBehaviour.type == EnemyBehaviours.FollowPlayer)
        {
            if(squareDistanceToPlayer < squareDistanceToAttack)
                EndBehaviour(ec, EnemyBehaviours.Attack);
            else if(squareDistanceToPlayer > squareDistanceToEndChasing)
                EndBehaviour(ec, EnemyBehaviours.MoveToPoint);
        }
        //MoveToPoint:
        //  * Point too close -> Idle
        //  * Player too close -> FollowPlayer
        else if(ec.currentBehaviour.type == EnemyBehaviours.MoveToPoint)
        {
            if(squareDistancePointToHold < squareRangeToHoldPoint)
                EndBehaviour(ec, EnemyBehaviours.Idle);
            else if(squareDistanceToPlayer < squareDistanceToStartChasing)
                EndBehaviour(ec, EnemyBehaviours.FollowPlayer);
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
            //When finish Attack -> FollowPlayer
            if(ec.currentBehaviour.type == EnemyBehaviours.Attack) 
                ec.ChangeBehaviour(EnemyBehaviours.FollowPlayer);
        }
    }
}
