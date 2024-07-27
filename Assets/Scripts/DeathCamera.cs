using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    public bool activated;
    public Transform deathTarget;
    public GameObject[] deactivateOnDeath, activateOnDeath;
    public TipGenerator tipGenerator;

    Vector3 deathPos; Quaternion targetRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 0.05f);
            transform.LookAt(deathTarget);
            transform.position = Vector3.Lerp(transform.position, deathPos, 0.05f * Time.deltaTime);
        }
        
    }

    public void Activate()
    {
        activated = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathTarget.SetParent(null);
        transform.SetParent(null);
        GetComponent<CameraVisuals>().enabled = false;
        GetComponent<CameraControl>().enabled = false;
        foreach (GameObject gameObject in deactivateOnDeath)
        {
            gameObject.SetActive(false);
        }
        foreach (GameObject gameObject in activateOnDeath)
        {
            gameObject.SetActive(true);
        }
        deathPos = transform.position + (transform.forward * -10) + new Vector3(0, 5, 0);
        tipGenerator.NewTip();
    }
}
