using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawner", menuName = "ScriptableObjects/EnemySpawner")]
public class EnemySpawner : ScriptableObject
{
    [Header("Spawner Config - (To spawn enemies without limit set spawnerSize = 0)")]
    public GameObject EnemyPrefab;
    public int spawnerSize = 1;
    public float secondsToFirstSpawn = 1f;
    public float secondsToSpawn = 100f;
    
    public bool respawnEnemies = true;
    public Vector2 randomOffsetRange = Vector2.zero;
    
}
