using UnityEngine;
using Unity.Cinemachine; // ← corrigé

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    private CinemachineImpulseSource _impulseSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        _impulseSource = GetComponent<CinemachineImpulseSource>();

        if (_impulseSource == null)
            Debug.LogError("Aucun CinemachineImpulseSource trouvé !");
    }

    public void Shake(float force = 1f)
    {
        _impulseSource.GenerateImpulse(force);
    }
}