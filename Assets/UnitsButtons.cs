using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitsButtons : MonoBehaviour
{
    RectTransform rt;

    // Use this for initialization
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.sizeDelta = new Vector2(Mathf.Min(rt.childCount * 50, Screen.width), 100);
    }
}
