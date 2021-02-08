using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF15Digger
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF15Setup()
    {
        SceneManager.LoadScene("Digger", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    // Dust should be created and destroyed when player digs down
    [UnityTest]
    public IEnumerator TestDiggerCreateDestroyDust()
    {
        yield return null;
        DiggerLevelManager dlm = GameObject.Find("LevelManager").GetComponent<DiggerLevelManager>() as DiggerLevelManager;
        dlm.lvlState = 2;

        yield return null;
        int numOfGameObjects = SceneManager.GetActiveScene().GetRootGameObjects().Length;
        dlm.player.DigDown();

        yield return null;
        Assert.AreEqual(numOfGameObjects+1, SceneManager.GetActiveScene().GetRootGameObjects().Length);

        yield return new WaitForSeconds(2f);
        Assert.AreEqual(numOfGameObjects, SceneManager.GetActiveScene().GetRootGameObjects().Length);
    }
}

public class TestRequirementF15Feeder
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF15Setup()
    {
        SceneManager.LoadScene("Feeder", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    //
    [UnityTest]
    [Ignore("Nothing to test")]
    public IEnumerator TestFeederReqF15()
    {
        yield return null;
        FeederLevelManager flm = GameObject.Find("LevelManager").GetComponent<FeederLevelManager>() as FeederLevelManager;
    }
}

public class TestRequirementF15Rockstar
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF15Setup()
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
    public IEnumerator TestRockstarReqF15()
    {
        yield return null;
        RockstarLevelManager rlm = GameObject.Find("LevelManager").GetComponent<RockstarLevelManager>() as RockstarLevelManager;
    }
}



