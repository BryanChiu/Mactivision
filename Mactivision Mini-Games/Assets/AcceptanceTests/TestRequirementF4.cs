using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF4Digger
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF4Setup()
    {
        SceneManager.LoadScene("Digger", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // outroText should be enabled after the game ends, when player is able to continue to next scene
    [UnityTest]
    public IEnumerator TestDiggerShowOutro()
    {
        yield return null;
        DiggerLevelManager dlm = GameObject.Find("LevelManager").GetComponent<DiggerLevelManager>() as DiggerLevelManager;

        dlm.EndLevel(0f);

        yield return null;
        Assert.IsTrue(dlm.outroText.enabled);
    }
}

public class TestRequirementF4Feeder
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF4Setup()
    {
        SceneManager.LoadScene("Feeder", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // outroText should be enabled after the game ends, when player is able to continue to next scene
    [UnityTest]
    public IEnumerator TestFeederShowOutro()
    {
        yield return null;
        FeederLevelManager flm = GameObject.Find("LevelManager").GetComponent<FeederLevelManager>() as FeederLevelManager;

        flm.EndLevel(0f);

        yield return null;
        Assert.IsTrue(flm.outroText.enabled);
    }
}

public class TestRequirementF4Rockstar
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF4Setup()
    {
        SceneManager.LoadScene("Rockstar", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // outroText should be enabled after the game ends, when player is able to continue to next scene
    [UnityTest]
    [Ignore("Rockstar game not developed yet")]
    public IEnumerator TestRockstarShowOutro()
    {
        yield return null;
        RockstarLevelManager rlm = GameObject.Find("LevelManager").GetComponent<RockstarLevelManager>() as RockstarLevelManager;

        rlm.EndLevel(0f);

        yield return null;
        Assert.IsTrue(rlm.outroText.enabled);
    }
}



