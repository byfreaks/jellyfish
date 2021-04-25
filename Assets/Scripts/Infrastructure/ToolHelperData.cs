using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ToolHelper", order = 1)]
public class ToolHelperData : ScriptableObject
{
    [SerializeField] public GameObject blowtorchPrefab;
    [SerializeField] public GameObject harpoonPrefab;
}