using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent =GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(target !=null)
        {
            agent.SetDestination(target.position);
        }
    }
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget (Interactable newTarget)
    {
        target = newTarget.transform;
    }

    public void StopFollowTarget()
    {
        target = null;
    }
}
