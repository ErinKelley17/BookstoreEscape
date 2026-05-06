using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemyFSM : MonoBehaviour
{

    [Header("Ranges")]
    public float detectionRange = 15f;
    public float attackRange = 2f;

    [Header("Health")]
    public float maxHealth = 50f;
    protected float currentHealth;

    [Header("Knock Back")]
    public float knockBackAmount = 2f;

    protected Transform player;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected EnemyState currentState;

    protected Animator animator;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        ChangeState(EnemyState.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || currentState == EnemyState.Dead)
        {
            return;
        }

        float dist = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle(dist);
                break;
            case EnemyState.Chase:
                UpdateChase(dist);
                break;
            case EnemyState.Attack:
                UpdateAttack(dist);
                break;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        if (currentState == EnemyState.Dead)
        {
            return;
        }
        currentHealth -= amount;

        if (animator != null)
        {
            animator.SetTrigger("TakeDamage");
        }

        if (player != null)
        {
            agent.velocity = (transform.position - player.position).normalized
                                        * knockBackAmount;
        }

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    protected virtual void Die()
    {
        ChangeState(EnemyState.Dead);
        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.5f);
        }
        else
        {
            Destroy(gameObject, 0.5f);
        }

    }

    protected void ChangeState(EnemyState newState)
    {
        currentState = newState;
        OnStateEnter(newState);
    }

    protected virtual void OnStateEnter(EnemyState newState)
    {
        Debug.Log(newState);
        if (animator == null || newState == EnemyState.Dead)
        {
            return;
        }
        switch (newState)
        {
            case EnemyState.Idle:
                animator.SetBool("Walking", false);
                animator.SetBool("Attack", false);
                break;
            case EnemyState.Chase:
                animator.SetBool("Walking", true);
                animator.SetBool("Attack", false);
                break;
            case EnemyState.Attack:
                animator.SetBool("Walking", false);
                animator.SetBool("Attack", true);
                break;
        }
    }

    protected abstract void UpdateIdle(float dist);
    protected abstract void UpdateChase(float dist);
    protected abstract void UpdateAttack(float dist);
}
