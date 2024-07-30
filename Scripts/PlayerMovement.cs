using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 moveSpeed;
    public float gravity = -9.81f;
    public float drag = 0.2f;

    CharacterController cc;

    [HideInInspector]
    public Vector3 velocity;

    public Transform groundCheck;
    public float groundedDistance;
    public LayerMask validGround;

    bool grounded;
    Vector3 slideDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // get player's rigidbody component
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // use checksphere to test for groundedness
        grounded = Physics.CheckSphere(groundCheck.position, groundedDistance, validGround.value);
        if (grounded && velocity.y < 0) velocity.y = 0;

        // get player movement inputs from input manager
        float moveX = Input.GetAxis("Horizontal") * moveSpeed.x * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed.z * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            /*if (Mathf.Abs(velocity.z) < 0.001f && Mathf.Abs(velocity.x) < 0.001f)
                slideDir = transform.forward * moveZ * 1.5f;*/
            slideDir = (transform.right * moveX + transform.forward * moveZ) * 1.5f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
            cc.Move(slideDir);
        else cc.Move(transform.right * moveX + transform.forward * moveZ);

        // jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpCo = Input.GetKey(KeyCode.LeftControl) ? 1.5f : 1f;
            velocity.y = moveSpeed.y * Time.deltaTime * jumpCo;
        }

        // add physics! (important for epic stuff)
        cc.Move(velocity);

        // add gravity
        velocity.y += gravity * 0.5f * Time.deltaTime;

        // add simple drag
        velocity /= 1 + (drag * Time.deltaTime);

    }
}
