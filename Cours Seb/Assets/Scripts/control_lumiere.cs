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

    [Header("Fade In")]
    public float fadeInDuration = 1.5f; // ← durée du fade en secondes

    private float _seed;
    private float _fadeTimer = 0f;
    private bool _isFadingIn = false;

    void Start()
    {
        _light = GetComponentInChildren<Light2D>();
        _seed = Random.Range(0f, 100f);

        if (_light == null)
            Debug.LogError("Aucune Light2D trouvée dans les enfants !");
    }

    // appelé par LightToggle quand la lumière s'allume
    public void StartFadeIn()
    {
        _fadeTimer = 0f;
        _isFadingIn = true;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(_seed + Time.time * flickerSpeed, 0f);
        float rawIntensity = baseIntensity + (noise - 0.5f) * 2f * flickerAmount;
        float targetIntensity = Mathf.Clamp(rawIntensity, minIntensity, maxIntensity);

        if (_isFadingIn)
        {
            _fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_fadeTimer / fadeInDuration);

            // l'intensité monte progressivement de 0 vers la valeur cible
            _light.intensity = Mathf.Lerp(0f, targetIntensity, t);

            if (t >= 1f)
                _isFadingIn = false;
        }
        else
        {
            _light.intensity = targetIntensity;
        }
    }
}