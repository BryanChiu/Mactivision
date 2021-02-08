using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSceneController
{
    private SceneController Scene;

    [SetUp]
    public void NewSceneController()
    {
        var games = new List<string>();
        games.Add("Digger");
        games.Add("Feeder");
        Scene = new SceneController("Start", "End", games); 
    }

    [Test]
    public void TestCurrent()
    {
        Assert.AreEqual(Scene.Current(), -1);
    }
    
    [Test]
    public void TestNext()
    {
        Assert.AreEqual(Scene.Current(), -1);
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 0);
    }

    [Test]
    public void TestName()
    {
        Assert.AreEqual(Scene.Current(), -1);
        Assert.AreEqual(Scene.Name(), "Start");
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 0);
        Assert.AreEqual(Scene.Name(), "Digger");
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 1);
        Assert.AreEqual(Scene.Name(), "Feeder");
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 2);
        Assert.AreEqual(Scene.Name(), "End");
    }
   
    [Test]
    public void TestMax()
    {
        Scene.setMax(1);
        Assert.AreEqual(Scene.Current(), -1);
        Assert.AreEqual(Scene.Name(), "Start");
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 0);
        Assert.AreEqual(Scene.Name(), "Digger");
        Scene.Next();
        Assert.AreEqual(Scene.Current(), 1);
        Assert.AreEqual(Scene.Name(), "End");
    }

    [Test]
    public void TestOutofRangeIndex()
    {
        Scene.setIndex(-100);
        Assert.AreEqual(Scene.Current(), -100);
        Assert.AreEqual(Scene.Name(), "Start");
        Scene.setIndex(100100);
        Assert.AreEqual(Scene.Current(), 100100);
        Assert.AreEqual(Scene.Name(), "End");
    }
    

}
