using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;	
	[SerializeField]
	Transform target;	
	NavMeshAgent agent; 
	CharacterCombat combat;
	
	public LayerMask whatIsGround, whatIsPlayer;
	public float health;

    	//Patroling
    	public Vector3 walkPoint;
    	bool walkPointSet;
    	public float walkPointRange;

    	//Attacking
    	public float timeBetweenAttacks;
    	bool alreadyAttacked;
    	public GameObject projectile;
	public float sightRange, attackRange;
    	public bool playerInSightRange, playerInAttackRange;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
	}

	void Update () {
		float distance = Vector3.Distance(target.position, transform.position);

		if (distance <= lookRadius)
		{
			agent.SetDestination(target.position);
			if (distance <= agent.stoppingDistance)
			{
				AttackPlayer();
				FaceTarget();
			}
		}



	}

	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}



   

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(target);
		HPcontroller hp = target.GetComponent<HPcontroller>();
        hp.TakeDamage(10f);

        if (!alreadyAttacked)
        {
            ///Attack code here
            
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

  
}
