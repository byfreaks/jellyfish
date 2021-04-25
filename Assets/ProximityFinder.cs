using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityFinder : MonoBehaviour
{
    [SerializeField] List<GameObject> nearPickables = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.name.Contains("Pickable")){
            Debug.Log("Found!");
            nearPickables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.transform.name.Contains("Pickable")){
            nearPickables.Remove(other.gameObject);
        }
    }
}