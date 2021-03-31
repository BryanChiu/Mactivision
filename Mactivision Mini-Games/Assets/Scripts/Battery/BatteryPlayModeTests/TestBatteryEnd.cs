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

public class TestBatteryEnd
{
    private FileHandler File;

    [OneTimeSetUp]
    public void SetupBattery()
    {
        /*
        File = new FileHandler();
        SceneManager.LoadScene("Battery End", LoadSceneMode.Single);
        */
    }

    [UnityTest]
    public IEnumerator TestBatteryEndIsLoaded()
    {
        Assert.IsTrue(SceneManager.GetSceneByName("Battery End").isLoaded);
        yield return null;
    }

}
