using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OptionSliders : MonoBehaviour
{
    public CameraControl camera;
    public Volume volume;
    MotionBlur motionBlur;

    public Slider sensitivitySlider, motionBlurSlider;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet<MotionBlur>(out MotionBlur mb);
        motionBlur = mb;
    }

    // Update is called once per frame
    void Update()
    {
        camera.sensitivity = sensitivitySlider.value;
        motionBlur.intensity.value = motionBlurSlider.value;
    }
}
