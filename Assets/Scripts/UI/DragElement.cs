using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public InventoryManager invManager;
    private RectTransform rectTransform;
    private RectTransform origin;
    private CanvasGroup canvasGroup;
    private List<DropElement> cells = new List<DropElement>();
    private bool dropped = false;
    public ResourceData data;
    public GameObject worldReference;

    private void Awake(){
        origin = GetComponent<RectTransform>();
        rectTransform = origin;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData){
        foreach (var cell in cells)
        {
            cell.ClearCell();
        }
        dropped = false;
        cells.Clear();
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData){
        canvasGroup.alpha = .4f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData){
        // SE DEBE COLOCAR LA REFERENCIA DE LA CÁMARA SI NO ES OVERLAY, SI ES OVERLAY DEJAR EN NULL
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.pointerCurrentRaycast.screenPosition, null, out anchoredPos);

        Debug.Log(anchoredPos);

        if(!dropped){
            foreach (var cell in invManager.cells)
            {   
                if(!cell.GetComponent<DropElement>().checkCell(anchoredPos) && cell.GetComponent<DropElement>().checkPositionCell(anchoredPos)){
                    Vector2 safePlace = new Vector2(invManager.safeBound, Random.Range(canvas.GetComponent<RectTransform>().rect.yMin + 100, canvas.GetComponent<RectTransform>().rect.yMax - 100));
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = safePlace;
                    break;
                }
            }
            invManager.RemoveInventoryItem(this.gameObject);
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void SetCellReference(DropElement cell){
        cells.Clear();
        cells.Add(cell);
        invManager.SetInventoryItem(this.gameObject);
        if(worldReference != null){
            Destroy(worldReference);
        }
        dropped = true;
    }

    public void SetElementData(ResourceData data, GameObject item){
        this.data = data;
        worldReference = item;
    }
}
