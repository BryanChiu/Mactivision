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
        /*
        File = new FileHandler();
        SceneManager.LoadScene("Battery Start", LoadSceneMode.Single);
        ConfigHandler Config = new ConfigHandler();

        // If tests error the file can get out of sync so always create it.
        File.WriteGenerated(Config.Generate());
        */
    }

    [UnityTest]
    public IEnumerator TestStartButton()
    {
        var button = GameObject.Find("Start Button").GetComponent<Button>();
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        
        Assert.AreEqual(dropdown.captionText.text, "SELECT BATTERY");
        Assert.IsFalse(button.interactable); 

        // clean up
        //File.DeleteDirectory(Battery.Instance.GetOutputPath(), true);

        yield return null;
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
    public IEnumerator TestSelectUnparsableConfig()
    {
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var output = GameObject.Find("Developer Output").GetComponent<Text>();
        dropdown.value = dropdown.options.FindIndex(option => option.text == "BadConfig");

        Assert.IsTrue(output.text.Contains("Error"));
        Assert.IsTrue(output.text.Contains("parse"));
        
        // reset value back to last for other tests.
        dropdown.value = 0;

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestSelectBadSceneConfig()
    {
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var output = GameObject.Find("Developer Output").GetComponent<Text>();
        dropdown.value = dropdown.options.FindIndex(option => option.text == "BadScenes");

        Assert.IsTrue(output.text.Contains("Error"));
        Assert.IsTrue(output.text.Contains("scene"));
        
        // reset value back to last for other tests.
        dropdown.value = 0;

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestSelectEmptyConfig()
    {
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var output = GameObject.Find("Developer Output").GetComponent<Text>();
        dropdown.value = dropdown.options.FindIndex(option => option.text == "Empty");

        Assert.IsTrue(output.text.Contains("Error"));
        Assert.IsTrue(output.text.Contains("empty"));
        
        // reset value back to last for other tests.
        dropdown.value = 0;

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestDropDownOptions()
    {
        /*
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        //string[] filenames = File.ListConfigFiles();

        bool correct_options = true;

        foreach (Dropdown.OptionData option in dropdown.options)
        {
            if (Array.IndexOf(filenames, option) == 0)
            {
                correct_options = false;
            }
        }
        
        Assert.True(correct_options);
        */
        yield return null;
        
    }

    [UnityTest]
    public IEnumerator TestStartBattery()
    {
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var button = GameObject.Find("Start Button").GetComponent<Button>();
        dropdown.value = dropdown.options.FindIndex(option => option.text == "GeneratedTemplate");

        //File.DeleteDirectory(Battery.Instance.GetOutputPath(), true);
        
        //Assert.IsFalse(File.DirectoryExists(Battery.Instance.GetOutputPath()));
        Assert.AreEqual(Battery.Instance.GetStartTime(), null);
        
        button.onClick.Invoke();
        
        //Assert.IsTrue(File.DirectoryExists(Battery.Instance.GetOutputPath()));
        Assert.AreNotEqual(Battery.Instance.GetStartTime(), null);

        // reset value back to last for other tests.
        dropdown.value = 0;

        // return to start battery scene.
        
        SceneManager.LoadScene("Battery Start", LoadSceneMode.Single);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestDropDownSelect()
    {
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var button = GameObject.Find("Start Button").GetComponent<Button>();
        var text = GameObject.Find("Developer Output").GetComponent<Text>();
      
        // default option
        Assert.AreEqual(dropdown.captionText.text, "SELECT BATTERY");
        Assert.IsFalse(button.interactable); 
        Assert.AreEqual(text.text, "Developer Output");
        
        // select new option
        // changing the value triggers onchangevalue event in startscreen
        dropdown.value = dropdown.options.FindIndex(option => option.text == "GeneratedTemplate");

        Assert.AreEqual(dropdown.captionText.text, "GeneratedTemplate");
        Assert.IsTrue(button.interactable); 
        Assert.AreNotEqual(text.text, "Developer Output");

        // reset value back to last for other tests.
        dropdown.value = 0;

        // default option
        Assert.AreEqual(dropdown.captionText.text, "SELECT BATTERY");
        Assert.IsFalse(button.interactable); 
        Assert.AreEqual(text.text, "Developer Output");

        yield return null;
    }

}
