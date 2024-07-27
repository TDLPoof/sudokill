using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 1500;
    public Transform player;

    float yRotation = 0;

    // Start is called before the first frame update

    // time to comment my code :(
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // get mouse x and y from input manager (use deltaTime to handle framerate)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = -1 * Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // use mouseData to rotate player for horizontal, rotate camera only for vertical
        if (!Input.GetKey(KeyCode.K))
        {
            player.Rotate(Vector3.up * mouseX);
            yRotation += mouseY;
            if (yRotation > 80f) yRotation = 80f;
            if (yRotation < -70f) yRotation = -70f;

            transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 offsetEuler = transform.localRotation.eulerAngles + new Vector3(-10, 0, 0);
            transform.localRotation = Quaternion.Euler(offsetEuler.x, offsetEuler.y, offsetEuler.z);
            transform.localPosition = Vector3.zero;
        }
        else transform.localPosition = new Vector3(0, 0.8f, 0);
    }
}
