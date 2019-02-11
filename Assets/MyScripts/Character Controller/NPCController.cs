using UnityEngine.AI;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    int index;
    float speed, agentSpeed;
    Transform player;
    Animator animator;
    NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agentSpeed = agent.speed;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f); //инвоки есть в BaseObjecScene
        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol",Random.Range(0,patrolTime),patrolTime);
        }

    }

    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude);
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        if(waypoints.Length>0)
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 8;

        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)// дописать рейкаст на проверку препятствий
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,aggroRange);
    }


}
