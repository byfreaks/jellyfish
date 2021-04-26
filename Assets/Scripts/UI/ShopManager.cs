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

    public void BuyInventoryUpgrade(ShopUpgradeInventoryTypeHelper type){
        switch (type)
        {
            case ShopUpgradeInventoryTypeHelper.i3x3:
                if(current.validateBuy(referenceI3X3)){
                    Debug.Log("COMPRA DE INVENTARIO");
                }else{
                    Debug.Log("COMPRA DE INVENTARIO FALLIDA (FALTA DE RECURSOS)");
                }
                break;
            case ShopUpgradeInventoryTypeHelper.i3x4:
                current.validateBuy(referenceI3X4);
                break;
            case ShopUpgradeInventoryTypeHelper.i4x4:
                current.validateBuy(referenceI4X4);
                break;
            case ShopUpgradeInventoryTypeHelper.i5x5:
                current.validateBuy(referenceI5X5);
                break;
        }
    }

    public bool validateBuy(ShopData reference){
        bool accept = false;

        List<GameObject> currentCopperOre = new List<GameObject>();
        List<GameObject> currentSilicon = new List<GameObject>();
        List<GameObject> currentFiber = new List<GameObject>();

        foreach (var item in inventoryManager.currentItems)
        {
            switch (item.name)
            {
                case "AlgaeDragElement":
                    currentFiber.Add(item);
                    break;
                case "SiliconDragElement":
                    currentSilicon.Add(item);
                    break;
                case "CopperOreDragElement":
                    currentCopperOre.Add(item);
                    break;
            }
            
        }

        if(currentCopperOre.Count >= reference.copperOre && currentFiber.Count >= reference.fiber && currentSilicon.Count >= reference.silicon){
            return true;
        }else{
            return false;
        }
    }
}
