using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;

    Animator animator;
    BoxCollider boxCollider;
    bool playerInSight, playerInAttackRange;
    

    public override void Enter()
    {
        agent = enemy.agent;
        agent.speed = 4f;
        player = enemy.player;
        playerLayer = LayerMask.GetMask("Player");
        animator = enemy.GetComponent<Animator>();
        boxCollider = enemy.GetComponentInChildren<BoxCollider>();
    }
    public override void Perform()
    {
        float distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        playerInSight = distance < enemy.sightDistance;
        playerInAttackRange = distance < enemy.attackRange;

        if(playerInSight && !playerInAttackRange) Chase();
        if(playerInSight && playerInAttackRange) Attack();


/*
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            if(moveTimer > Random.Range(2,6))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                // change to patrol state
                stateMachine.ChangeState(new PatrolState());
            }
        }
*/
    }
    public override void Exit()
    {

    }
    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    void Chase()
    {
        Debug.Log("in the chase method");
        agent.SetDestination(player.transform.position);
    }
    void Attack()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack_TwoHanded"))
        {
            animator.SetTrigger("Attack");
            agent.SetDestination(enemy.transform.position);
        }
    }
    void ChangeToPatrol()
    {
        stateMachine.ChangeState(new PatrolState());
    }


}
