using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] [Range(1,5)] private int columns;
    [SerializeField] [Range(1,4)] private int rows;
    [SerializeField] private int gap;
    public float safeBound = 0;
    public List<GameObject> cells = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {   
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
}
