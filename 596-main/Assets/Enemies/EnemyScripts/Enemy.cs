using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    public NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    // just for debugging purposes.
    [SerializeField] 
    private string currentState;
    public Path path;
    public GameObject player;
    public float sightDistance = 30f;
    public float attackRange = 1f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    public bool attackMode = false; // flag to indicate if the player activated horde/attack mode
    Animator animator;
    BoxCollider boxCollider;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();

        // Create a new instance of PatrolState
        PatrolState patrolState = new PatrolState();

        stateMachine.Initialize(patrolState);

        player = GameObject.FindGameObjectWithTag("Player");
        
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attackMode)
        {
            CanSeePlayer();
        }
        else
        {
            stateMachine.ChangeState(new AttackState());
        }
        currentState = stateMachine.activeState.ToString();

    }

    public bool CanSeePlayer(){
        if (player != null)
        {
            // is player close enough to be seen?
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {   
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                        return true;
                    }

                    
                }
                return false;
            }
        }
        return false;
    }
    public void SetAttackMode()
    {
        attackMode = true;
        Debug.Log("Enemy was set to attack mode");
        stateMachine.ChangeState(new AttackState());
    }
    void EnableAttack(){
        boxCollider.enabled = true;
    }
    void DisableAttack(){
        boxCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //var player = other.GetComponent<Movement>();
        
        if(other.gameObject.name == "P_LPSP_FP_CH")
        {
            //here, we can adjust the player's HP
            Debug.Log("Hit");
            playerHealth.TakeDamage(5);
        }
    }
}

