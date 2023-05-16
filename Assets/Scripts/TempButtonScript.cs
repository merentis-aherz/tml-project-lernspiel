using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempButtonScript : MonoBehaviour
{
    public void OnClick(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnClickQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
