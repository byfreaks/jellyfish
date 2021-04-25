using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] ResourceData item;

    Vector2 anchorPosition;
    [SerializeField] float factor;
    [SerializeField] float horizontalTimeOffset;
    [SerializeField] float verticalTimeOffset;
    float naturalOffset;

    private void Start() {
        anchorPosition = this.transform.position;
        naturalOffset = Random.Range(-0.4f, 0.4f);
    }

    private void SetAnchor(Vector2 pos) {
        anchorPosition = pos;
    }

    private void Update() {
        this.transform.position = anchorPosition + new Vector2(Mathf.Sin(Time.time + naturalOffset + horizontalTimeOffset) * factor, Mathf.Sin(Time.time + naturalOffset + verticalTimeOffset) * factor);
    }
}
