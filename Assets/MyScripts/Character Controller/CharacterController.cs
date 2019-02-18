
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public AttackDefenition demoAttack; 
    Animator animator;
    NavMeshAgent agent;
    CharacterStats stats;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
    }


    void Update()
    {
        animator.SetFloat("Speed",agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination)
    {
        agent.destination=destination;
    }

    public void AttackTarget(GameObject target)
    {
        var attack = demoAttack.CreateAttack(stats, target.GetComponent<CharacterStats>());

        var attackables = target.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable attackable in attackables)
        {
            attackable.OnAttack(gameObject,attack);
        }
    }
}
