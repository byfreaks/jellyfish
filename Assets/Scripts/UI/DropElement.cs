using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropElement : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image element;
    private bool hasElement;
    private Vector2 position;

    private void Start(){
        element = GetComponent<Image>();
        hasElement = false;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        element.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(!hasElement){
            element.color = Color.white;
        }
    }

    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag != null){
            hasElement = true;
            DragElement draggedElement = eventData.pointerDrag.GetComponent<DragElement>();
            draggedElement.SetCellReference(this);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void ClearCell(){
        hasElement = false;
    }

    public void SetCellCoordinates(Vector2 position){
       this.position = position;
    }

    public bool checkCell(Vector2 cell){
        if(!hasElement){
            element.color = Color.white;
            return true;
        }else{
            return false;
        }
    }

    public bool checkPositionCell(Vector2 cell){
        Debug.Log(cell);
        Debug.Log(position);
        if(
            (cell.x <= position.x && cell.x >= position.x - element.GetComponent<RectTransform>().sizeDelta.x) &&
            (cell.y <= position.y && cell.y >= position.y - element.GetComponent<RectTransform>().sizeDelta.y)
        ){
            return true;
        }else{
            return false;
        }
    }
}
