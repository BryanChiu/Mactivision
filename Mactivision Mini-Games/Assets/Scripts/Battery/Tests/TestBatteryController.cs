using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBatteryController
{

    private string MockConfig(string file)
    {
        return System.IO.File.ReadAllText("./Assets/Scripts/Battery/Tests/Mocks/" + file);
    }

    private BatteryController Controller;

    [SetUp]
    public void AutoGenerateAndLoad()
    {
        Controller = new BatteryController();
        Controller.LoadConfig(Controller.GenerateConfig());
    }

    [Test]
    public void TestFolderTimeStamp()
    {
        string actual = System.DateTime.Now.ToString("yyyyMMddhhmm"); 
        string expected = Controller.FolderTimeStamp();
        Assert.AreEqual(expected, actual);  
    }

    [Test]
    public void TestGenerateAndLoad()
    {
        var battery = new BatteryController();
        var json = battery.GenerateConfig();

        Assert.DoesNotThrow(() => battery.LoadConfig(json));
        List<string> scenes = battery.GameScenes();
        Assert.True(scenes.SequenceEqual(battery.GetGameList().Keys));
    }

    [Test]
    public void TestBadSceneParams()
    {
        var json = MockConfig("BadSceneNames.json");
        Assert.Throws<InvalidScenesException>(() => Controller.LoadConfig(json));
    }

    [Test]
    public void TestNoGamesConfig()
    {
        var json = MockConfig("NoGames.json");
        Assert.DoesNotThrow(() => Controller.LoadConfig(json));
    }

    [Test]
    public void TestMultipleSameGame()
    {
        var json = MockConfig("SameGame.json");
        Assert.DoesNotThrow(() => Controller.LoadConfig(json));
        Assert.That(Controller.GetConfig(0), Is.TypeOf<DiggerConfig>());
        Assert.That(Controller.GetConfig(1), Is.TypeOf<DiggerConfig>());
        Assert.That(Controller.GetConfig(2), Is.TypeOf<DiggerConfig>());
        Assert.That(Controller.GetConfig(3), Is.TypeOf<DiggerConfig>());
        Assert.That(Controller.GetConfig(4), Is.TypeOf<DiggerConfig>());
        Assert.That(Controller.GetConfig(5), Is.TypeOf<DiggerConfig>());
    }

    [Test]
    public void TestEmptyConfig()
    {
        var empty_json = "";
        Assert.Throws<EmptyConfigException>(() => Controller.LoadConfig(empty_json));
    }

    [Test]
    public void TestMissingTypeData()
    {
        // Missing parameters are set to default "empty" values.
        var json = MockConfig("MissingTypes.json");
        Assert.Throws<BadConfigException>(() => Controller.LoadConfig(json));
    }
    
    [Test]
    public void TestGetTestName()
    {
        Assert.AreEqual(Controller.GetTestName(0), null); 
        var json = MockConfig("Good.json");
        Assert.DoesNotThrow(() => Controller.LoadConfig(json));
        Assert.AreEqual(Controller.GetTestName(0), "Digger Easy");
    }

    [Test]
    public void TestConvertableBadParamTypes()
    {
        // The json deserializer will attempt to convert types.
        // In this example Scene is -1 and it converts it to "-1".
        var json = MockConfig("BadParamTypes.json");
        Assert.DoesNotThrow(() => Controller.LoadConfig(json));
        Assert.AreEqual(Controller.GetConfig(0).TestName, "-1");
    }

    [Test]
    public void TestUnConvertableBadParamTypes()
    {
        // The json deserializer will attempt to convert types.
        // Sometimes it can't, when the param should be a int but was 
        // given an int.
        var json = MockConfig("BadParamTypes2.json");
        Assert.Throws<BadConfigException>(() => Controller.LoadConfig(json));
    }

    [Test]
    public void TestMissingParams()
    {
        var json = MockConfig("MissingParams.json");
        Assert.DoesNotThrow(() => Controller.LoadConfig(json));
        Assert.AreEqual(Controller.GetConfig(0).Scene, "Digger");
    }

    [Test]
    public void TestWrongSceneName()
    {
        var json = MockConfig("WrongSceneName.json");
        Assert.Throws<InvalidScenesException>(() => Controller.LoadConfig(json));
    }

    [Test]
    public void TestStartBattery()
    {
        Assert.AreEqual(Controller.StartTime(), null); 
        Controller.Start();
        Assert.AreNotEqual(Controller.StartTime(), null); 
    }
    
    [Test]
    public void TestEndBattery()
    {
        Assert.AreEqual(Controller.EndTime(), null); 
        Controller.End();
        Assert.AreNotEqual(Controller.EndTime(), null); 
    }

    [Test]
    public void TestGetGamesScenes()
    {
        var expected = Controller.GetGameList().Keys;
        Assert.True(expected.SequenceEqual(Controller.GameScenes()));
    }
}
