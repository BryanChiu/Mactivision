using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // GUI Objects
    public Button StartButton;
    public InputField PlayerInput;
    public Text GameList;

    // Developer GUI Objects
    public Button GenerateButton;
    public Dropdown ConfigDropdown;
    public Text ConfigDropdownSelected;

    // We can only start the battery if a configuration is loaded.
    private bool ConfigIsLoaded;

    // Start is called before the first frame update
    void Start()
    {
        ConfigIsLoaded = false;

        StartButton.interactable = false;
        StartButton.onClick.AddListener(StartButtonClicked);

        PlayerInput.onValueChanged.AddListener(delegate {PlayerInputOnChange ();});

        GenerateButton.onClick.AddListener(GenerateButtonClicked); 

        ConfigDropdown.onValueChanged.AddListener(delegate {ConfigDropdownChange (); });

        // Creates a dropdown list of all the available battery configuration files. This is used for testing and debugging configurations. TODO: Generate a list names programmatically.
        ConfigDropdown.ClearOptions();
        var options = new List<string>();
        options.Add("SELECT BATTERY");
        options.Add("Demo");
        options.Add("LongDemo");
        options.Add("GeneratedTemplate");
        ConfigDropdown.AddOptions(options);

        Debug.Log("Start Screen Started.");
    }

    // On change make sure it's an actual configuration file and not a heading. TODO: Better error handling.
    void ConfigDropdownChange()
    {
        ConfigIsLoaded = false;
        string text = ConfigDropdownSelected.text;
        // If dropdown selection is not a heading.
        if (!text.Equals("SELECT BATTERY"))
        {
            ConfigIsLoaded = true;
            Battery.Instance.LoadBattery(text);
            ListGames();
        }
    }

    // Generate a configuration template if button is clicked.
    void GenerateButtonClicked()
    {
        Battery.Instance.WriteExampleConfig();
    }

    // List games titles on the start screen. Useful for debugging. Not sure if it will remain for final version.
    void ListGames()
    {
        var games = Battery.Instance.GetGameList();
        GameList.text = "Battery Game List\n";
        foreach (string game in games)
        {
            GameList.text = GameList.text + "\t -" + game + "\n";  
        }
    }

    void PlayerInputOnChange()
    {
        // Make sure that the player puts a name in before they can hit the start Battery button.
        if (string.IsNullOrEmpty(PlayerInput.text))
        {
            StartButton.interactable = false;  
        }
        else
        {
            StartButton.interactable = true;
        }
        Debug.Log("Player Input Field Changed.");
    }

    void StartButtonClicked()
    {
        // Start Battery and record playername for configuration output log.
        Battery.Instance.StartBattery();
        Battery.Instance.SetPlayerName(PlayerInput.text);

        Debug.Log("Start Button Clicked.");
        if (ConfigIsLoaded)
        {
            // Start scene index -1 so next scene should be 0, the first game in the list under the configuration. TODO: Better error handling.
            Battery.Instance.LoadNextScene();
        }
    }
}
