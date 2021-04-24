using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class Enemy: ScriptableObject
{
    public int MaxHealth = 5;
    public Color32 IdleColor = new Color32(255,255,255,255);
    public float IdleSpeed = 150.0f;
    public float IdleTimeBetweenSwams = 1.5f;
    public Color32 FollowPlayerColor = new Color32(255,100,0,255);
    public float FollowPlayerSpeed = 50.0f;
    public float FollowPlayerTimeBetweenSwams = 0.1f;
    public Color32 EscapeColor = new Color32(0,0,255,255);
    public float EscapeSpeed = 100.0f;
    public float EscapeTimeBetweenSwams = 0.1f;
}
