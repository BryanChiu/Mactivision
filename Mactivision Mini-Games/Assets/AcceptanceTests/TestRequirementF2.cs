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
        Assert.NotNull(levelManager);
        Assert.NotNull(diggerLevelManager);

        string[] filesBefore = Directory.GetFiles("./Logs/", "digger_*.json").Select(filename => Path.GetFileName(filename)).ToArray();

        diggerLevelManager.chest.opened = true;
        diggerLevelManager.lvlState = 2;

        yield return null;
        string[] filesAfter = Directory.GetFiles("./Logs/", "digger_*.json").Select(filename => Path.GetFileName(filename)).ToArray();
        Assert.AreEqual(1, filesAfter.Count() - filesBefore.Count());

        string newFile = "";
        foreach (string file in filesAfter) {
            if (!filesBefore.Contains(file)) {
                newFile = file;
            }
        }
        Assert.AreNotEqual("", newFile);

        // read file and test if start and end times are outputted
        string json = File.ReadAllText("./Logs/" + newFile);
        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json);
        Assert.IsNotNull(jsonObject);
        Assert.IsNotNull(jsonObject["startTime"].ToString());
        Assert.IsNotNull(jsonObject["endTime"].ToString());
        Assert.AreNotEqual("", jsonObject["startTime"].ToString());
        Assert.AreNotEqual("", jsonObject["endTime"].ToString());

        File.Delete("./Logs/" + newFile);
    }
}
