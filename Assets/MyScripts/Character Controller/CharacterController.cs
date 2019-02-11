
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude);
    }
}
