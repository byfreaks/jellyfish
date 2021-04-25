using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public ToolHelperData toolHelperData;

    void Start()
    {
        //Initialize tools
        ToolHelper.Init(toolHelperData);
    }

    void Update()
    {
        
    }
}