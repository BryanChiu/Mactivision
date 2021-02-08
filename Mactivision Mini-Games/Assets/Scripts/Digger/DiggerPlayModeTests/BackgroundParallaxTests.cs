using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BackgroundParallaxTests
{
    GameObject parentObj;
    GameObject testObj;

    // SetUp before each test method
    [SetUp]
    public void BackgroundParallaxTestsSetUp()
    {
        parentObj = new GameObject();
        testObj = new GameObject();
        testObj.transform.parent = parentObj.transform;
    }

    // TearDown after each test method
    [TearDown]
    public void BackgroundParallaxTestsTearDown()
    {
        testObj = null;
        parentObj = null;
    }

    // The testObj's x position should simply change the same amount as its parent
    [UnityTest]
    public IEnumerator TestUpdateXMovesWithParent()
    {
        // test three starting x positions for parent and testObj (neg, zero, pos) (9 tests total)
        for (int px=-1; px<2; px++) {
            for (int tx=-1; tx<2; tx++) {
                parentObj.transform.position = new Vector3(px, 0, 0);
                testObj.transform.position = new Vector3(tx, 0, 0);
                testObj.AddComponent<BackgroundParallax>();

                yield return null;

                // test when parent moves right (+x)
                parentObj.transform.Translate(Vector3.right);

                yield return null;
                Assert.AreEqual(tx+1, testObj.transform.position.x, 0.000001f);

                // test when parent moves left (-x)
                parentObj.transform.Translate(Vector3.left*2);

                yield return null;
                Assert.AreEqual(tx-1, testObj.transform.position.x, 0.000001f);

                Object.Destroy(testObj.GetComponent<BackgroundParallax>());
            } 
        }
    }

    // testObj's y position is positive
    [UnityTest]
    public IEnumerator TestUpdateYPositive()
    {
        // test three starting y positions for parent (neg, zero, pos)
        for (int py=-1; py<2; py++) {
            parentObj.transform.position = new Vector3(0, py, 0);
            testObj.transform.position = new Vector3(0, 1, 0);
            testObj.AddComponent<BackgroundParallax>();

            yield return null;

            // test when parent moves up (+y)
            parentObj.transform.Translate(Vector3.up);

            yield return null;
            Assert.AreEqual(1.1f, testObj.transform.position.y, 0.000001f);

            // test when parent moved down (-y)
            parentObj.transform.Translate(Vector3.down*2);

            yield return null;
            Assert.AreEqual(0.9f, testObj.transform.position.y, 0.000001f);

            Object.Destroy(testObj.GetComponent<BackgroundParallax>());
        }
    }

    // testObj's y position is zero
    [UnityTest]
    public IEnumerator TestUpdateYZero()
    {
        // test three starting y positions for parent (neg, zero, pos)
        for (int py=-1; py<2; py++) {
            parentObj.transform.position = new Vector3(0, py, 0);
            testObj.transform.position = new Vector3(0, 0, 0);
            testObj.AddComponent<BackgroundParallax>();

            yield return null;

            // test when parent moves up (+y)
            parentObj.transform.Translate(Vector3.up);

            yield return null;
            Assert.AreEqual(0.1f, testObj.transform.position.y, 0.000001f);

            // test when parent moved down (-y)
            parentObj.transform.Translate(Vector3.down*2);

            yield return null;
            Assert.AreEqual(-0.1f, testObj.transform.position.y, 0.000001f);

            Object.Destroy(testObj.GetComponent<BackgroundParallax>());
        }
    }

    // testObj's y position is negative
    [UnityTest]
    public IEnumerator TestUpdateYNegative()
    {
        // test three starting y positions for parent (neg, zero, pos)
        for (int py=-1; py<2; py++) {
            parentObj.transform.position = new Vector3(0, py, 0);
            testObj.transform.position = new Vector3(0, -1, 0);
            testObj.AddComponent<BackgroundParallax>();

            yield return null;

            // test when parent moves up (+y)
            parentObj.transform.Translate(Vector3.up);

            yield return null;
            Assert.AreEqual(-0.9f, testObj.transform.position.y, 0.000001f);

            // test when parent moved down (-y)
            parentObj.transform.Translate(Vector3.down*2);

            yield return null;
            Assert.AreEqual(-1.1f, testObj.transform.position.y, 0.000001f);

            Object.Destroy(testObj.GetComponent<BackgroundParallax>());
        }
    }
}
