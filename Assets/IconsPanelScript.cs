using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconsPanelScript : MonoBehaviour
{
    public List<Texture2D> sprites;

    public RawImage harpoonImg, oxyTankImg, torchImg;

    public void SetUsing(int i)
    {
        harpoonImg.texture = i == 1 ? sprites[0] : sprites[1];
        oxyTankImg.texture = i == 2 ? sprites[2] : sprites[3];
        torchImg.texture = i == 3 ? sprites[4] : sprites[5];
    }
}
