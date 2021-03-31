using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DispenserTests
{
    GameObject testObj;
    Dispenser testObj_dispenser;

    // SetUp before each test method
    [SetUp]
    public void DispenserTestsSetUp()
    {
        testObj = new GameObject();
        testObj_dispenser = testObj.AddComponent<Dispenser>() as Dispenser;
        testObj_dispenser.pipe = new GameObject().AddComponent<Animator>();
        testObj_dispenser.monitor = new GameObject().GetComponent<Transform>();
        testObj_dispenser.screenGreen  = new GameObject();
        testObj_dispenser.screenRed = new GameObject();
        testObj_dispenser.dispense_sound = AudioClip.Create("ds", 1, 1, 1000, false);
        testObj_dispenser.screen_sound = AudioClip.Create("ds", 1, 1, 1000, false);
        testObj.AddComponent<AudioSource>();
        testObj_dispenser.allFoods = new GameObject[7];
        for (int i=0; i<7; i++) {
            testObj_dispenser.allFoods[i] = new GameObject();
            testObj_dispenser.allFoods[i].name = i.ToString();
            GameObject temp = new GameObject();
            temp.name = i.ToString()+" (screen)";
            temp.transform.SetParent(testObj_dispenser.monitor);
        }
    }

    // TearDown after each test method
    [TearDown]
    public void DispenserTestsTearDown()
    {
        testObj_dispenser = null;
        testObj = null;
    }

    // When initializing dispenser with 0 tf, goodFoods should be empty
    [Test]
    public void TestInit0()
    {
        testObj_dispenser.Init("imaseed", 0, 2.5f, 3f);
        Assert.AreEqual(0, testObj_dispenser.goodFoods.Length);
        Assert.AreEqual(null, testObj_dispenser.currentFood);
    }

    // When initializing dispenser, goodFoods should be all empty strings
    [Test]
    public void TestInit5()
    {
        testObj_dispenser.Init("imaseed", 5, 2.5f, 3f);
        Assert.AreEqual(5, testObj_dispenser.goodFoods.Length);
        foreach (string food in testObj_dispenser.goodFoods) {
            Assert.AreEqual("", food);
        }
        Assert.AreEqual(null, testObj_dispenser.currentFood);
    }

    // Dispensing the first food will add a food to goodFoods
    [UnityTest]
    public IEnumerator TestDispenseNext()
    {
        testObj_dispenser.Init("imaseed", 5, 2.5f, 3f);
        testObj_dispenser.DispenseNext();

        yield return new WaitForSeconds(1.75f);
        int count = 0;
        foreach (string food in testObj_dispenser.goodFoods) {
            if (food == "") count++;
        }
        Assert.AreEqual(4, count);
        Assert.AreNotEqual(null, testObj_dispenser.currentFood);
        Assert.AreNotEqual("", testObj_dispenser.currentFood);
    }

    // With correct settings, ensure food is updated every nth food
    [UnityTest]
    public IEnumerator TestDispenseNext6()
    {
        testObj_dispenser.Init("imaseed", 5, 6f, 15f);
        testObj_dispenser.DispenseNext();
        yield return new WaitForSeconds(1.75f);

        int count = 0;
        for (int i=0; i<6; i++) {
            count = 0;
            foreach (string food in testObj_dispenser.goodFoods) {
                if (food == "") count++;
            }
            Assert.AreEqual(4, count);

            testObj_dispenser.DispenseNext();
            yield return null;
        }

        yield return new WaitForSeconds(1.75f);
        count = 0;
        foreach (string food in testObj_dispenser.goodFoods) {
            if (food == "") count++;
        }
        Assert.AreEqual(4, count);
    }

    // If there's only one food, it will always be dispensed
    [UnityTest]
    public IEnumerator TestMakeChoice1()
    {
        testObj_dispenser.Init("imaseed", 1, 2.5f, 3f);
        testObj_dispenser.DispenseNext();

        yield return new WaitForSeconds(1.75f);
        Assert.IsTrue(testObj_dispenser.MakeChoice(true));
    }

    // If there's two foods, both will be good after the second update
    [UnityTest]
    public IEnumerator TestMakeChoice2()
    {
        testObj_dispenser.Init("imaseed", 2, 1f, 15f);
        testObj_dispenser.DispenseNext();
        yield return new WaitForSeconds(1.75f);
        testObj_dispenser.DispenseNext();
        yield return new WaitForSeconds(1.75f);
        Assert.IsFalse(testObj_dispenser.MakeChoice(true));
    }
}
