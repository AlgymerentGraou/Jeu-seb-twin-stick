using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private PlayerControls controls;
    private NavMeshAgent agent;

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
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // déplacement sur NavMesh
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        agent.Move(move * speed * Time.deltaTime);

        // rotation stick droit
        if (aimInput.sqrMagnitude > 0.1f)
        {
            angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;

        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}