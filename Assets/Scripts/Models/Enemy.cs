using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy: ScriptableObject
{
    public int MaxHealth = 5;

    [Header("Enemy Behaviour Configs")]
    public Color32 IdleColor = new Color32(255,255,255,255);
    public float IdleSpeed = 50.0f;
    public float IdleTimeBetweenSwams = 1.5f;
    public Color32 FollowPlayerColor = new Color32(255,100,0,255);
    public float FollowPlayerSpeed = 75.0f;
    public float FollowPlayerTimeBetweenSwams = 0.1f;
    public Color32 EscapeColor = new Color32(0,0,255,255);
    public float EscapeSpeed = 100.0f;
    public float EscapeTimeBetweenSwams = 0.1f;

    [Header("Flock Behaviour Configs")]
    
    [Range(40f, 80f)]
    public float NeighborRadius = 50f;

    [Range(20f, 40f)]
    public float AvoidanceRadius = 30f;

    [Range(0f, 3f)]
    public float AvoidanceWeight = 2f;

    [Range(0f, 3f)]
    public float AligmentWeight = 1;

    [Range(0f, 3f)]
    public float CohesionWeight = 0.5f;
}
