using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot2D : MonoBehaviour
{
    [Tooltip("ShootingPoint Transform must be outside of the Avatar Collider")]
    public Transform shootingPoint;
    public int ammoCount; // ← int au lieu de float

    [Header("Prefab Settings")]
    [Tooltip("Specify HitTag & Damages in Bullet Script.")]
    public GameObject bulletPrefab; // ← renommé en bulletPrefab
    public float bulletSpeed;

    private PlayerControls _controls;
    private Vector2 _aimDirection = Vector2.right;

  void Awake()
{
    _controls = new PlayerControls();

    _controls.Player.Aim.performed += ctx => _aimDirection = ctx.ReadValue<Vector2>();
    _controls.Player.Aim.canceled += ctx => { }; // ← on ne réinitialise plus la direction

    _controls.Player.Shoot.performed += ctx => Shoot();
}

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    public void Update()
    {
        //Debug.Log(_aimDirection);        
    }

    void Shoot()
    {
        if (ammoCount <= 0) return;

        // si le stick est au repos, on ne tire pas
        if (_aimDirection == Vector2.zero) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = _aimDirection.normalized * bulletSpeed;
        ammoCount--;
        Debug.Log("alors : " + _aimDirection.normalized * bulletSpeed + " et  " + _aimDirection);

        //print("Il reste " + ammoCount + " balles");
    }
}