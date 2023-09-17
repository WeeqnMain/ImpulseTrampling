using System.Collections;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    [Header("Specification")]
    [SerializeField] private float rotationTimeInterval;
    [SerializeField] private float minRotationAngle; 
    [SerializeField] private float maxRotationAngle;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float explosionRange;
    [SerializeField] private LayerMask exlodeLayer;

    private Quaternion rotateDirection;

    private void OnEnable()
    {
        StartCoroutine(RotationChangeRoutine());
        base.EnableInit();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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

    private IEnumerator RotationChangeRoutine()
    {
        var timer = rotationTimeInterval;
        while (timer > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, rotateSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
            yield return null;
        }

        rotateDirection = GetNewRotation();
        StartCoroutine(RotationChangeRoutine());
    }

    private Quaternion GetNewRotation()
    {
        var newDirection = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(minRotationAngle, maxRotationAngle + 1), transform.rotation.z);
        Debug.Log(newDirection);
        return newDirection;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange, exlodeLayer);

        foreach (var collider in colliders)
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy != this)
                enemy.Destroy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRange);
    }
}
