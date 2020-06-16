using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollRate = 0.2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, scrollRate * Time.deltaTime, 0));
    }
}
