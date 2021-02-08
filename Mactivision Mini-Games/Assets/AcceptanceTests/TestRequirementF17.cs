using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestRequirementF17Digger
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF17Setup()
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
    public IEnumerator TestDiggerPlayerFall()
    {
        yield return null;
        DiggerLevelManager dlm = GameObject.Find("LevelManager").GetComponent<DiggerLevelManager>() as DiggerLevelManager;
        dlm.lvlState = 2;

        yield return new WaitForSeconds(0.5f);
        Transform player = GameObject.Find("Player").transform;
        Vector3 pos = player.position;
        player.Translate(Vector3.up);

        yield return new WaitForSeconds(0.5f);
        Assert.AreEqual(pos.y, player.position.y, 0.0015f);
    }
}

public class TestRequirementF17Feeder
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF17Setup()
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
    public IEnumerator TestFeederFoodFall()
    {
        yield return null;
        FeederLevelManager flm = GameObject.Find("LevelManager").GetComponent<FeederLevelManager>() as FeederLevelManager;
        flm.lvlState = 2;

        yield return new WaitForSeconds(1.8f);
        Vector3 pos = Vector3.zero;
        GameObject food = new GameObject();
        foreach (GameObject obj in flm.dispenser.allFoods) {
            if (obj.name==flm.dispenser.currentFood) {
                pos = obj.transform.position;
                food = obj;
                break;
            }
        }

        yield return new WaitForSeconds(0.75f);
        Assert.IsTrue(pos.y-3 > food.transform.position.y);
    }
}

public class TestRequirementF17Rockstar
{
    // A Test behaves as an ordinary method
    [OneTimeSetUp]
    public void TestRequirementF17Setup()
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
    public IEnumerator TestRockstarReq17()
    {
        yield return null;
        RockstarLevelManager rlm = GameObject.Find("LevelManager").GetComponent<RockstarLevelManager>() as RockstarLevelManager;
    }
}



