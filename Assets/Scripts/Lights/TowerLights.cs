using UnityEngine;

public class TowerLights : MonoBehaviour
{
    public float flickerFrequency = 2f;
    public Light[] lights;
    
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
    
    void Update()
    {
        float flickerDelay = 1f / flickerFrequency;

        foreach (Light light in lights)
        {
            if (Time.time % flickerDelay < flickerDelay / 2f)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }
}
