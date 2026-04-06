using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickerPerlin2D : MonoBehaviour
{
    private Light2D _light;

    [Header("Flicker Settings")]
    public float baseIntensity = 1f;
    public float flickerAmount = 0.3f;
    public float flickerSpeed = 2f;

    [Header("Intensity Limits")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 2f;

    private float _seed;

    void Start()
    {
        _light = GetComponentInChildren<Light2D>(); // ← corrigé
        _seed = Random.Range(0f, 100f);

        if (_light == null)
            Debug.LogError("Aucune Light2D trouvée dans les enfants !");
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(_seed + Time.time * flickerSpeed, 0f);
        float rawIntensity = baseIntensity + (noise - 0.5f) * 2f * flickerAmount;
        _light.intensity = Mathf.Clamp(rawIntensity, minIntensity, maxIntensity);
    }
}