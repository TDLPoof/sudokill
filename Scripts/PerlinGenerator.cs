using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinGenerator
{
    public static float[,] GenerateNoise(int x, int y, int numVectors, int depth)
    {
        float[,] noise = new float[x,y];
        List<Vector2> points = new List<Vector2>();
        for (int p = 0; p < numVectors; p++) // pick a number of imaginary points on the texture
        {
            points.Add(new Vector2(Random.Range(0f, x), Random.Range(0f, y)));
        }

        // loop through every point and find its distance from the closest point
        float maxDist = 0;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3 point = new Vector3(i, j);
                float closestDist = Utils.DistanceSQ(point, points[0]);
                for (int p = 1; p < points.Count; p++)
                {
                    if (Utils.DistanceSQ(point, points[p]) < closestDist)
                        closestDist = Utils.DistanceSQ(point, points[p]);
                }
                noise[i, j] = Mathf.Sqrt(closestDist);
                if ((closestDist) > maxDist) maxDist = (closestDist);
            }
        }

        // normalize every value between [0, 1] by dividing by max
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                noise[i, j] = 1 - (noise[i, j] / Mathf.Sqrt(maxDist));
            }
        }
        if (depth <= 1) return noise;
        else return TwoToOneMerge(noise, GenerateNoise(x, y, numVectors * 2, depth - 1));
    }

    private static float[,] TwoToOneMerge(float[,] a, float[,] b)
    {
        // pre-condition: lists have same dimensions
        int x = a.GetLength(0);
        int y = a.GetLength(1);
        float[,] result = new float[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                result[i, j] = (2 * a[i, j] + b[i, j]) / 3;
            }
        }
        return result;
    }
}
