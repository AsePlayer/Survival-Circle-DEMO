using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    // Create states for the menu
    public enum MenuState { Main, Customize, Game, GameOver, Pause, Settings, Style, Tutorial, Back };
    public MenuState menuState = MenuState.Main;

    // Cache StyleButton
    private StyleButton styleButton;
    // Cache BackButton
    private BackButton backButton;

    // Cache PlayButton
    public PlayButton playButton;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Cache StyleButton
        styleButton = GetComponentInChildren<StyleButton>();
        // Cache BackButton
        backButton = GetComponentInChildren<BackButton>();
        // Cache PlayButton
        playButton = GetComponentInChildren<PlayButton>();
    }

    // Update is called once per frame
    void Update()
    {

        // If menu is in back state, become main state
        if(menuState == MenuState.Back)
        {
            menuState = MenuState.Main;
        }

        // disable back button if player is in main state
        if(menuState != MenuState.Main)
        {
            backButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            playButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            backButton.gameObject.GetComponent<Collider2D>().enabled = true;
            backButton.MoveBackButton();
        }

        if(menuState == MenuState.Main)
        {
            playButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        }

    }

    // Update menu state to the new state
    public void UpdateMenuState(MenuState newState)
    {
        menuState = newState;
    }
}
