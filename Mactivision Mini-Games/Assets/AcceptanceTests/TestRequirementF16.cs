using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF16Digger
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF16Setup()
    {
        SceneManager.LoadScene("Digger", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    //
    [UnityTest]
    [Ignore("Nothing to test")]
    public IEnumerator TestDiggerReq16()
    {
        yield return null;
        DiggerLevelManager dlm = GameObject.Find("LevelManager").GetComponent<DiggerLevelManager>() as DiggerLevelManager;
    }
}

public class TestRequirementF16Feeder
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF16Setup()
    {
        SceneManager.LoadScene("Feeder", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // Pipe should animate when dispensing food
    [UnityTest]
    public IEnumerator TestFeederAnimatePipe()
    {
        yield return null;
        FeederLevelManager flm = GameObject.Find("LevelManager").GetComponent<FeederLevelManager>() as FeederLevelManager;
        Assert.IsFalse(flm.dispenser.pipe.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.pipe_dispense"));

        flm.lvlState = 2;

        yield return new WaitForSeconds(1.8f);
        Assert.IsTrue(flm.dispenser.pipe.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.pipe_dispense"));
    }
}

public class TestRequirementF16Rockstar
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF16Setup()
    {
        SceneManager.LoadScene("Rockstar", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    //
    [UnityTest]
    [Ignore("Rockstar game not developed yet")]
    public IEnumerator TestRockstarReq16()
    {
        yield return null;
        RockstarLevelManager rlm = GameObject.Find("LevelManager").GetComponent<RockstarLevelManager>() as RockstarLevelManager;
    }
}



