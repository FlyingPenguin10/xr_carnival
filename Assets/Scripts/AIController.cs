using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Patrol,
    Chase,
    Interact,
    Flee
}

public class AIController : MonoBehaviour
{
    [Header("AI Components")]
    public NavMeshAgent agent;
    public Animator animator;
    public Transform player;

    [Header("State Settings")]
    public AIState currentState = AIState.Patrol;
    private AIState previousState;

    [Header("Detection Settings")]
    public float detectionRange = 10f;
    public float chaseRange = 15f;
    public float personalSpaceRange = 2f;
    public float fieldOfViewAngle = 120f;
    public LayerMask detectionLayers;

    [Header("Movement Settings")]
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3.5f;
    public float rotationSpeed = 5f;

    [Header("Patrol Settings")]
    public Transform[] patrolWaypoints;
    public float waypointReachDistance = 0.5f;
    public float idleTimeAtWaypoint = 2f;
    private int currentWaypointIndex = 0;
    private float idleTimer = 0f;

    [Header("Interaction Settings")]
    public float interactionDistance = 3f;
    public string[] interactionMessages = { "Welcome to the carnival!", "Enjoy the games!", "Have fun!" };

    [Header("Debug")]
    public bool showDebugGizmos = true;

    private float distanceToPlayer;
    private bool canSeePlayer;

    private void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.speed = patrolSpeed;
        ChangeState(AIState.Patrol);
    }

    private void Update()
    {
        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            canSeePlayer = CanSeePlayer();
        }

        UpdateState();
        UpdateAnimator();
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Idle:
                HandleIdleState();
                break;

            case AIState.Patrol:
                HandlePatrolState();
                break;

            case AIState.Chase:
                HandleChaseState();
                break;

            case AIState.Interact:
                HandleInteractState();
                break;

            case AIState.Flee:
                HandleFleeState();
                break;
        }
    }

    private void HandleIdleState()
    {
        agent.isStopped = true;

        if (canSeePlayer && distanceToPlayer < detectionRange)
        {
            ChangeState(AIState.Chase);
            return;
        }

        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            ChangeState(AIState.Patrol);
        }
    }

    private void HandlePatrolState()
    {
        if (patrolWaypoints == null || patrolWaypoints.Length == 0)
        {
            Debug.LogWarning("No patrol waypoints assigned!");
            ChangeState(AIState.Idle);
            return;
        }

        if (canSeePlayer && distanceToPlayer < detectionRange)
        {
            ChangeState(AIState.Chase);
            return;
        }

        agent.isStopped = false;
        agent.speed = patrolSpeed;

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];
        agent.SetDestination(targetWaypoint.position);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointReachDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            idleTimer = idleTimeAtWaypoint;
            ChangeState(AIState.Idle);
        }
    }

    private void HandleChaseState()
    {
        if (!canSeePlayer || distanceToPlayer > chaseRange)
        {
            ChangeState(AIState.Patrol);
            return;
        }

        if (distanceToPlayer <= interactionDistance)
        {
            ChangeState(AIState.Interact);
            return;
        }

        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void HandleInteractState()
    {
        agent.isStopped = true;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        if (distanceToPlayer > interactionDistance + 1f)
        {
            ChangeState(AIState.Chase);
        }
        else if (distanceToPlayer < personalSpaceRange)
        {
            ChangeState(AIState.Flee);
        }
    }

    private void HandleFleeState()
    {
        if (distanceToPlayer > personalSpaceRange * 2f)
        {
            ChangeState(AIState.Patrol);
            return;
        }

        agent.isStopped = false;
        agent.speed = chaseSpeed;

        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * 5f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleePosition, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, detectionRange, detectionLayers))
            {
                if (hit.transform == player || hit.transform.IsChildOf(player))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ChangeState(AIState newState)
    {
        if (currentState == newState) return;

        OnStateExit(currentState);
        previousState = currentState;
        currentState = newState;
        OnStateEnter(newState);

        Debug.Log($"AI State changed: {previousState} → {currentState}");
    }

    private void OnStateEnter(AIState state)
    {
        switch (state)
        {
            case AIState.Interact:
                if (interactionMessages.Length > 0)
                {
                    string message = interactionMessages[Random.Range(0, interactionMessages.Length)];
                    Debug.Log($"Remy says: {message}");
                }
                break;
        }
    }

    private void OnStateExit(AIState state)
    {
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsMoving", speed > 0.1f);
    }

    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRange);

        if (player != null)
        {
            Gizmos.color = canSeePlayer ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up, player.position + Vector3.up);
        }

        if (patrolWaypoints != null && patrolWaypoints.Length > 0)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < patrolWaypoints.Length; i++)
            {
                if (patrolWaypoints[i] != null)
                {
                    Gizmos.DrawSphere(patrolWaypoints[i].position, 0.3f);
                    
                    int nextIndex = (i + 1) % patrolWaypoints.Length;
                    if (patrolWaypoints[nextIndex] != null)
                    {
                        Gizmos.DrawLine(patrolWaypoints[i].position, patrolWaypoints[nextIndex].position);
                    }
                }
            }
        }
    }
}
