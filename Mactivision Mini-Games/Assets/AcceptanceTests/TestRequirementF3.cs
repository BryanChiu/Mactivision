using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF3Digger
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF3Setup()
    {
        SceneManager.LoadScene("Digger", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // introText should be enabled right at scene start
    [UnityTest]
    public IEnumerator TestDiggerShowInstructions()
    {
        yield return null;
        DiggerLevelManager dlm = GameObject.Find("LevelManager").GetComponent<DiggerLevelManager>() as DiggerLevelManager;
        Assert.IsTrue(dlm.introText.enabled);
    }
}

public class TestRequirementF3Feeder
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF3Setup()
    {
        SceneManager.LoadScene("Feeder", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // introText should be enabled right at scene start
    [UnityTest]
    public IEnumerator TestFeederShowInstructions()
    {
        yield return null;
        FeederLevelManager flm = GameObject.Find("LevelManager").GetComponent<FeederLevelManager>() as FeederLevelManager;
        Assert.IsTrue(flm.introText.enabled);
    }
}

public class TestRequirementF3Rockstar
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF3Setup()
    {
        SceneManager.LoadScene("Rockstar", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // introText should be enabled right at scene start
    [UnityTest]
    [Ignore("Rockstar game not developed yet")]
    public IEnumerator TestRockstarShowInstructions()
    {
        yield return null;
        RockstarLevelManager rlm = GameObject.Find("LevelManager").GetComponent<RockstarLevelManager>() as RockstarLevelManager;
        Assert.IsTrue(rlm.introText.enabled);
    }
}

