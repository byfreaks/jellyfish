using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObjects/ShopData", order = 2)]
public class ShopData : ScriptableObject
{
    public string resourceName;
    public int fiber;
    public int silicon;
    public int copperOre;
}