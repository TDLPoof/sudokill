using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraVisuals : MonoBehaviour
{
    public bool doFovBoost, doAberration;
    public Volume postProcessingVolume;

    Vector3 lastPosition, velocity;
    Vignette vignette;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        postProcessingVolume.profile.TryGet<Vignette>(out Vignette v);
        vignette = v;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        // dot product allows us to scale based on how the player is looking
        float directionVelocity = transform.forward.x * velocity.x + transform.forward.z * velocity.z;
        directionVelocity = 1 - Mathf.Clamp(directionVelocity * 0.25f, 1, -1);
        if (doFovBoost)
        {
            GetComponent<Camera>().fieldOfView = 60 + (5 * FOVCurve(Length(velocity * 0.25f)) * directionVelocity);
        }

        if (doAberration)
        {
            postProcessingVolume.profile.TryGet<ChromaticAberration>(out ChromaticAberration aberration);
            aberration.intensity.value = 0.1f + (0.4f * FOVCurve(Length(velocity * 0.25f)) * directionVelocity);
        }
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0, 0.1f);
        lastPosition = transform.position;
    }

    float Length(Vector3 vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
    }

    float FOVCurve(float vel) // custom curve chosen for its horizontal asymptote
    {
        return -Mathf.Exp(-0.5f * vel) + 1;
    }

    public void DamageFlash()
    {
        vignette.intensity.value = 0.5f;
    }
}
