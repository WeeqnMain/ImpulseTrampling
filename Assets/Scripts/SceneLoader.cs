using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
