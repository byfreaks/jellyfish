using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public string message;
    public void OnPointerEnter(PointerEventData eventData){
        TooltipSystem.Show(message);
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipSystem.Hide();
    }
}
