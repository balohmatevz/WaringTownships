using UnityEngine;
using System.Collections;

public class CloseMe : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void INPUT_DoClose(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
