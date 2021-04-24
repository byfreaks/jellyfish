using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropElement : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Image element;
    private bool hasElement;

    private void Start(){
        element = GetComponent<Image>();
        hasElement = false;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        element.color = Color.green;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(!hasElement){
            element.color = Color.white;
        }else{
            element.color = Color.red;
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
}
