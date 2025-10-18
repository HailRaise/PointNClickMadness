using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CloudPassing : MonoBehaviour
{
    private Light2D moonLight;
    public float baseIntensity = 1.2f;
    public float variation = 0.4f;
    public float speed = 0.5f;

    void Start()
    {
        moonLight = GetComponent<Light2D>();
    }

    void Update()
    {
        float wave = Mathf.PerlinNoise(Time.time * speed, 0f);
        moonLight.intensity = baseIntensity - (wave * variation);
    }
}
