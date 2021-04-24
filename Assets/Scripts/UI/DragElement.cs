using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform origin;
    private CanvasGroup canvasGroup;
    private List<DropElement> cells = new List<DropElement>();

    private void Awake(){
        origin = GetComponent<RectTransform>();
        rectTransform = origin;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData){

        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData){
        foreach (var cell in cells)
        {
            cell.ClearCell();
        }
        cells.Clear();
        canvasGroup.alpha = .4f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData){
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void SetCellReference(DropElement cell){
        cells.Add(cell);
        Debug.Log(cells.Count);
    }
}
