using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TwinStickController2D : MonoBehaviour
{
    public float playerSpeed;
    public float deadZone;

    private Rigidbody2D rb;
    private Vector2 leftStickInput;
    private Vector2 rightStickInput;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        leftStickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (leftStickInput.magnitude < deadZone)
        {
            leftStickInput = Vector2.zero;
        }

        rightStickInput = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical"));
        if (rightStickInput.magnitude < deadZone)
        {
            rightStickInput = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector2 curMovement = leftStickInput * playerSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + curMovement);

        if(rightStickInput.magnitude > 0f)
        {
            Vector3 curRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
            Quaternion playerRotation = Quaternion.LookRotation(curRotation, Vector3.forward);

            rb.SetRotation(playerRotation);
        }
    }
}
