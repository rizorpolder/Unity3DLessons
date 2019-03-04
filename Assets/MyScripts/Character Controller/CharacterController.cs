using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CharacterController : MonoBehaviour,IDestructable
{
    public AttackDefenition demoAttack; 
    Animator animator;
    NavMeshAgent agent;
    CharacterStats stats;

    private GameObject atttackTarget;

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
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination=destination;
    }

    public void AttackTarget(GameObject target)
    {
        var weapon = stats.GetCurrentWeapon();
        if (weapon != null)
        {
            StopAllCoroutines();
            agent.isStopped = false;
            atttackTarget = target;
            StartCoroutine(PursueAndAttackTarget());
        }
    }

    private IEnumerator PursueAndAttackTarget()
    {
        //agent.isStopped = false;
        var weapon = stats.GetCurrentWeapon();

        while(Vector3.Distance(transform.position,atttackTarget.transform.position)>weapon.Range)
        {
            agent.destination = atttackTarget.transform.position;
            yield return null;
        }

       // agent.isStopped = true;

        transform.LookAt(atttackTarget.transform);
        animator.SetTrigger("Attack");
    }

    public void Hit()
    {
        // Have our weapon attack the target
        if(atttackTarget!=null)stats.GetCurrentWeapon().ExecuteAttack(gameObject,atttackTarget);
    }

    public void OnDestruction(GameObject destroyer)
    {
        gameObject.SetActive(false);
    }
}
