using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerControllerTests
{
    GameObject testObj;
    PlayerController testObj_player;

    // SetUp before each test method
    [SetUp]
    public void PlayerControllerTestsSetUp()
    {
        testObj = new GameObject();
        testObj_player = testObj.AddComponent<PlayerController>() as PlayerController;
        testObj_player.dust1 = new GameObject();
        testObj_player.dust2 = new GameObject();
        testObj_player.jackhammer = new GameObject();
        testObj.AddComponent<Rigidbody2D>();
    }

    // TearDown after each test method
    [TearDown]
    public void PlayerControllerTestsTearDown()
    {
        testObj_player = null;
        testObj = null;
    }

    // DigUp should put the jackhammer in the hammerJump position
    [UnityTest]
    public IEnumerator TestDigUpOncePosition()
    {
        testObj_player.DigUp();

        yield return null;
        Assert.IsTrue(testObj_player.jackhammer.transform.localPosition == testObj_player.hammerJump);
    }

    // Multiple DigUp calls should still put the jackhammer in the hammerJump position
    [UnityTest]
    public IEnumerator TestDigUpManyPosition()
    {
        testObj_player.DigUp();
        testObj_player.DigUp();
        testObj_player.DigUp();
        testObj_player.DigUp();
        testObj_player.DigUp();

        yield return null;
        Assert.IsTrue(testObj_player.jackhammer.transform.localPosition == testObj_player.hammerJump);
    }

    // DigDown should put the jackhammer in the hammerRest position
    [UnityTest]
    public IEnumerator TestDigDownOncePosition()
    {
        testObj_player.DigDown();

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(testObj_player.jackhammer.transform.localPosition == testObj_player.hammerRest);
    }

    // Multiple DigDown calls should still put the jackhammer in the hammerRest position
    [UnityTest]
    public IEnumerator TestDigDownManyPosition()
    {
        testObj_player.DigDown();
        testObj_player.DigDown();
        testObj_player.DigDown();
        testObj_player.DigDown();
        testObj_player.DigDown();

        yield return new WaitForSeconds(2f);
        Assert.IsTrue(testObj_player.jackhammer.transform.localPosition == testObj_player.hammerRest);
    }

    // DigDown should create a new object, and destroy it after 2 seconds
    [UnityTest]
    public IEnumerator TestDigDownOneNewObject()
    {
        int numOfGameObjects = testObj.scene.GetRootGameObjects().Length;
        testObj_player.DigDown();

        yield return null;
        Assert.AreEqual(numOfGameObjects+1, testObj.scene.GetRootGameObjects().Length);

        yield return new WaitForSeconds(2f);
        Assert.AreEqual(numOfGameObjects, testObj.scene.GetRootGameObjects().Length);
    }

    // DigDown should create a new object, and destroy it after 2 seconds
    [UnityTest]
    public IEnumerator TestDigDownFewNewObjects()
    {
        int numOfGameObjects = testObj.scene.GetRootGameObjects().Length;
        testObj_player.DigDown();

        yield return new WaitForSeconds(0.75f);
        Assert.AreEqual(numOfGameObjects+1, testObj.scene.GetRootGameObjects().Length);

        testObj_player.DigDown();

        yield return new WaitForSeconds(0.75f);
        Assert.AreEqual(numOfGameObjects+2, testObj.scene.GetRootGameObjects().Length);

        yield return new WaitForSeconds(0.75f);
        Assert.AreEqual(numOfGameObjects+1, testObj.scene.GetRootGameObjects().Length);

        yield return new WaitForSeconds(1f);
        Assert.AreEqual(numOfGameObjects, testObj.scene.GetRootGameObjects().Length);
    }
}
