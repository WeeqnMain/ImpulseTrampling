using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private float moveSpeed;
    private Vector3 direction;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider hitCollider;

    private void Awake()
    {
        direction = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
        transform.Rotate(direction);

        moveSpeed = UnityEngine.Random.Range(8f, 11f);
        StartCoroutine(LifeTimer());
    }

    private void Update()
    {
        rigidbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        var collider = collision.contacts[0].thisCollider;

        if (collider == bodyCollider)
        {
            player.RecieveDamage();
        }

        if (collider == hitCollider)
        {
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
