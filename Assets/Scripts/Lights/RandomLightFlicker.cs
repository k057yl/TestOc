using System.Collections;
using UnityEngine;

public class RandomLightFlicker : MonoBehaviour
{
    public float minDelay = 0.1f;
    public float maxDelay = 0.5f;
    public Light[] lights;
    
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
    
    void Update()
    {
        foreach (Light light in lights)
        {
            if (Random.value < 0.01f)
            {
                light.enabled = !light.enabled;
                float delay = Random.Range(minDelay, maxDelay);
                StartCoroutine(ResetLight(light, delay));
            }
        }
    }

    IEnumerator ResetLight(Light light, float delay)
    {
        yield return new WaitForSeconds(delay);
        light.enabled = !light.enabled;
    }
}
