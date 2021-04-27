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

    public ShopData referenceOxygen;

    public ShopData referenceTanks;

    public ShopData referenceHarpoon;
    private void Awake() {
        current = this;
    }

    public void BuyInventoryUpgrade(ShopUpgradeInventoryTypeHelper type){
        switch (type)
        {
            case ShopUpgradeInventoryTypeHelper.oxygen:
                if(current.validateBuy(referenceOxygen)){
                    Debug.Log("COMPRA DE INVENTARIO");
                }else{
                    Debug.Log("COMPRA DE INVENTARIO FALLIDA (FALTA DE RECURSOS)");
                }
                break;
            case ShopUpgradeInventoryTypeHelper.harpoon:
                if(current.validateBuy(referenceHarpoon)){
                    Debug.Log("COMPRA DE INVENTARIO");
                }else{
                    Debug.Log("COMPRA DE INVENTARIO FALLIDA (FALTA DE RECURSOS)");
                }
                break;
            case ShopUpgradeInventoryTypeHelper.tanks:
                if(current.validateBuy(referenceTanks)){
                    Debug.Log("COMPRA DE INVENTARIO");
                }else{
                    Debug.Log("COMPRA DE INVENTARIO FALLIDA (FALTA DE RECURSOS)");
                }
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
                case "AlgaeDragElement(Clone)":
                    currentFiber.Add(item);
                    break;
                case "SiliconDragElement(Clone)":
                    currentSilicon.Add(item);
                    break;
                case "CopperOreDragElement(Clone)":
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
