using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPtesting : MonoBehaviour
{
    PostProcessVolume m_Volume;
    Vignette m_Vignette;


    // Adjust this variable to change the frequency of oscillation
    public float frequency = 5f;

    // Adjust these variables to set the minimum and maximum intensity values
    public float minIntensity = 0.7f;
    public float maxIntensity = 0.72f;


    void Start()
    {
        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);
        m_Vignette.smoothness.Override(1f);
        m_Vignette.roundness.Override(0f);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);

    }
    void Update()
    {

        // Change vignette intensity using a sinus curve with adjusted frequency
        float intensity = Mathf.Sin(frequency * Time.time);

        // Map the intensity value to the range [0, 1]
        float normalizedIntensity = Remap(intensity, -1f, 1f, 0f, 1f);

        // Apply smoothstep function for smoother transition
        float smoothedIntensity = Mathf.SmoothStep(minIntensity, maxIntensity, normalizedIntensity);

        m_Vignette.intensity.value = smoothedIntensity;
    }

    // Helper function to remap a value from one range to another
    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return from2 + (value - from1) * (to2 - from2) / (to1 - from1);
    }





}
