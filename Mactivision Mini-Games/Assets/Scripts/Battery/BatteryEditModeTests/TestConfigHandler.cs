using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestConfigHandler
{

    private string MockConfig(string file)
    {
        return System.IO.File.ReadAllText("./Assets/Scripts/Battery/BatteryEditModeTests/Mocks/" + file);
    }

    private ConfigHandler Config;

    [SetUp]
    public void AutoGenerateAndLoad()
    {
        Config = new ConfigHandler();
        Config.Load(Config.Generate());
    }

    [Test]
    public void TestFolderTimeStamp()
    {
        string actual = System.DateTime.Now.ToString("yyyyMMddhhmmss"); 
        string expected = Config.FolderTimeStamp();
        Assert.AreEqual(expected, actual);  
    }

    [Test]
    public void TestGenerateAndLoad()
    {
        var battery = new ConfigHandler();
        var json = battery.Generate();

        Assert.DoesNotThrow(() => battery.Load(json));
        List<string> scenes = battery.GameScenes();
        Assert.True(scenes.SequenceEqual(battery.GetGameList().Keys));
    }

    [Test]
    public void TestLoadBadSceneParams()
    {
        var json = MockConfig("BadSceneNames.json");
        Assert.Throws<InvalidScenesException>(() => Config.Load(json));
    }

    [Test]
    public void TestLoadNoGamesConfig()
    {
        var json = MockConfig("NoGames.json");
        Assert.DoesNotThrow(() => Config.Load(json));
    }

    [Test]
    public void TestLoadMultipleSameGame()
    {
        var json = MockConfig("SameGame.json");
        Assert.DoesNotThrow(() => Config.Load(json));
        Assert.That(Config.Get(0), Is.TypeOf<DiggerConfig>());
        Assert.That(Config.Get(1), Is.TypeOf<DiggerConfig>());
        Assert.That(Config.Get(2), Is.TypeOf<DiggerConfig>());
        Assert.That(Config.Get(3), Is.TypeOf<DiggerConfig>());
        Assert.That(Config.Get(4), Is.TypeOf<DiggerConfig>());
        Assert.That(Config.Get(5), Is.TypeOf<DiggerConfig>());
    }

    [Test]
    public void TestLoadEmptyConfig()
    {
        var empty_json = "";
        Assert.Throws<EmptyConfigException>(() => Config.Load(empty_json));
    }

    [Test]
    public void TestLoadMissingTypeData()
    {
        // Missing parameters are set to default "empty" values.
        var json = MockConfig("MissingTypes.json");
        Assert.Throws<BadConfigException>(() => Config.Load(json));
    }
    
    [Test]
    public void TestGetTestName()
    {
        Assert.AreEqual(Config.GetTestName(0), null); 
        var json = MockConfig("Good.json");
        Assert.DoesNotThrow(() => Config.Load(json));
        Assert.AreEqual(Config.GetTestName(0), "Digger Easy");
    }

    [Test]
    public void TestLoadLooseTypeParams()
    {
        // The json deserializer will attempt to convert types.
        // In this example Scene is -1 and it converts it to "-1".
        var json = MockConfig("BadParamTypes.json");
        Assert.DoesNotThrow(() => Config.Load(json));
        Assert.AreEqual(Config.Get(0).TestName, "-1");
    }

    [Test]
    public void TestLoadBadTypeParams()
    {
        // The json deserializer will attempt to convert types.
        // Sometimes it can't, when the param should be a int but was 
        // given a string.
        var json = MockConfig("BadParamTypes2.json");
        Assert.Throws<BadConfigException>(() => Config.Load(json));
    }

    [Test]
    public void TestLoadMissingParams()
    {
        var json = MockConfig("MissingParams.json");
        Assert.DoesNotThrow(() => Config.Load(json));
        Assert.AreEqual(Config.Get(0).Scene, "Digger");
    }

    [Test]
    public void TestLoadWrongSceneName()
    {
        var json = MockConfig("WrongSceneName.json");
        Assert.Throws<InvalidScenesException>(() => Config.Load(json));
    }

    [Test]
    public void TestStartBattery()
    {
        Assert.AreEqual(Config.StartTime(), null); 
        Config.Start();
        Assert.AreNotEqual(Config.StartTime(), null); 
    }
    
    [Test]
    public void TestEndBattery()
    {
        Assert.AreEqual(Config.EndTime(), null); 
        Config.End();
        Assert.AreNotEqual(Config.EndTime(), null); 
    }

    [Test]
    public void TestGetGamesScenes()
    {
        var expected = Config.GetGameList().Keys;
        Assert.True(expected.SequenceEqual(Config.GameScenes()));
    }
}
