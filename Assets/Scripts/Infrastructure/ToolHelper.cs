using UnityEngine;

public static class ToolHelper
{
    public static void Init(ToolHelperData _data){
        data = _data;
    }

    static ToolHelperData data;
    static bool initialized = false;

    public static GameObject InstantiateTool(tools tool, Transform parent)
    {
        // if(CheckInitialized())
        //     return null;
        
        switch(tool){
            case tools.blowtorch:
                return Inst(data.blowtorchPrefab, parent);

            case tools.harpoon:
                return Inst(data.harpoonPrefab, parent);
            
            default:
                Debug.LogError($"{tool} instantiate not yet implemented");
                return null;
        }
    }

    private static GameObject Inst(GameObject prefab, Transform parent = null)
    {
        // if(CheckInitialized())
        //     return null;

        if(parent != null)
            return GameObject.Instantiate(prefab, parent.position, Quaternion.identity, parent);
        else
            return GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private static bool CheckInitialized(){
        if(!initialized)
            Debug.LogError("Tool Helper not Initialized");
        return initialized;
    }
}

public enum tools{
    blowtorch,
    harpoon
}