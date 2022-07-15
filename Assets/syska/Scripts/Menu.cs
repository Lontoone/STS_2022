using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Leave()
    {
        /*
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        UnityEngine.Application.Quit();
        */
    }
}
