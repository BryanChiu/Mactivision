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

    // Can we start the battery?
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

        ConfigDropdown.ClearOptions();
        var options = new List<string>();
        options.Add("SELECT BATTERY");
        options.Add("Demo");
        options.Add("LongDemo");
        options.Add("Example");
        ConfigDropdown.AddOptions(options);

        Debug.Log("Start Screen Started.");
    }

    void ConfigDropdownChange()
    {
        ConfigIsLoaded = false;
        string text = ConfigDropdownSelected.text;
        if (!text.Equals("SELECT BATTERY"))
        {
            ConfigIsLoaded = true;
            Battery.Instance.LoadBattery(text);
            ListGames();
        }
    }

    void GenerateButtonClicked()
    {
        Battery.Instance.WriteExampleConfig();
    }

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
        Battery.Instance.StartBattery();
        Battery.Instance.SetPlayerName(PlayerInput.text);

        Debug.Log("Start Button Clicked.");
        if (ConfigIsLoaded)
        {
            Battery.Instance.LoadNextScene();
        }
    }
}
