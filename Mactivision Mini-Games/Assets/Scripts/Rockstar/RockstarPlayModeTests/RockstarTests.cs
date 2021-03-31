using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RockstarTests
{
    GameObject testObj;
    Rockstar testObj_rockstar;

    // A Test behaves as an ordinary method
    [SetUp]
    public void RockstarTestsSetUp()
    {
        testObj = new GameObject();
        testObj_rockstar = testObj.AddComponent<Rockstar>() as Rockstar;
        testObj_rockstar.rockstar = new GameObject().AddComponent<Animator>();
        testObj_rockstar.Init("imaseed", 0f, 10f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [TearDown]
    public void RockstarTestsTearDown()
    {
        testObj_rockstar = null;
        testObj = null;
    }

    // Initial rockstar velocity is 0
    [Test]
    public void TestInitVelocity()
    {
        Assert.AreEqual(0f, testObj_rockstar.currVelocity);
    }

    // currVelocity doesn't change until position changes
    [UnityTest]
    public IEnumerator TestPositionChange()
    {
        Vector2 startPos = testObj_rockstar.GetPosition();

        yield return new WaitForSeconds(0.1f);
        if (testObj_rockstar.currVelocity==0f) {
            Assert.AreEqual(startPos, testObj_rockstar.GetPosition());
        } else {
            Assert.AreNotEqual(startPos, testObj_rockstar.GetPosition());
        }
    }
}
