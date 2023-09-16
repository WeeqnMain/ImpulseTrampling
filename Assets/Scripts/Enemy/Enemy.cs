using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Specs")]
    [SerializeField] private int minEnemyMoveSpeed;
    [SerializeField] private int maxEnemyMoveSpeed;
    private float moveSpeed;

    [SerializeField] private int minPointsForDestroy;
    [SerializeField] private int maxPointsForDestroy;
    private int pointsForDestroy;

    [Header("Physics")]
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider hitCollider;

    [Header("Effects")]
    [SerializeField] private GameObject hitEffect;

    public Action<int> Destroyed;

    public void SetRotation(Vector3 rotateTo)
    {
        transform.LookAt(rotateTo);
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + moveSpeed * Time.fixedDeltaTime * transform.forward);
    }

    protected void EnableInit()
    {
        moveSpeed = UnityEngine.Random.Range(minEnemyMoveSpeed, maxEnemyMoveSpeed + 1);
        pointsForDestroy = UnityEngine.Random.Range(minPointsForDestroy, maxPointsForDestroy + 1);
    }

    protected void HandleCollision(Collision collision)
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
            Destroyed?.Invoke(pointsForDestroy);
            player.OnEnemyDestroy();
            AudioManager.instance.PlayEffect("EnemyDeath");
        }

        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
