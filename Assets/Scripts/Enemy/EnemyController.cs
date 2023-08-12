using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Specs")]
    [SerializeField] private int minEnemyMoveSpeed;
    [SerializeField] private int maxEnemyMoveSpeed;
    private float moveSpeed;

    [SerializeField] private float lifeTime;
    private float lifeTimer;

    [SerializeField] private int minPointsForDestroy;
    [SerializeField] private int maxPointsForDestroy;
    private int pointsForDestroy;

    [Header("Physics")]
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider hitCollider;

    [Header("Effects")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AudioClip[] deathSounds;

    public Action<int> Destroyed;

    public void SetRotation(Vector3 rotateTo)
    {
        transform.LookAt(rotateTo);
    }

    private void Awake()
    {
        lifeTimer = lifeTime;
    }

    private void OnEnable()
    {
        lifeTimer = lifeTime;
        moveSpeed = UnityEngine.Random.Range(minEnemyMoveSpeed, maxEnemyMoveSpeed + 1);
        pointsForDestroy = UnityEngine.Random.Range(minPointsForDestroy, maxPointsForDestroy + 1);
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0f)
            Deactivate();

        rigidbody.MovePosition(transform.position + moveSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        }

        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
