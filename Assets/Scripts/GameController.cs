using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public ToolHelperData toolHelperData;
    public InventoryManager inventoryManager;
    private bool openInventory = false;

    public Texture2D cursorTexture;

    void Start()
    {
        //Initialize tools
        ToolHelper.Init(toolHelperData);
        inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            openInventory = !openInventory;
            inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
        }
    }
}