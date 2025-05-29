using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button playButton;
    public Button howToButton;
    public Button quitButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;

    void Start()
    {
        //Button Bindings
        if (quitButton) quitButton.onClick.AddListener(QuitGame);
        if (playButton) playButton.onClick.AddListener(() => SceneManager.LoadScene("MainLevel"));
        if (howToButton) howToButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));
        if (backButton) backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));


        //inital menu states
        if (mainMenu)
            mainMenu.SetActive(true);
        if (settingsMenu)
            settingsMenu.SetActive(false);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate)
            menuToActivate.SetActive(true);

        if (menuToDeactivate)
            menuToDeactivate.SetActive(false);
    }
}