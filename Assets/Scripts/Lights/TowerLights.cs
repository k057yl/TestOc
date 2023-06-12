using UnityEngine;

public class TowerLights : MonoBehaviour
{
    private float _flickerFrequency = Constants.MIN_DELAY;
    public Light[] lights;
    
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
    
    void Update()
    {
        float flickerDelay = Constants.ONE / _flickerFrequency;

        foreach (Light light in lights)
        {
            if (Time.time % flickerDelay < flickerDelay / Constants.TWO)
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
