using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GroundBreakerTests
{
    GameObject testObj;
    BoxCollider2D testObj_box;
    GroundBreaker testObj_gb;

    GameObject playerObj;
    BoxCollider2D playerObj_box;
    PlayerController playerObj_pc;

    GameObject arbitraryObj;
    BoxCollider2D arbitraryObj_box;
    PlayerController arbitraryObj_pc;

    // SetUp before each test method
    [SetUp]
    public void GroundBreakerTestsSetUp()
    {
        testObj = new GameObject();
        testObj_box = testObj.AddComponent<BoxCollider2D>() as BoxCollider2D;
        testObj_box.size = new Vector2(2f, 2f);
        testObj.transform.position = Vector3.zero;
        testObj_gb = testObj.AddComponent<GroundBreaker>() as GroundBreaker;
        testObj_gb.breakAnimation = new Sprite[10];
        testObj.AddComponent<SpriteRenderer>();

        playerObj = new GameObject();
        playerObj.AddComponent<Rigidbody2D>();
        playerObj_box = playerObj.AddComponent<BoxCollider2D>() as BoxCollider2D;
        playerObj_box.isTrigger = true;
        playerObj_box.size = new Vector2(2f, 2f);
        playerObj.transform.position = new Vector3(4f, 4f, 0f);
        playerObj_pc = playerObj.AddComponent<PlayerController>() as PlayerController;

        testObj_gb.player = playerObj_pc;

        arbitraryObj = new GameObject();
        arbitraryObj.AddComponent<Rigidbody2D>();
        arbitraryObj_box = arbitraryObj.AddComponent<BoxCollider2D>() as BoxCollider2D;
        arbitraryObj_box.isTrigger = true;
        arbitraryObj_box.size = new Vector2(2f, 2f);
        arbitraryObj.transform.position = new Vector3(4f, 4f, 0f);
        arbitraryObj_pc = arbitraryObj.AddComponent<PlayerController>() as PlayerController;
    }

    // TearDown after each test method
    [TearDown]
    public void GroundBreakerTestsTearDown()
    {
        testObj_gb = null;
        testObj_box = null;
        testObj = null;

        playerObj_pc = null;
        playerObj_box = null;
        playerObj = null;

        arbitraryObj_pc = null;
        arbitraryObj_box = null;
        arbitraryObj = null;
    }

    // no change if the object is not the "player", not touching, didn't dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DNotPlayerNotTouchingNotDig()
    {
        testObj_gb.hitsToBreak = 1;
        arbitraryObj.transform.position = new Vector3(3f, 3f, 0f);
        playerObj_pc.dig = false;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is the "player", not touching, didn't dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DIsPlayerNotTouchingNotDig()
    {
        testObj_gb.hitsToBreak = 1;
        playerObj.transform.position = new Vector3(3f, 3f, 0f);
        playerObj_pc.dig = false;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is not the "player", is touching, didn't dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DNotPlayerIsTouchingNotDig()
    {
        testObj_gb.hitsToBreak = 1;
        arbitraryObj.transform.position = new Vector3(1f, 1f, 0f);
        playerObj_pc.dig = false;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is the "player", is touching, didn't dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DIsPlayerIsTouchingNotDig()
    {
        testObj_gb.hitsToBreak = 1;
        playerObj.transform.position = new Vector3(1f, 1f, 0f);
        playerObj_pc.dig = false;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is not the "player", not touching, did dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DNotPlayerNotTouchingIsDig()
    {
        testObj_gb.hitsToBreak = 1;
        arbitraryObj.transform.position = new Vector3(3f, 3f, 0f);
        playerObj_pc.dig = true;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is the "player", not touching, did dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DIsPlayerNotTouchingIsDig()
    {
        testObj_gb.hitsToBreak = 1;
        playerObj.transform.position = new Vector3(3f, 3f, 0f);
        playerObj_pc.dig = true;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // no change if the object is not the "player", is touching, did dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DNotPlayerIsTouchingIsDig()
    {
        testObj_gb.hitsToBreak = 1;
        arbitraryObj.transform.position = new Vector3(1f, 1f, 0f);
        playerObj_pc.dig = true;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj.activeSelf);
    }

    // object deactivated if the object is the "player", is touching, did dig
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DIsPlayerIsTouchingIsDig()
    {
        testObj_gb.hitsToBreak = 1;
        playerObj.transform.position = new Vector3(1f, 1f, 0f);
        playerObj_pc.dig = true;

        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Assert.IsFalse(testObj.activeSelf);
    }

    // object is active until number of hits reached
    [UnityTest]
    public IEnumerator TestOnTriggerStay2DActiveUntilBroken()
    {
        testObj_gb.hitsToBreak = 10;
        playerObj.transform.position = new Vector3(1f, 1f, 0f);

        yield return new WaitForFixedUpdate();
        for (int i=0; i<9; i++) {
            playerObj_pc.dig = true;
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(testObj.activeSelf);
        }

        playerObj_pc.dig = true;
        yield return new WaitForFixedUpdate();
        Assert.IsFalse(testObj.activeSelf);
    }
}
