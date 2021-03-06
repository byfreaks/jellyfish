using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] public ResourceData item;

    Vector2 anchorPosition;
    [SerializeField] float factor;
    [SerializeField] float horizontalTimeOffset;
    [SerializeField] float verticalTimeOffset;
    float naturalOffset;
    public bool anchorPos = true;

    private void Start() {
        anchorPosition = this.transform.position;
        naturalOffset = Random.Range(-0.4f, 0.4f);
    }

    private void SetAnchor(Vector2 pos) {
        anchorPosition = pos;
    }

    private void Update() {
        if(anchorPos)
            this.transform.position = anchorPosition + new Vector2(0, Mathf.Sin(Time.time + naturalOffset + verticalTimeOffset) * factor);
    }
}
