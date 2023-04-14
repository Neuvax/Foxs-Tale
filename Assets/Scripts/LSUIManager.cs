 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelInfoPanel;

    public TMP_Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if(shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
        
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;

        gemsFound.text = "Collected: " + levelInfo.gemsCollected;
        gemsTarget.text = "Available: " + levelInfo.totalGems;

        timeTarget.text = "Target: " + levelInfo.targetTime + "s";

        if(levelInfo.bestTime == 0)
        {
            bestTime.text = "Best: ---s";
        } else
        {
            bestTime.text = "Best: " + levelInfo.bestTime.ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }

    public void ShowInfoLock()
    {
        levelName.text = "???";

        gemsTarget.text = "Available: ???";

        timeTarget.text = "Target: ???";

        levelInfoPanel.SetActive(true);
    }
}
