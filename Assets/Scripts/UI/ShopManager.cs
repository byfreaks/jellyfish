using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopManager current;
    public InventoryManager inventoryManager;
    public ShopData referenceI3X3;
    public ShopData referenceI3X4;
    public ShopData referenceI4X4;
    public ShopData referenceI5X5;

    private void Awake() {
        current = this;
    }

    public static void BuyInventoryUpgrade(ShopUpgradeInventoryTypeHelper type){
        Debug.Log("COMPRA DE INVENTARIO");
        Debug.Log(type);

        switch (type)
        {
            case ShopUpgradeInventoryTypeHelper.i3x3:
                Debug.Log("Case 1");
                break;
            case ShopUpgradeInventoryTypeHelper.i3x4:
                Debug.Log("Case 2");
                break;
            case ShopUpgradeInventoryTypeHelper.i4x4:
                Debug.Log("Case 3");
                break;
            case ShopUpgradeInventoryTypeHelper.i5x5:
                Debug.Log("Case 4");
                break;
        }
    }
}
