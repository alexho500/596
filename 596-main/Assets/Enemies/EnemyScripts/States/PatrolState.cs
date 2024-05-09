using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    // track which waypoint we are currently targeting
    public int waypointIndex;
    public float waitTimer;
    public override void Enter(){

    }
    public override void Perform(){
        PatrolCycle();
        if (enemy.CanSeePlayer() && enemy.attackMode)
        {
            enemy.SetAttackMode();
        }
    }
    public override void Exit(){

    }
    public void PatrolCycle(){
        // patrol logic
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= Random.Range(3f, 15f)){
                if(waypointIndex < enemy.path.waypoints.Count - 1){
                    waypointIndex = Random.Range(0, 14);
                }
                else{
                        waypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
            waitTimer = 0;
            }
        }
    }
}
