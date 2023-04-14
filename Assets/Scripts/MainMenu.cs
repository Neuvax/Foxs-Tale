using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string startScene, continueScene;

    public GameObject continueButton;

    private void Start() 
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        BlackScreen.instance.FadeToBlack();
        SceneManager.LoadScene(startScene);

        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        BlackScreen.instance.FadeToBlack();
        SceneManager.LoadScene(continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
