using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    // Create states for the menu
    public enum MenuState { Main, Customize, Game, GameOver, Pause, Settings, Style, Tutorial, Back, Credits, Controls };
    public MenuState menuState = MenuState.Main;

    // Cache StyleButton
    private StyleButton styleButton;
    // Cache BackButton
    private BackButton backButton;

    // Cache PlayButton
    public PlayButton playButton;

    // Cache CreditsButton
    public CreditsButton creditsButton;

    // Cache ControlsButton
    public ControlsButton controlsButton;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cache StyleButton
        styleButton = GetComponentInChildren<StyleButton>();
        // Cache BackButton
        backButton = GetComponentInChildren<BackButton>();
        // Cache PlayButton
        playButton = GetComponentInChildren<PlayButton>();
        // Cache CreditsButton
        creditsButton = GetComponentInChildren<CreditsButton>();
        // Cache controls button
        controlsButton = GetComponentInChildren<ControlsButton>();
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
            creditsButton.creditsText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            creditsButton.GetComponent<Collider2D>().enabled = false;
            controlsButton.controlsText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            controlsButton.GetComponent<Collider2D>().enabled = false;
            backButton.MoveBackButton();
        }

        if(menuState == MenuState.Main)
        {
            playButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            playButton.GetComponent<Collider2D>().enabled = true;

            styleButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            styleButton.gameObject.GetComponent<Collider2D>().enabled = true;

            creditsButton.creditsNameText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            creditsButton.creditsText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            creditsButton.GetComponent<Collider2D>().enabled = true;

            controlsButton.controlsText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            controlsButton.GetComponent<Collider2D>().enabled = true;
            controlsButton.controls.SetActive(false);
        }

        if(menuState == MenuState.Credits)
        {
            backButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            backButton.gameObject.GetComponent<Collider2D>().enabled = true;
            playButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            playButton.gameObject.GetComponent<Collider2D>().enabled = false;

            styleButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            styleButton.gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if(menuState == MenuState.Controls)
        {
            // disable text
            controlsButton.controlsText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;

            backButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            backButton.gameObject.GetComponent<Collider2D>().enabled = true;
            playButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            playButton.gameObject.GetComponent<Collider2D>().enabled = false;

            styleButton.transform.parent.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
            styleButton.gameObject.GetComponent<Collider2D>().enabled = false;
        }

    }

    // Update menu state to the new state
    public void UpdateMenuState(MenuState newState)
    {
        menuState = newState;
    }
}
