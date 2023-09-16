using UnityEngine;

public class StandartEnemy : Enemy
{
    private void OnEnable()
    {
        base.EnableInit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.HandleCollision(collision);
    }
}
