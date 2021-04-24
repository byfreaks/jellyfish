using UnityEngine;

public class ToolComponent : MonoBehaviour
{
    void Update()
    {
        var mouesPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        PointHelper.PointAtTarget(this.transform, Camera.main.ScreenToWorldPoint(Input.mousePosition), false);
    }
}