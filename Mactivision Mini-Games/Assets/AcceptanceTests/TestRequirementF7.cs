using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF7
{

    [OneTimeSetUp]
    public void SetupBattery()
    {
        SceneManager.LoadScene("Battery Start", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    [UnityTest]
    public IEnumerator TestBatteryStartToEnd()
    {
        /*
        var file = new FileHandler();
        var dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        var button = GameObject.Find("Start Button").GetComponent<Button>();

        dropdown.value = dropdown.options.FindIndex(option => option.text == "GeneratedTemplate");
    
        //file.DeleteDirectory(Battery.Instance.GetOutputPath(), true);

        button.onClick.Invoke();
        
        //Assert.IsTrue(file.DirectoryExists(Battery.Instance.GetOutputPath()));

        SceneManager.LoadScene("Battery End", LoadSceneMode.Single);
        
        yield return null;

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Battery End");

        // Was the config file written?
        //Assert.IsTrue(file.ConfigFileExists(Battery.Instance.GetOutputPath()));

        // clean up
        //file.DeleteDirectory(Battery.Instance.GetOutputPath(), true);
        */
        yield return null;
        
    }
}
