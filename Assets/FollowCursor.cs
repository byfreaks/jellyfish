using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        this.transform.position = PointHelper.MouseWorldPos();
    }
}
