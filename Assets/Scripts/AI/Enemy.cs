using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy: ScriptableObject
{
    public int MaxHealth = 5;

    [Header("Graphic Configs")]
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public Material material;

    [Header("Enemy Behaviour Configs")]
    public float IdleSpeed = 50.0f;
    
    [Range(MIN_TIME_BETWEEN_SWAMS, MAX_TIME_BETWEEN_SWAMS)]
    public float IdleTimeBetweenSwams = 1.5f;
    public float FollowPlayerSpeed = 75.0f;
    
    [Range(MIN_TIME_BETWEEN_SWAMS, MAX_TIME_BETWEEN_SWAMS)]
    public float FollowPlayerTimeBetweenSwams = 0.1f;
    
    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float FollowPlayerWeight = 1.0f;
    public float EscapeSpeed = 100.0f;
    
    [Range(MIN_TIME_BETWEEN_SWAMS, MAX_TIME_BETWEEN_SWAMS)]
    public float EscapeTimeBetweenSwams = 0.1f;
    
    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float EscapeWeight = 0.5f;

    [Header("Flock Behaviour Configs")]
    
    [Range(MIN_BEHAVIOUR_RADIUS, MAX_BEHAVIOUR_RADIUS)]
    public float NeighborRadius = 50f;

    [Range(MIN_BEHAVIOUR_RADIUS, MAX_BEHAVIOUR_RADIUS)]
    public float AvoidanceRadius = 30f;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float AvoidanceWeight = 2f;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float AligmentWeight = 1;

    [Range(MIN_BEHAVIOUR_WEIGHT, MAX_BEHAVIOUR_WEIGHT)]
    public float CohesionWeight = 0.5f;

    //EDITOR CONFIG
    private const float MIN_TIME_BETWEEN_SWAMS = 0f;
    private const float MAX_TIME_BETWEEN_SWAMS = 3f;
    private const float MIN_BEHAVIOUR_WEIGHT = 0f;
    private const float MAX_BEHAVIOUR_WEIGHT = 5f;
    private const float MIN_BEHAVIOUR_RADIUS = 5f;
    private const float MAX_BEHAVIOUR_RADIUS = 300f;
}
