using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgScaler : MonoBehaviour
{
    private float height;
    private float width;
    void Start()
    {
        height = Camera.main.orthographicSize * 2f;
        width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width, height, 1f);
    }

    void Update()
    {

    }
}
