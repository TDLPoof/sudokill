using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int dimensions;
    public GameObject mapTile;
    public float minHeight, maxHeight, intensity = 2;
    public Material mapMaterial;
    public Gradient colorGradient;
    [HideInInspector]
    public float[,] noisemap;
    int pointCount = 8;
    int depth = 4;
    Color lineColor = Color.white;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < dimensions.x; i++)
        {
            for (int j = 0; j < dimensions.y; j++)
            {
                Vector2 pos = new Vector2(2 * i, 2 * j);
                Instantiate(mapTile, new Vector3(pos.x, 0, pos.y), Quaternion.identity, transform);
            }
        }
        noisemap = GenerateNoisemap();
    }

    public float[,] GenerateNoisemap()
    {
        time = GameObject.Find("Wave Controller").GetComponent<WaveController>().wave / 50f;
        time = Mathf.Clamp(time, 0, 1f);
        return PerlinGenerator.GenerateNoise(dimensions.x, dimensions.y, pointCount, depth);

    }

    // Update is called once per frame
    void Update()
    {
        lineColor = Color.Lerp(lineColor, colorGradient.Evaluate(time), 2f * Time.deltaTime);
        mapMaterial.SetColor("_EmissionColor", lineColor);
        for (int i = 0; i < noisemap.GetLength(0); i++)
        {
            for (int j = 0; j < noisemap.GetLength(1); j++)
            {
                SetTile(i, j);
            }
        }
    }

    public void SetTile(int i, int j)
    {
        Transform tile = transform.GetChild((i * dimensions.x) + j);
        float heightValue = minHeight + (maxHeight - minHeight) * Mathf.Pow(noisemap[i, j], intensity);
        tile.position = Vector3.Lerp(tile.position, new Vector3(tile.position.x, heightValue, tile.position.z), 0.5f * Time.deltaTime);
    }
}
