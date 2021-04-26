using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
  public ShopUpgradeInventoryTypeHelper upgradeInventoryType;
  public ShopManager shopManager;
  public void OnPointerClick(PointerEventData pointerEventData)
  {
    shopManager.BuyInventoryUpgrade(upgradeInventoryType);
  }
}
