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
        if(ec.currentBehaviour.type == EnemyBehaviours.FollowPlayer)
        {
            if(squareDistanceToPlayer < squareDistanceToAttack)
                EndBehaviour(ec, EnemyBehaviours.Attack);
            else if(squareDistanceToPlayer > squareDistanceToEndChasing)
                EndBehaviour(ec, EnemyBehaviours.Idle);
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
