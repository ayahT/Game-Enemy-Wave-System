using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float attackRange = 2f;

    [Header("Attack")]
    [SerializeField] float attackCooldown = 1f;
    private float attackTimer = 0f;

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody>();
        attackTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            // Use physics-aware movement
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 newPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
        else
        {
            // Attack logic
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer >= attackCooldown)
            {
                AttackPlayer();
                attackTimer = 0f;
            }
        }
    }

    private void AttackPlayer()
    {
        Debug.Log($"{name} attacks the player!");
    }

    public void Die()
    {
        WaveManager.Instance.OnEnemyKilled(gameObject);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
         {
             Die();
         }
    }
}
