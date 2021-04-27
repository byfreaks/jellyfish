using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ShopManager current;
    public PlayerController player;
    public InventoryManager inventoryManager;
    // public ShopData referenceI3X3;
    // public ShopData referenceI3X4;
    // public ShopData referenceI4X4;
    // public ShopData referenceI5X5;

    public ShopData referenceOxygen;

    public ShopData referenceTanks;

    public ShopData referenceHarpoon;


    public GameObject purchaseableHarpoon, purchaseableTank;

    private void Awake() {
        current = this;
    }

    public void BuyInventoryUpgrade(ShopUpgradeInventoryTypeHelper type){
        switch (type)
        {
            case ShopUpgradeInventoryTypeHelper.oxygen:
                if(current.validateBuy(referenceOxygen)){
                    player.SetMaxOxygen(320);
                    current.confirmBuy(referenceOxygen);
                }
                break;
            case ShopUpgradeInventoryTypeHelper.harpoon:
                if(current.validateBuy(referenceHarpoon)){
                    current.confirmBuy(referenceHarpoon);
                    var h = Instantiate(purchaseableHarpoon, new Vector2(15, -10), Quaternion.identity).GetComponent<MissileComponent>();
                    h.Setup(0, 0f, Vector2.zero, false);
                }else{
                    Debug.Log("COMPRA DE INVENTARIO FALLIDA (FALTA DE RECURSOS)");
                }
                break;
            case ShopUpgradeInventoryTypeHelper.tanks:
                if(current.validateBuy(referenceTanks)){
                    current.confirmBuy(referenceTanks);
                    Instantiate(purchaseableTank, new Vector2(15, -10), Quaternion.identity);
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

    public void DeleteItem(string name){
        var found = inventoryManager.currentItems.Find(h => h.name.Contains(name));
        if(found) inventoryManager.currentItems.Remove(found);
    }

    public void confirmBuy(ShopData reference){
        for (int i = 0; i < reference.copperOre; i++)
        {
            DeleteItem("CopperOre");
        }

        for (int i = 0; i < reference.silicon; i++)
        {
            DeleteItem("Silicon");
        }

        for (int i = 0; i < reference.fiber; i++)
        {
            DeleteItem("Algae");
        }
        inventoryManager.SetShopInventory();
    }

}
