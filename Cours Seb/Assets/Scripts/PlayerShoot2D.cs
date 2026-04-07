using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot2D : MonoBehaviour
{
    [Tooltip("ShootingPoint Transform must be outside of the Avatar Collider")]
    public Transform shootingPoint;
    public int ammoCount;

    [Header("Prefab Settings")]
    [Tooltip("Specify HitTag & Damages in Bullet Script.")]
    public GameObject bulletPrefab;
    public float bulletSpeed;

    [Header("Cooldown")]
    public float cooldown = 0.5f; // ← temps entre chaque tir en secondes
    private float _cooldownTimer = 0f;

    private PlayerControls _controls;
    private Vector2 _aimDirection = Vector2.right;

    void Awake()
    {
        _controls = new PlayerControls();

        _controls.Player.Aim.performed += ctx => _aimDirection = ctx.ReadValue<Vector2>();
        _controls.Player.Aim.canceled += ctx => { };

        _controls.Player.Shoot.performed += ctx => Shoot();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Update()
    {
        // décrémente le timer à chaque frame
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        if (ammoCount <= 0) return;
        if (_aimDirection == Vector2.zero) return;

        // bloque le tir si le cooldown n'est pas écoulé
        if (_cooldownTimer > 0f) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = _aimDirection.normalized * bulletSpeed;

        // remet le timer à sa valeur max
        _cooldownTimer = cooldown;

        ammoCount--;
        print("Il reste " + ammoCount + " balles");
    }
}