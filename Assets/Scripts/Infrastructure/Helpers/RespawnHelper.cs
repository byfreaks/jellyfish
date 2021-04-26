using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RespawnHelper {

    static List<GameObject> toRespawn = new List<GameObject>();

    public static void RespawnList(){
        toRespawn.ForEach(i => i.GetComponent<ResourceController>().Restore());
        toRespawn.Clear();
    }

    public static void AddToRestore(GameObject obj){
        toRespawn.Add(obj);
    }
}