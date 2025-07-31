using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooter : MonoBehaviour
{
    [Header("Bullet Settings")]
    public Transform firePoint;
    public float fireRate = 0.2f;

    private float fireTimer;

    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    // Called by PlayerInput
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPooling.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }
}
