using System.Collections;
using UnityEngine;

public class RandomLightFlicker : MonoBehaviour
{
    public Light[] lights;
    
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
    
    void Update()
    {
        foreach (Light light in lights)
        {
            if (Random.value < Constants.DELAY)
            {
                light.enabled = !light.enabled;
                float delay = Random.Range(Constants.MIN_DELAY, Constants.MAX_DELAY);
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
