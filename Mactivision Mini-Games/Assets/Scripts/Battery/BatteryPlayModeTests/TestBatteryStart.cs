using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestBatteryStart
{
    private FileHandler File;

    [OneTimeSetUp]
    public void SetupBattery()
    {
        Dictionary<string,GameConfig> GameList = new Dictionary<string,GameConfig>
        {
            {"Digger", new DiggerConfig()},
            {"Feeder", new FeederConfig()},
            {"Rockstar", new RockstarConfig()}
        };   
        File = new FileHandler("./Assets/Configs/", "GeneratedTemplate.json", "GeneratedTemplate.meta" );
        SceneManager.LoadScene("Battery Start", LoadSceneMode.Single);
        
        ConfigHandler Config = new ConfigHandler(GameList);

        // If tests error the file can get out of sync so always create it.
        File.WriteGenerated(Config.Generate());
    }

    [UnityTest]
    public IEnumerator TestBatteryStartIsLoaded()
    {
        Assert.IsTrue(SceneManager.GetSceneByName("Battery Start").isLoaded);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestGenerateButton()
    {
        File.DeleteGeneratedConfig();
        Assert.IsFalse(File.GeneratedConfigExists());

        var button = GameObject.Find("Generate Button").GetComponent<Button>();
        button.onClick.Invoke();
        
        Assert.IsTrue(File.GeneratedConfigExists());

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestStartBattery()
    {
        var button = GameObject.Find("Start Button").GetComponent<Button>();

        Assert.AreEqual(Battery.Instance.GetStartTime(), null);
        
        button.onClick.Invoke();
        
        Assert.AreNotEqual(Battery.Instance.GetStartTime(), null);

        SceneManager.LoadScene("Battery Start", LoadSceneMode.Single);
        yield return null;
    }

}
