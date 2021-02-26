using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

	public Transform player;
	public float playerDistance;
	public float awareAI = 10f;
	public float AIMoveSpeed;
	public GameObject PlayerObj;
	public float fieldOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 lastSighting;
	private SphereCollider col;
	// private Animator anim;
	// private LastPlayerSighting lastPlayerSighting;

	private float waitTime;
	public float startWaitTime;

	public Transform[] navPoint;
	public UnityEngine.AI.NavMeshAgent agent;
	public int destPoint = 0;
	public Transform goal;

	public static float enemyHealth;

	void Start()
	{
		col = GetComponent<SphereCollider>();
		col.radius = 5;
		lastSighting = transform.position;
		playerInSight = false;
		enemyHealth = 100;
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = navPoint[0].position;
		AIMoveSpeed = agent.speed;
		agent.autoBraking = false;

	}

	void Update()
	{
		
		if (enemyHealth <= 0)
			Destroy(gameObject);


		playerDistance = Vector3.Distance(player.position, transform.position);

		if (playerInSight)
		{
			Debug.DrawLine(transform.position + transform.up, transform.position + transform.up, Color.red);
			Debug.DrawLine(transform.position + transform.up, transform.position + transform.right, Color.red);
			Debug.DrawLine(transform.position + transform.up, transform.position - transform.right, Color.red);
			transform.LookAt(player);
			agent.destination = lastSighting;
			agent.speed = 7;
		}
		else
		{
			//Debug.Log(Vector3.Distance(transform.position, navPoint[destPoint].position));
			//Debug.Log(agent.destination);
			if (Vector3.Distance(transform.position, navPoint[destPoint].position) < 0.5f || Vector3.Distance(transform.position, lastSighting) < 0.5f)
			{
				//Debug.Log("changed");
				destPoint = (destPoint + 1) % navPoint.Length;
				agent.destination = navPoint[destPoint].position;
				waitTime = startWaitTime;
				agent.speed = 0;
				while (waitTime > 0)
				{
					waitTime -= Time.deltaTime;
				}
				
			}
			agent.speed = AIMoveSpeed;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		//playerInSight = true;
		//if (other.gameObject == player)
		//{
		//	Vector3 direction = other.transform.position - transform.position;
		//	float angle = Vector3.Angle(direction, transform.forward);

		//	if (angle < fieldOfViewAngle * 0.5f)
		//	{
		//		RaycastHit hit;
		//		Debug.DrawLine(transform.position + transform.up, direction.normalized, Color.red);
		//		//if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
		//		if(Physics.Linecast(transform.position, player.position, out hit))
		//		{
					
		//			if (hit.collider.gameObject == player && hit.collider.gameObject.l)
		//			{
		//				playerInSight = true;
		//				// last position
		//			}

		//		}
		//	}

		//}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == PlayerObj)
        {
			Vector3 direction = other.transform.position;
			LayerMask mask = LayerMask.GetMask("Default");
			RaycastHit hit;
			//Debug.Log(other);
			if (!Physics.Linecast(transform.position, direction, out hit, mask))
			{
				Debug.DrawLine(transform.position, direction, Color.red);
				//Debug.Log(transform.position);
				Debug.Log(hit.collider);
				playerInSight = true;
				lastSighting = player.position;
				
			}
            else
            {
				playerInSight = false;
			}
        }
        else
        {
			playerInSight = false;
        }
		
	}
	private void OnTriggerExit(Collider other)
	{
		playerInSight = false;
	}
}