using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaResetScript : MonoBehaviour
{
    public CameraScript camera;
    public int clampSet;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Contains("Player")){
            camera.ResetClamp(clampSet);
        }
    }

}
