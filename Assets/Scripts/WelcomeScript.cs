using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScript : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the next scene in build settings, or use SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
