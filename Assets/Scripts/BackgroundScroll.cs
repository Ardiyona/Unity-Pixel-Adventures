using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    Material material;
    Vector2 offset;

    public float x, y;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset = new Vector2(x, y);
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
