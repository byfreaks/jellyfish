using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyPanelScript : MonoBehaviour
{

    public RectTransform oxyTop;

    public void UpdateOxygenBar(float n)
    {
        oxyTop.sizeDelta = new Vector2(94 * n, 12);
    }

}
