using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*
 * This test requirement F-2
 * "Record start and end times for each game."
 */
public class TestRequirementF2
{
    [OneTimeSetUp]
    public void SetupScene()
    {
        SceneManager.LoadScene("Digger", LoadSceneMode.Single);
    }

    [OneTimeTearDown]
    public void TearDownBattery()
    {
        Battery.Instance.Reset();
    }

    [UnityTest]
    public IEnumerator TestDiggerButtonPresses()
    {
        yield return null;
        var levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        DiggerLevelManager diggerLevelManager = levelManager.GetComponent<DiggerLevelManager>();
        diggerLevelManager.Client = new ClientServerMock();
        Assert.NotNull(levelManager);
        Assert.NotNull(diggerLevelManager);

        diggerLevelManager.StartGame();
        diggerLevelManager.EndGame();

        string filename = ((ClientServerMock) diggerLevelManager.Client).filename;
        string data = ((ClientServerMock) diggerLevelManager.Client).data;

        // read file and test if start and end times are outputted
        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(data);
        Assert.IsNotNull(jsonObject);
        Assert.IsNotNull(jsonObject["startTime"].ToString());
        Assert.IsNotNull(jsonObject["endTime"].ToString());
        Assert.AreNotEqual("", jsonObject["startTime"].ToString());
        Assert.AreNotEqual("", jsonObject["endTime"].ToString());
    }
}
