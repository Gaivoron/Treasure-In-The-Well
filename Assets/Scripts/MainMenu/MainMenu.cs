using UnityEngine;
using UnityEngine.SceneManagement;

//TODO - move to a namespace.
public class MainMenu : MonoBehaviour
{
   public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
