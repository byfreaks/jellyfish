using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;

    private void Awake() {
        current = this;
    }

    public static void Show(string message){
        current.tooltip.SetText(message);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide(){
        current.tooltip.gameObject.SetActive(false);
    }
}
