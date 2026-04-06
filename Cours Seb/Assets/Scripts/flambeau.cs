using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightToggle : MonoBehaviour
{
    [Header("Light Settings")]
    public bool isLightOn = true;

    [Header("Fire Animation")]
    public float minAnimSpeed = 0.5f;
    public float maxAnimSpeed = 2f;

    [Header("Ignition Burst")]
    public int burstCount = 30; // ← nombre de particules au démarrage

    private Light2D _light;
    private GameObject _fireSprite;
    private Animator _animator;
    private ParticleSystem _etincelles;
    private LightFlickerPerlin2D _flickerScript;
    private bool _previousState;

    void Start()
    {
        _light = GetComponentInChildren<Light2D>();
        _fireSprite = transform.Find("Feu").gameObject;
        _animator = _fireSprite.GetComponent<Animator>();
        _flickerScript = GetComponent<LightFlickerPerlin2D>();
        _etincelles = transform.Find("Etincelles").GetComponent<ParticleSystem>();

        if (_light == null) Debug.LogError("Aucune Light2D trouvée !");
        if (_fireSprite == null) Debug.LogError("Aucun GameObject 'Feu' trouvé !");
        if (_animator == null) Debug.LogError("Aucun Animator trouvé sur 'Feu' !");
        if (_flickerScript == null) Debug.LogError("Aucun LightFlickerPerlin2D trouvé !");
        if (_etincelles == null) Debug.LogError("Aucun Particle System 'Etincelles' trouvé !");

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

        if (isLightOn)
        {
            _etincelles.Play();

            // burst d'allumage : envoie un gros shot de particules d'un coup
            _etincelles.Emit(burstCount);

            // démarre le fade in de la lumière
            _flickerScript.StartFadeIn();
        }
        else
        {
            _etincelles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void SyncAnimationToLight()
    {
        float t = Mathf.InverseLerp(
            _flickerScript.minIntensity,
            _flickerScript.maxIntensity,
            _light.intensity
        );

        _animator.speed = Mathf.Lerp(minAnimSpeed, maxAnimSpeed, t);

        var emission = _etincelles.emission;
        emission.rateOverTime = Mathf.Lerp(5f, 20f, t);
    }
}