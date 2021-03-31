using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MeterTests
{
    GameObject testObj;
    Meter testObj_meter;

    // A Test behaves as an ordinary method
    [SetUp]
    public void MeterTestsSetUp()
    {
        testObj = new GameObject();
        testObj_meter = testObj.AddComponent<Meter>() as Meter;
        testObj_meter.meterLevelR = new GameObject().GetComponent<Transform>();
        testObj_meter.meterGoodRangeR = new GameObject();
        testObj_meter.meterGoodRangeR.AddComponent<SpriteRenderer>();
        testObj_meter.meterTopR = new GameObject().GetComponent<Transform>();
        testObj_meter.meterBottomR = new GameObject().GetComponent<Transform>();
        testObj_meter.meterLevelL = new GameObject().GetComponent<Transform>();
        testObj_meter.meterGoodRangeL = new GameObject();
        testObj_meter.meterGoodRangeL.AddComponent<SpriteRenderer>();
        testObj_meter.meterTopL = new GameObject().GetComponent<Transform>();
        testObj_meter.meterBottomL = new GameObject().GetComponent<Transform>();
        testObj_meter.firework = new GameObject();
        testObj_meter.firework_sound = new GameObject().AddComponent<AudioSource>();
        testObj_meter.Init("imaseed", 2f, 5f, 30f, 60f, 0f, 100f, 60f, 50f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [TearDown]
    public void RockstarTestsTearDown()
    {
        testObj_meter = null;
        testObj = null;
    }

    // Initialization resizes good range
    [Test]
    public void TestInit()
    {
        Assert.AreEqual(new Vector3(1f, 0.6f, 1f), testObj_meter.meterGoodRangeR.transform.localScale);
        Assert.AreEqual(new Vector3(1f, 0.6f, 1f), testObj_meter.meterGoodRangeL.transform.localScale);
        Assert.IsTrue(testObj_meter.velocity>=5f);
        Assert.IsTrue(testObj_meter.velocity<=30f);
    }

    // level should drop automatically
    [UnityTest]
    public IEnumerator TestDrop()
    {
        float currlevl = testObj_meter.level;

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(testObj_meter.level<currlevl);
    }

    // raise() should increase level
    [UnityTest]
    public IEnumerator TestRaise()
    {
        float currlevl = testObj_meter.level;
        testObj_meter.Raise();

        yield return new WaitForEndOfFrame();
        Assert.IsTrue(testObj_meter.level>currlevl);
    }
}
