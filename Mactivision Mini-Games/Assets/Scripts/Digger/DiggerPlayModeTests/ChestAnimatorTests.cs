using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChestAnimatorTests
{
    GameObject testObj;
    BoxCollider2D testObj_box;
    ChestAnimator testObj_chest;

    GameObject arbitraryObj;
    BoxCollider2D arbitraryObj_box;

    // SetUp before each test method
    [SetUp]
    public void ChestAnimatorTestsSetUp()
    {
        testObj = new GameObject();
        testObj_chest = testObj.AddComponent<ChestAnimator>() as ChestAnimator;
        testObj_box = testObj.AddComponent<BoxCollider2D>() as BoxCollider2D;
        testObj_box.isTrigger = true;
        testObj_box.size = new Vector2(2f, 2f);
        testObj_chest.coin = new GameObject();
        testObj.AddComponent<Animator>();
        testObj.AddComponent<AudioSource>();
        testObj.transform.position = Vector3.zero;

        arbitraryObj = new GameObject();
        arbitraryObj_box = arbitraryObj.AddComponent<BoxCollider2D>() as BoxCollider2D;
        arbitraryObj_box.size = new Vector2(2f, 2f);
        arbitraryObj.AddComponent<Rigidbody2D>();
        arbitraryObj.transform.position = new Vector3(4f, 4f, 0f);
    }

    // TearDown after each test method
    [TearDown]
    public void ChestAnimatorTestsTearDown()
    {
        testObj = null;
        testObj_box = null;
        testObj_chest = null;

        arbitraryObj = null;
        arbitraryObj_box = null;
    }

    // Chest should not be opened if the object is not the "player" and they are not touching
    [UnityTest]
    public IEnumerator TestOnTriggerEnter2DNotPlayerNotTouching()
    {
        arbitraryObj.transform.position = new Vector3(3f, 3f, 0f);

        yield return new WaitForFixedUpdate();
        Assert.IsFalse(testObj_chest.opened);
    }

    // Chest should not be opened if the object is not the "player" and they are touching
    [UnityTest]
    public IEnumerator TestOnTriggerEnter2DNotPlayerIsTouching()
    {
        arbitraryObj.transform.position = new Vector3(1f, 1f, 0f);

        yield return new WaitForFixedUpdate();
        Assert.IsFalse(testObj_chest.opened);
    }

    // Chest should not be opened if the object is the "player" and they are not touching
    [UnityTest]
    public IEnumerator TestOnTriggerEnter2DIsPlayerNotTouching()
    {
        testObj_chest.player = arbitraryObj;
        arbitraryObj.transform.position = new Vector3(3f, 3f, 0f);

        yield return new WaitForFixedUpdate();
        Assert.IsFalse(testObj_chest.opened);
    }

    // Chest should be opened if the object is the "player" and they are touching
    [UnityTest]
    public IEnumerator TestOnTriggerEnter2DIsPlayerIsTouching()
    {
        testObj_chest.player = arbitraryObj;
        arbitraryObj.transform.position = new Vector3(1f, 1f, 0f);

        yield return new WaitForFixedUpdate();
        Assert.IsTrue(testObj_chest.opened);
    }

    // Coin should be in incorrect location after distance/speed - 0.1 seconds
    [UnityTest]
    public IEnumerator TestUpdateCoinBeforeDestination()
    {
        testObj_chest.opened = true;
        testObj_chest.coin.transform.localPosition = Vector3.zero;

        float waitTime = Vector3.Distance(testObj_chest.coin.transform.localPosition, testObj_chest.destination)/testObj_chest.coinspeed;

        yield return new WaitForSeconds(waitTime-0.1f);
        Assert.IsFalse(testObj_chest.coin.transform.localPosition == testObj_chest.destination);
    }

    // Coin should be in correct location after distance/speed seconds
    [UnityTest]
    public IEnumerator TestUpdateCoinReachedDestination()
    {
        testObj_chest.opened = true;
        testObj_chest.coin.transform.localPosition = Vector3.zero;

        float waitTime = Vector3.Distance(testObj_chest.coin.transform.localPosition, testObj_chest.destination)/testObj_chest.coinspeed;

        yield return new WaitForSeconds(waitTime);
        Assert.IsTrue(testObj_chest.coin.transform.localPosition == testObj_chest.destination);
    }

    // Coin should not have moved if chest is not open
    [UnityTest]
    public IEnumerator TestUpdateCoinUnmoved()
    {
        testObj_chest.opened = false;
        testObj_chest.coin.transform.localPosition = Vector3.zero;

        float waitTime = Vector3.Distance(testObj_chest.coin.transform.localPosition, testObj_chest.destination)/testObj_chest.coinspeed;

        yield return new WaitForSeconds(waitTime);
        Assert.IsTrue(testObj_chest.coin.transform.localPosition == Vector3.zero);
    }
}
