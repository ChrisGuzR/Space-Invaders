using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public void RestartGameFromZero()
    {
        // Loads the next scene in build settings, or use SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("WelcomeScene");
    }
}
