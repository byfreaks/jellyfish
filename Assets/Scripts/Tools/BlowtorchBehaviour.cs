using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToolComponent))]
public class BlowtorchBehaviour : MonoBehaviour
{

    [SerializeField] GameObject fire;

    void Update()
    {
        //TODO: turn into proper logic so it can be animated
        if(Input.GetMouseButton(0)) 
            fire.gameObject.SetActive(true); 
        
        if(Input.GetMouseButtonUp(0))
            fire.gameObject.SetActive(false);
    }
}
