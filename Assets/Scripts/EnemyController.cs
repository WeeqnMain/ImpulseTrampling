using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider hitCollider;

    public Action<EnemyController> Destroyed;

    private float moveSpeed;
    private bool canReceiveHit = true;

    public void Init(Vector3 rotateTo, int minSpeed, int maxSpeed)
    {
        transform.LookAt(rotateTo);
        moveSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        StartCoroutine(LifeTimer());
    }

    private void Update()
    {
        rigidbody.MovePosition(transform.position + moveSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;
        if (canReceiveHit == false) return;

        var collider = collision.contacts[0].thisCollider;

        if (collider == bodyCollider)
        {
            player.RecieveDamage();
            canReceiveHit = false;
        }

        if (collider == hitCollider)
        {
            Destroyed?.Invoke(this);
            player.OnEnemyDestroy();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    //fix when enemy pool is created
    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
