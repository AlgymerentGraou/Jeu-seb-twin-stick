using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    private Animator anim;

    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float agroDistance = 10f;
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float attackCooldown = 1.5f;

    private bool isAggro = false;
    private bool isAttacking = false;
    private bool isPlayerInAttackRange = false;

    private Vector3 lastPos;
    private Vector3 zombiSpeed;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = speed; // Applique la vitesse réglable dans l'inspector
        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Vérifier si le joueur entre dans la zone d'aggro
            if (distanceToPlayer <= agroDistance)
            {
                isAggro = true;
            }
            else
            {
                isAggro = false;
                isAttacking = false;
                isPlayerInAttackRange = false;
                agent.isStopped = true;
                if(anim != null)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", true);
                }
                
            }

            // Si le joueur est dans la zone d'aggro mais pas en attaque
            if (isAggro && !isPlayerInAttackRange && !isAttacking)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                if(anim != null)
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isIdle", false);
                }
                
            }

            // Vérifier si le zombie atteint la position du joueur
            if (distanceToPlayer <= stopDistance && !isAttacking)
            {
                agent.isStopped = true;

                if(anim != null)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isIdle", true);
                }
            }
                
        }

        // Gestion de la vitesse pour l'animation
        if (lastPos != transform.position)
        {
            zombiSpeed = transform.position - lastPos;
            zombiSpeed /= Time.deltaTime;
            lastPos = transform.position;
        }

        if(anim != null){anim.SetFloat("SpeedZ", zombiSpeed.magnitude);}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player && !isAttacking)
        {
            isPlayerInAttackRange = true;
            StartCoroutine(AttackLoop());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInAttackRange = false;
            isAttacking = false;
            agent.isStopped = false;

            if(anim != null)
            {   
                if (isAggro)
                {             
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);                
                }
            else
                {                
                anim.SetBool("isRunning", false);
                anim.SetBool("isIdle", true);                
                }
            }
            
        }
    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;
        agent.isStopped = true;

        while (isPlayerInAttackRange) // Continue d'attaquer tant que le joueur est dans la zone d'attaque
        {
            

            if(anim != null)
            {
                anim.SetTrigger("attack");
                anim.SetBool("isRunning", true);
                anim.SetBool("isIdle", false);
            }

            yield return new WaitForSeconds(attackCooldown);
        }

        // Quand le joueur quitte la zone d'attaque
        isAttacking = false;
        agent.isStopped = false;
        if(anim != null)
        {
            if (isAggro)
            {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
            }
            else
            {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
            }
        }    
        
    }
}
