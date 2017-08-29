using UnityEngine;
using System.Collections;

/// <summary>
/// Controls Enemy AI navigation.
/// 
/// Synthesized from MysterySoftware's code at
/// http://forum.unity3d.com/threads/cant-make-a-simple-patrol-waypoint.272336/
/// and Griffo's code at
/// http://answers.unity3d.com/questions/429623/enemy-movement-from-waypoint-to-waypoint.html
/// </summary>

public class EnemyNav : MonoBehaviour {
	public Transform[] waypoints;

	/// <summary>
	/// How fast the enemy will move from waypoint to waypoint.
	/// </summary>
	public float patrolSpeed = 2F;

	/// <summary>
	/// How close the enemy needs to be to a waypoint for the enemy to have
	/// reached the waypoint.
	/// </summary>
	public float minimumWaypointDistance = 2F;

	/// <summary>
	/// How long the enemy should pause at a waypoint before moving to the next.
	/// </summary>
	public float pauseDuration = 3F;

	/// <summary>
	/// How long the enemy should chase the player before returning to patrolling.
	/// </summary>
	public float chaseDuration = 10F;

	public bool isChasingPlayer = false;

	// Reference to the enemy's NavMeshAgent.
	private UnityEngine.AI.NavMeshAgent nma;

	// The waypoint the enemy is currently moving towards.
	private int currentWaypoint = 0;

	// The last waypoint in the array.
	private int finalWaypoint;

	// For use with timing the pause.
	private float pauseTimer = 0;

	// For use with chasing the player.
	private float chaseTimer = 0;

	// For chasing the player.
	private Transform player;

	void Start() {
		nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		finalWaypoint = waypoints.Length - 1;

		// Set the NavMeshAgent's speed to the desired speed.
		nma.speed = patrolSpeed;
	}

	void Update() {
		if (isChasingPlayer) {
			chase();
		} else {
			patrol();
		}
	}

	// If the player has stayed near the enemy long enough,
	// have the enemy chase the player.
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			isChasingPlayer = true;
			chaseTimer = chaseDuration;
		}
	}

	void patrol() {
		// Store the enemy's current position
		Vector3 tempLocalPosition;

		// Store the position of the waypoint the enemy is moving towards.
		Vector3 target;

		// Set to the enemy's current position with 0 as the y-position.
		tempLocalPosition = transform.position;
		tempLocalPosition.y = 0F;

		// Set to the waypoint the enemy is currently moving towards
		// with 0 as the y-position.
		target = waypoints[currentWaypoint].position;
		target.y = 0F;

		// Is the enemy close enough to the waypoint to say that the enemy has reached
		// the waypoint?
		// If so, check to see if the enemy has reached the last waypoint.
		// If the enemy has reached the last waypoint, tell it to loop back around to the first.
		// If the enemy has not reached the last waypoint, tell it to go to the next waypoint.
		if (Vector3.Distance(tempLocalPosition, target) <= minimumWaypointDistance) {
			// Causes the enemy to pause on waypoints.
			if (pauseTimer <= 0) {
				pauseTimer = Time.time;
			}
			// If the enemy has stayed longer than the pause duration,
			// have the enemy move to the next waypoint.
			if ((Time.time - pauseTimer) >= pauseDuration) {
				if (currentWaypoint == finalWaypoint) {
					currentWaypoint = 0;
					pauseTimer = 0;
				} else {
					currentWaypoint++;
					pauseTimer = 0;
				}
			}
		}

		// Considering the result of the previous if statement, tell the enemy to go to the
		// appropriate waypoint.
		nma.SetDestination(waypoints[currentWaypoint].position);
	}

	void chase() {
		chaseTimer -= Time.deltaTime;

		nma.SetDestination(player.position);

		// Have the enemy target the player until the timer reaches zero.
		if (chaseTimer <= 0) {
			isChasingPlayer = false;
		}
	}
}
