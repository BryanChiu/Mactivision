using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF12
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
    
        Assert.AreEqual(Battery.Instance.GetStartTime(), null);

        //file.DeleteDirectory(Battery.Instance.GetOutputPath(), true);

        button.onClick.Invoke();
        
        Assert.AreNotEqual(Battery.Instance.GetStartTime(), null);
        
        yield return null;
        
        //Assert.IsTrue(file.DirectoryExists(Battery.Instance.GetOutputPath()));

        yield return null;

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Digger");

        yield return null;
    
        Battery.Instance.LoadNextScene();
        
        yield return null;
        
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Feeder");
        
        yield return null;
        
        Battery.Instance.LoadNextScene();
        
        yield return null;
        
        Assert.AreEqual(SceneManager.GetActiveScene().name, "RockStar");
        
        yield return null;

        Assert.AreEqual(Battery.Instance.GetEndTime(), null);
        
        // This should load the Battery End
        Battery.Instance.LoadNextScene();
        
        yield return null;

        Assert.AreEqual(SceneManager.GetActiveScene().name, "Battery End");
        
        yield return null;
       
        Assert.AreNotEqual(Battery.Instance.GetEndTime(), null);

        // clean up
        //file.DeleteDirectory(Battery.Instance.GetOutputPath(), true);

        */
        yield return null;
        

    }
    
}
