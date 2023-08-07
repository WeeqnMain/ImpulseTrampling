using UnityEngine;

public class EntryPoint : MonoBehaviour
{
   private void Awake()
   {
       SceneLoader.ChangeScene("GameScene");
   }
}
