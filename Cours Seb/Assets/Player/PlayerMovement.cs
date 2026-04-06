using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private PlayerControls controls;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 aimInput;
    private float angle;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Player.Aim.canceled += ctx => aimInput = Vector2.zero;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // ⚠️ On utilise FixedUpdate pour tout ce qui touche au Rigidbody
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        // rotation stick droit
        if (aimInput.sqrMagnitude > 0.1f)
        {
            angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}