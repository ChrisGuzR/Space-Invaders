using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelScript : MonoBehaviour
{
    public void RestartGame()
    {
        // Loads the next scene in build settings, or use SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("__Scene_0");
    }
}
