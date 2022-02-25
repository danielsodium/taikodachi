using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void goToMenu() {
        SceneManager.LoadScene (sceneName:"Menu");
    }
    public void quitGame() {
        Application.Quit();
    }
}
