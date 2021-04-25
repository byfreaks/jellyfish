using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/ResourceData", order = 2)]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public GameObject droppable;
    public GameObject inventoryItem;
    public ResourceType resourceType;

}

public enum ResourceType{
    equipement,
    resource
}