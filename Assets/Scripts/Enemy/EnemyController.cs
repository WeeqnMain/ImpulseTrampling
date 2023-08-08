using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider hitCollider;

    [SerializeField] private GameObject hitEffect;

    [SerializeField] private float lifeTime;
    private float lifeTimer;

    public Action<EnemyController> Destroyed;

    private float moveSpeed;

    private bool isActive;
    private bool canReceiveHit;

    private void Awake()
    {
        lifeTimer = lifeTime;
    }

    public void Init(Vector3 rotateTo, int minSpeed, int maxSpeed)
    {
        transform.LookAt(rotateTo);
        moveSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private void OnEnable()
    {
        isActive = true;
        canReceiveHit = true;
        lifeTimer = lifeTime;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive == false) return;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0f)
            Deactivate();

        rigidbody.MovePosition(transform.position + moveSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActive == false) return;
        if (canReceiveHit == false) return;

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
            Destroyed?.Invoke(this);
            player.OnEnemyDestroy();
            Deactivate();
        }
        canReceiveHit = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
