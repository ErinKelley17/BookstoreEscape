using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 5f;
    public float lifetime = 3f;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        //Damage the enemy
        BaseEnemyFSM enemy = other.GetComponent<BaseEnemyFSM>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);

            }
            Destroy(gameObject);
        }
    }
}
