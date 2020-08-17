using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{
    public string ARStartSceneName = "ARStartScene";
    public void LoadARScene()
    {
        SceneManager.LoadScene(ARStartSceneName, LoadSceneMode.Single);
    }
}
