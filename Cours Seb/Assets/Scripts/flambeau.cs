using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggle : MonoBehaviour
{
    [Header("Light Settings")]
    public bool isLightOn = true;

    [Header("Fire Animation")]
    public float minAnimSpeed = 0.5f;
    public float maxAnimSpeed = 2f;

    private Light2D _light;
    private GameObject _fireSprite;
    private Animator _animator;
    private ParticleSystem _etincelles; // ← nouveau
    private bool _previousState;
    private LightFlickerPerlin2D _flickerScript;

    void Start()
    {
        _light = GetComponentInChildren<Light2D>();
        _fireSprite = transform.Find("Feu").gameObject;
        _animator = _fireSprite.GetComponent<Animator>();
        _flickerScript = GetComponent<LightFlickerPerlin2D>();
        _etincelles = transform.Find("Etincelles").GetComponent<ParticleSystem>(); // ← nouveau

        if (_etincelles == null)
            Debug.LogError("Aucun Particle System 'Etincelles' trouvé !");

        ApplyState();
        _previousState = isLightOn;
    }

    void Update()
    {
        if (isLightOn != _previousState)
        {
            ApplyState();
            _previousState = isLightOn;
        }

        if (isLightOn)
            SyncAnimationToLight();
    }

    void ApplyState()
    {
        _light.gameObject.SetActive(isLightOn);
        _fireSprite.SetActive(isLightOn);

        // allume ou éteint les particules proprement
        if (isLightOn)
            _etincelles.Play();
        else
            _etincelles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void SyncAnimationToLight()
    {
        float t = Mathf.InverseLerp(
            _flickerScript.minIntensity,
            _flickerScript.maxIntensity,
            _light.intensity
        );

        _animator.speed = Mathf.Lerp(minAnimSpeed, maxAnimSpeed, t);

        // bonus : le nombre de particules suit aussi l'intensité
        var emission = _etincelles.emission;
        emission.rateOverTime = Mathf.Lerp(5f, 20f, t);
    }
}