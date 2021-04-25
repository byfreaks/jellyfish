using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject player;
    [SerializeField] [Range(1,5)] private int columns;
    [SerializeField] [Range(1,4)] private int rows;
    [SerializeField] private int gap;
    public float safeBound = 0;
    public List<GameObject> cells = new List<GameObject>();
    public List<GameObject> currentItems = new List<GameObject>();
    public List<GameObject> availibleItems = new List<GameObject>();
    public List<GameObject> draggableItems = new List<GameObject>();

    void Start()
    {   
        currentItems.Clear();
        availibleItems.Clear();
        DrawInventory(rows, columns);
    }

    public void DrawInventory(int rows, int columns) {
        if(cells.Count > 0){
            foreach (var cell in cells)
            {
                Destroy(cell);                
            }
        }

        float rigthTopCornerX = inventoryPanel.GetComponent<RectTransform>().rect.xMax;
        float rigthTopCornerY = inventoryPanel.GetComponent<RectTransform>().rect.yMax;

        float x = rigthTopCornerX;
        float y = rigthTopCornerY;
        
        for (int i = 0; i < rows; i++)
        {
            for (int z = 0; z < columns; z++)
            {   
                Vector2 cellPosition = new Vector2(x, y);
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as GameObject;
                cell.transform.SetParent(inventoryPanel.transform, false);
                cell.GetComponent<DropElement>().SetCellCoordinates(cellPosition);
                cells.Add(cell);
                x -= cellPrefab.GetComponent<RectTransform>().sizeDelta.x + gap;      
            }
            safeBound = x;
            y -= cellPrefab.GetComponent<RectTransform>().sizeDelta.y + gap;
            x = rigthTopCornerX;  
        }  
    }

    public void DrawAvailableItems(){
        foreach (var item in availibleItems)
        {
            Vector2 itemPosition = new Vector2(safeBound, Random.Range(inventoryPanel.GetComponent<RectTransform>().rect.yMin + 100, inventoryPanel.GetComponent<RectTransform>().rect.yMax - 100));
            GameObject availibleItem = Instantiate(item.GetComponent<DroppedItem>().item.inventoryItem, itemPosition, Quaternion.identity) as GameObject;
            availibleItem.transform.SetParent(inventoryPanel.transform, false); 
            availibleItem.GetComponent<DragElement>().SetElementData(item.GetComponent<DroppedItem>().item, item);
            availibleItem.GetComponent<DragElement>().invManager = this;
            availibleItem.GetComponent<DragElement>().canvas = canvas;
            draggableItems.Add(availibleItem);
        }
    }

    public void SetInventoryItem(GameObject item){
        currentItems.Add(item);
        draggableItems.Add(item);
    }

    public void RemoveInventoryItem(GameObject item){
        bool toAdd = true;
        foreach (var available in availibleItems)
        {
            if(available == item){
                toAdd = false;
                break;
            }
        }

        if(toAdd){
            currentItems.Remove(item);
            draggableItems.Remove(item);
            Vector2 worldItemPosition = new Vector2(player.transform.position.x, player.transform.position.y);
            item.GetComponent<DragElement>().worldReference = Instantiate(item.GetComponent<DragElement>().data.droppable, worldItemPosition, Quaternion.identity) as GameObject;
            availibleItems.Add(item);
        }
    }

    public void SetAvailableItems(List<GameObject> items){
        if(draggableItems.Count == 0 && currentItems.Count >= 0 && items.Count == 0){
            foreach (var item in availibleItems)
            {
                Destroy(item);
            }
        }

        foreach (var item in draggableItems)
        {
            if(checkDestroy(item)){
                Destroy(item);
            }
        }

        availibleItems.Clear();
        draggableItems.Clear();

        foreach (var item in items)
        {
            availibleItems.Add(item);
        }

        DrawAvailableItems();
    }

    public bool checkDestroy(GameObject item){
        foreach (var available in currentItems)
        {
            if(available == item){
                return false;
            }
        }
        return true;
    }
}
