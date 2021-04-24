using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryManager invManager;
    private RectTransform rectTransform;
    private RectTransform origin;
    private CanvasGroup canvasGroup;
    private List<DropElement> cells = new List<DropElement>();
    private bool dropped = false;

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
        dropped = false;
        cells.Clear();
        canvasGroup.alpha = .4f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData){
        // SE DEBE COLOCAR LA REFERENCIA DE LA CÁMARA SI NO ES OVERLAY, SI ES OVERLAY DEJAR EN NULL
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.pointerCurrentRaycast.screenPosition, null, out anchoredPos);

        if(!dropped){
            foreach (var cell in invManager.cells)
            {
                if(!cell.GetComponent<DropElement>().checkCell(anchoredPos)){
                    Vector2 prevPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.pressPosition, null, out prevPos);
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = prevPos;
                    break;
                }
            }
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void SetCellReference(DropElement cell){
        cells.Add(cell);
        dropped = true;
    }
}
