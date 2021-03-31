using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpotlightTests
{
    GameObject testObj;
    Spotlight testObj_spotlight;

    // A Test behaves as an ordinary method
    [SetUp]
    public void SpotlightTestsSetUp()
    {
        testObj = new GameObject();
        testObj_spotlight = testObj.AddComponent<Spotlight>() as Spotlight;
        testObj_spotlight.Init(60f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [TearDown]
    public void RockstarTestsTearDown()
    {
        testObj_spotlight = null;
        testObj = null;
    }

    // position should stay the same if no move
    [UnityTest]
    public IEnumerator TestNoMove()
    {
        Vector2 currPos = testObj_spotlight.GetPosition();

        yield return new WaitForEndOfFrame();
        Assert.AreEqual(currPos, testObj_spotlight.GetPosition());
    }

    // position.x should increase if move right
    [UnityTest]
    public IEnumerator TestMoveRight()
    {
        Vector2 currPos = testObj_spotlight.GetPosition();
        testObj_spotlight.Move(true);

        yield return new WaitForEndOfFrame();
        Assert.IsTrue(currPos.x < testObj_spotlight.GetPosition().x);
    }

    // position.x should increase if move left
    [UnityTest]
    public IEnumerator TestMoveLeft()
    {
        Vector2 currPos = testObj_spotlight.GetPosition();
        testObj_spotlight.Move(false);

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(currPos.x > testObj_spotlight.GetPosition().x);
    }
}
