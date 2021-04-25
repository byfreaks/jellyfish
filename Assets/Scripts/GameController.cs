using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public ToolHelperData toolHelperData;
    public InventoryManager inventoryManager;
    private bool openInventory = false;

    void Start()
    {
        //Initialize tools
        ToolHelper.Init(toolHelperData);
        inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            openInventory = !openInventory;
            inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
        }
    }
}