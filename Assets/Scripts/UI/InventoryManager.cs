using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] [Range(1,4)] private int columns;
    [SerializeField] [Range(1,5)] private int rows;
    [SerializeField] private int gap;
    public List<GameObject> cells = new List<GameObject>();

    void Start()
    {   
        float rigthTopCornerX = canvas.GetComponent<RectTransform>().rect.xMax - cellPrefab.GetComponent<RectTransform>().sizeDelta.x/2;
        float rigthTopCornerY = canvas.GetComponent<RectTransform>().rect.yMax - cellPrefab.GetComponent<RectTransform>().sizeDelta.y/2;;

        float x = rigthTopCornerX;
        float y = rigthTopCornerY;
        
        for (int i = 0; i < columns; i++)
        {
            for (int z = 0; z < rows; z++)
            {   
                Vector2 cellPosition = new Vector2(x, y);
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as GameObject;
                cell.transform.SetParent (canvas.transform, false);
                cell.GetComponent<DropElement>().SetCellCoordinates(cellPosition);
                cells.Add(cell);
                x -= cellPrefab.GetComponent<RectTransform>().sizeDelta.x + gap;      
            }  
            y -= cellPrefab.GetComponent<RectTransform>().sizeDelta.y + gap;
            x = rigthTopCornerX;  
        }    
    }
}
