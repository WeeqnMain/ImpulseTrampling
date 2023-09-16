using System.Collections;
using UnityEngine;

public class FastEnemy : Enemy
{
    [Header("Specification")]
    [SerializeField] private float rotateSpeed;

    private bool canReflect;

    private void OnEnable()
    {
        StartCoroutine(InvulnerableCoroutine(0.5f));
        base.EnableInit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.HandleCollision(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canReflect && other.gameObject.CompareTag("Wall"))
        {
            canReflect = false;
            var rotateDirection = Vector3.Reflect(transform.forward, other.transform.right);
            transform.rotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            canReflect = true;
        }
    }

    private IEnumerator InvulnerableCoroutine(float secondsToWait)
    {
        canReflect = false;
        yield return new WaitForSeconds(secondsToWait);
        canReflect = true;
    }
}
