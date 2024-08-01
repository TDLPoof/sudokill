using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasComponentMover : MonoBehaviour
{
    Vector3 ogPos;
    
    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDelta = ogPos - Input.mousePosition;
        mouseDelta *= 20 / Utils.Length(mouseDelta);
        transform.position = Vector3.Lerp(transform.position, (5 * Random.insideUnitSphere) + ogPos + mouseDelta, 0.5f * Time.deltaTime);
    }
}
