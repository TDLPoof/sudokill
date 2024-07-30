using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TextureTester : MonoBehaviour
{
    Texture2D texture;
    public Vector2Int resolution;
    public int pointCount, depth;
    float[,] noisemap; // a 2d array
    public int accentuation = 1;

    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(resolution.x, resolution.y, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Point;
        GetComponent<Renderer>().material.SetTexture("_BaseMap", this.texture);
        noisemap = PerlinGenerator.GenerateNoise(resolution.x, resolution.y, pointCount, depth);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.N)) noisemap = PerlinGenerator.GenerateNoise(resolution.x, resolution.y, pointCount, depth);
        for (int i = 0; i < resolution.x; i++)
        {
            for (int j = 0; j < resolution.y; j++)
            {
                float value = Mathf.Pow(noisemap[i, j], accentuation);
                texture.SetPixel(i, j, new Color(value, value, value));
            }
        }
        texture.Apply();
    }
}
