using UnityEngine;

public class ExplodingEnemy : Enemy
{
    [Header("Specification")]
    [SerializeField] private float explosionRange;

    private void OnEnable()
    {
        base.EnableInit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeathWall"))
        {
            Deactivate();
            return;
        }

        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        var contact = collision.contacts[0];
        var collider = contact.thisCollider;

        Instantiate(hitEffect, contact.point, Quaternion.identity, null);

        if (collider == bodyCollider)
        {
            player.RecieveDamage();
        }

        else if (collider == hitCollider)
        {
            player.OnEnemyDestroy();
            Destroyed?.Invoke(pointsForDestroy);
            AudioManager.instance.PlayEffect("EnemyDeath");
            Explode();
        }

        Deactivate();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

        foreach (var collider in colliders)
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Destroy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRange);
    }
}
