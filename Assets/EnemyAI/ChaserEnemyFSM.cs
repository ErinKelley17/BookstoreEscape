using UnityEngine;

public class ChaserEnemyFSM : BaseEnemyFSM
{
    public float damage = 10f;

    protected override void UpdateIdle(float dist)
    {
        if (dist <= detectionRange)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    protected override void UpdateChase(float dist)
    {
        if (dist <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (dist <= attackRange)
            {
                ChangeState(EnemyState.Attack);
            }
        }
        else
        {

            ChangeState(EnemyState.Idle);
        }
    }

    protected override void UpdateAttack(float dist)
    {

        agent.SetDestination(player.position);

        if (dist > attackRange)
        {
            ChangeState(EnemyState.Chase);
        }

    }

    protected override void OnStateEnter(EnemyState newState)
    {
        base.OnStateEnter(newState);
        if (newState == EnemyState.Idle)
        {
            agent.SetDestination(transform.position);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
