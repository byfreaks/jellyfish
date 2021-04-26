using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    private static LTDescr delay;
    public string message;
    public void OnPointerEnter(PointerEventData eventData){
        delay = LeanTween.delayedCall(0.5f, () => {
            TooltipSystem.Show(message);
        });
    }

    public void OnPointerExit(PointerEventData eventData){
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
