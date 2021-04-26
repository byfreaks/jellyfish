using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawnerController : MonoBehaviour
{
    public EnemySpawner spawnerData; //[VALUE DEFINED FROM EDITOR]
    public List<GameObject> enemies;
    
    private bool crIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        crIsRunning = false;
        StartCoroutine(SpawnEachNSeconds(spawnerData.secondsToFirstSpawn));
    }

    //Spawn each x seconds
    IEnumerator SpawnEachNSeconds(float seconds)
    {
        crIsRunning = true;
        yield return new WaitForSeconds(seconds);
        SpawnEnemy();
        crIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnerData.spawnerSize == 0 || enemies.Count < spawnerData.spawnerSize)
            if(!crIsRunning) StartCoroutine(SpawnEachNSeconds(spawnerData.secondsToSpawn));
        
        for(int i = 0; i < enemies.Count; i++)
            if (enemies[i] == null) enemies.RemoveAt(i);
                
    }

    void SpawnEnemy()
    {
        // If spawner is limited and too many enemies alive -> No spawn
        if(spawnerData.spawnerSize != 0 && spawnerData.spawnerSize == enemies.Count)
            return;
        
        //Spawn
        GameObject newEnemy = Instantiate(spawnerData.EnemyPrefab, transform.position, Quaternion.identity);

        //[PERFORMANCE] If spawner is unlimited it is not necessary save entities references
        if(spawnerData.spawnerSize != 0) enemies.Add(newEnemy);
        
        //[DESTROY] When all enemies are spawned and the spawner has not respawn configs
        if(spawnerData.spawnerSize != 0 && !spawnerData.respawnEnemies && enemies.Count == spawnerData.spawnerSize)
            Destroy(gameObject);
    }
}
