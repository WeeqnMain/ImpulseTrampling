using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private DefeatScreen defeatScreen;

    public void PlayerReceivedDamage(int value)
    {
        defeatScreen.Show();
    }
}
