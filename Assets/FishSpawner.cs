using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public List<Sprite> fishes;
    public GameObject titleFishController;
    public Vector2 dir;
    public Material material;

    public float limit;
    int amount;

    float cooldown = 0;

    private void Start() {
        limit = Random.Range(2.5f, 5f);
        amount = Random.Range(2, 4);
    }

    private int rndFish => Random.Range(0, fishes.Count - 1);

    private Vector3 rndPos => this.transform.position + new Vector3(Random.Range(-1f,1f), Random.Range(-47f,47f), Random.Range(-8.8f, -9.1f));

    void Update()
    {
        cooldown += Time.deltaTime;
        if(cooldown >= limit){
            cooldown = 0;

            for (int i = 0; i < amount; i++)
            {
                var f = Instantiate(titleFishController, rndPos, Quaternion.identity, this.transform);
                f.GetComponent<FishController>().SetUp(dir, fishes[rndFish], material);
            }
        }
        
    }
}
