using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public ToolHelperData toolHelperData;
    public InventoryManager inventoryManager;
    private bool openInventory = false;
    public List<GameObject> ToDelete;

    void Start()
    {
        //Initialize tools
        ToolHelper.Init(toolHelperData);
        SFXHelper.Init(GameObject.Find("PIxel Perfect Camera").GetComponent<SFXManager>());
        inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
        
        foreach(GameObject go in ToDelete){
            GameObject.Destroy(go, 0.01f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            openInventory = !openInventory;
            inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
        }
    }
}