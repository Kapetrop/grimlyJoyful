using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class moveEnnemi : MonoBehaviour
{
    public Transform perso;
    public Transform home;
    public NavMeshAgent agent;

    private Transform maison;
    private Transform goal;
    void Start()
    {
        maison = home;
        goal = perso;

        InvokeRepeating("EnDeplacement",0.2f, 0.2f);
    }

    public void EnDeplacement()
    {
        if(agent.remainingDistance < 1.5f && agent.pathPending == false)
        {
        if(goal == perso)
            {
                goal = maison;
            }else if(goal == maison)
            {
                goal = perso;
            }
        }
        agent.destination = goal.position;
    }
}
