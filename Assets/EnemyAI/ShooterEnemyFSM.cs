using UnityEngine;

public class ShooterEnemyFSM : BaseEnemyFSM
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1;
    public float shootPower = 1500;

    private float fireCooldown;


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
            agent.stoppingDistance = attackRange;
            agent.SetDestination(player.position);

            if (dist <= attackRange + 0.05)
            {
                ChangeState(EnemyState.Attack);
            }
        }
        else
        {
            ChangeState(EnemyState.Idle);

        }
    }

    protected override void OnStateEnter(EnemyState state)
    {

        base.OnStateEnter(state);
        if (state == EnemyState.Idle)
        {
            agent.SetDestination(transform.position);
        }

        if (state == EnemyState.Attack)
        {
            agent.ResetPath();
        }
    }

    protected override void UpdateAttack(float dist)
    {
        transform.LookAt(player);
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        if (dist > attackRange)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    void Shoot()
    {
        GameObject instance = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);

        firePoint.LookAt(player.position + new Vector3(0, 0.75f, 0));
        Vector3 fwd = firePoint.TransformDirection(Vector3.forward);

        instance.GetComponent<Rigidbody>().AddForce(fwd * shootPower);
    }
}
