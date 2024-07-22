using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector2 sensitivity;
    public Transform player;

    float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // time to comment my code :(
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // get mouse x and y from input manager (use deltaTime to handle framerate)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = -1 * Input.GetAxis("Mouse Y") * sensitivity.y * Time.deltaTime;

        // use mouseData to rotate player for horizontal, rotate camera only for vertical
        if (!Input.GetKey(KeyCode.K))
        {
            player.Rotate(Vector3.up * mouseX);
            xRotation += mouseY;
            if (xRotation > 80f) xRotation = 80f;
            if (xRotation < -70f) xRotation = 70f;

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}
