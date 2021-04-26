using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy: ScriptableObject
{
    [Header("Attributes")]
    public int MaxHealth = 5;

    [Header("AI Configs")]
    public GameObject Hitbox;
    public Vector2 HitboxOffset;
    public EnemyBrain enemyBrain;

    [Header("Graphic Configs")]
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public Material material;
    public bool hasAttackAnimation;

    [Header("Behaviour Config - Default")]
    
    [Range(MIN_SPEED, MAX_SPEED)]
    public float Speed_Default;    
    
    [Range(MIN_TIME_BETWEEN_BEHAVIOUR_ACTION, MAX_TIME_BETWEEN_BEHAVIOUR_ACTION)]
    public float TimeBetweenAction_Default;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_Default;

    [Header("Behaviour Config - Follow Player")]

    [Range(MIN_SPEED, MAX_SPEED)]
    public float Speed_FollowPlayer;
    
    [Range(MIN_TIME_BETWEEN_BEHAVIOUR_ACTION, MAX_TIME_BETWEEN_BEHAVIOUR_ACTION)]
    public float TimeBetweenAction_FollowPlayer;
    
    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_FollowPlayer;
    
    [Header("Behaviour Config - Escape")]

    [Range(MIN_SPEED, MAX_SPEED)]
    public float Speed_Escape;
    
    [Range(MIN_TIME_BETWEEN_BEHAVIOUR_ACTION, MAX_TIME_BETWEEN_BEHAVIOUR_ACTION)]
    public float TimeBetweenAction_Escape;
    
    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_Escape;

    [Header("Behaviour Config - Attack")]

    [Range(MIN_SPEED, MAX_SPEED)]
    public float Speed_Attack;
    
    [Range(MIN_TIME_BETWEEN_BEHAVIOUR_ACTION, MAX_TIME_BETWEEN_BEHAVIOUR_ACTION)]
    public float TimeBetweenAction_Attack;

    [Header("Flock Config")]
    
    [Range(MIN_BEHAVIOUR_RADIUS, MAX_BEHAVIOUR_RADIUS)]
    public float NeighborRadius = 50f;

    [Range(MIN_BEHAVIOUR_RADIUS, MAX_BEHAVIOUR_RADIUS)]
    public float AvoidanceRadius = 30f;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_Avoidance = 2f;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_Aligment = 1;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float MovementWeight_Cohesion = 0.5f;

    //EDITOR CONFIG
    private const float MIN_SPEED = 10f;
    private const float MAX_SPEED = 500f;    
    private const float MIN_TIME_BETWEEN_BEHAVIOUR_ACTION = 0f;
    private const float MAX_TIME_BETWEEN_BEHAVIOUR_ACTION = 3f;
    private const float MIN_BEHAVIOUR_WEIGHT = 0f;
    private const float MAX_BEHAVIOUR_WEIGHT = 5f;
    private const float MIN_BEHAVIOUR_RADIUS = 5f;
    private const float MAX_BEHAVIOUR_RADIUS = 300f;
}
