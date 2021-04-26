using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPanelScript : MonoBehaviour
{
    public GameObject barPrefab;
    public List<GameObject> barPrefabList;
    private int hgap = 3;
    RectTransform firstPos;
    
    private void Awake() {
        firstPos = GameObject.Find("FirstBarPos").GetComponent<RectTransform>();
    }

    public void SetHealth(int n){
        barPrefabList.ForEach(b => GameObject.Destroy(b, 0.01f));
        barPrefabList.Clear();

        for (int i = 0; i < n; i++)
        {
            int dist = 7;
            var pos = new Vector2(  hgap+firstPos.position.x + (hgap + dist)*i, firstPos.position.y);
            var o = Instantiate(barPrefab, pos, Quaternion.identity, this.transform);
            barPrefabList.Add(o);
        }
    }

}
