using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafingAI : MonoBehaviour
{
    public Vector2 strafingBounds;
    public float strafeSpeed;

    public float changeStrafeDirectionFrequency = 0.001f;

    GroundedPathFinder gpf;
    // Start is called before the first frame update
    void Start()
    {
        // precondition: enemy has a pathfinder script
        gpf = GetComponent<GroundedPathFinder>();

        // set defaults if i forgor to set values (i am a bit lazy)
        if (strafingBounds == Vector2.zero)
        {
            strafingBounds.x = gpf.targetDistance - 1;
            strafingBounds.y = gpf.targetDistance + 1;
        }
        if (strafeSpeed == 0)
        {
            strafeSpeed = gpf.speed * 0.75f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Utils.DistanceSQ(gpf.player.transform.position, transform.position);
        if (gpf.grounded && dist >= strafingBounds.x * strafingBounds.x && dist <= strafingBounds.y * strafingBounds.y)
        {
            // actual strafe code here
            gpf.rb.AddForce(transform.right * strafeSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate() // run things that should be framerate-independent here (like switching strafe dir)
    {
        if (Random.value < changeStrafeDirectionFrequency) strafeSpeed *= -1;
    }
}
