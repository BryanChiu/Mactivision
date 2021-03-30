using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class LevelManagerTests
{
    class LevelManagerInheritor : LevelManager{}

    GameObject testObj;
    LevelManagerInheritor testObj_lm;

    [SetUp]
    public void LevelManagerTestsSetUp()
    {
        testObj = new GameObject();
        testObj_lm = testObj.AddComponent<LevelManagerInheritor>() as LevelManagerInheritor;
        testObj_lm.postprocess = new GameObject().AddComponent<PostProcessVolume>();
        testObj_lm.instructionParent = new GameObject();
        testObj_lm.countdownText = new GameObject();
        testObj_lm.outroText = new GameObject();
        testObj_lm.textBG = new GameObject();
        testObj_lm.textBG.AddComponent<RectTransform>();
        testObj_lm.textBG_Main = new GameObject().AddComponent<RectTransform>();
        testObj_lm.textBG_Edge = new GameObject().AddComponent<RectTransform>();
        testObj_lm.textBG_LArm = new GameObject().AddComponent<RectTransform>();
        testObj_lm.textBG_RArm = new GameObject().AddComponent<RectTransform>();
        testObj_lm.textBG_Main.SetParent(testObj_lm.textBG.GetComponent<RectTransform>());
        testObj_lm.textBG_Edge.SetParent(testObj_lm.textBG.GetComponent<RectTransform>());
        testObj_lm.textBG_LArm.SetParent(testObj_lm.textBG.GetComponent<RectTransform>());
        testObj_lm.textBG_RArm.SetParent(testObj_lm.textBG.GetComponent<RectTransform>());
        testObj_lm.sound = testObj.AddComponent<AudioSource>();  
    }

    [TearDown]
    public void LevelManagerTestsTearDown()
    {
        testObj_lm = null;
        testObj = null;
    }

    // all textBG rects should have resized and/or repositioned
    [Test]
    public void LevelManagerResizeTextBG()
    {
        Rect rect = testObj_lm.GetRect(testObj_lm.instructionParent);
        testObj_lm.ResizeTextBG(rect);
        Rect bgRect = testObj_lm.textBG.GetComponent<RectTransform>().rect;

        Assert.AreEqual(rect.width+40, bgRect.width, 0.000001f);
        Assert.AreEqual(rect.height+40, bgRect.height, 0.000001f);

        Assert.AreEqual(bgRect.x, testObj_lm.textBG_Main.GetComponent<RectTransform>().rect.x, 0.000001f);
        Assert.AreEqual(bgRect.y, testObj_lm.textBG_Main.GetComponent<RectTransform>().rect.y, 0.000001f);
        Assert.AreEqual(bgRect.width, testObj_lm.textBG_Main.GetComponent<RectTransform>().rect.width, 0.000001f);
        Assert.AreEqual(bgRect.height, testObj_lm.textBG_Main.GetComponent<RectTransform>().rect.height, 0.000001f);
    }

    // Setup initalizes values
    [Test]
    public void LevelManagerSetup()
    {
        testObj_lm.Setup();
        Assert.AreEqual(0, testObj_lm.lvlState);
    }

    // StartLevel should change the lvlState to 1, and four secs and 4 frame completions later to 2
    [UnityTest]
    public IEnumerator LevelManagerStartLevel()
    {
        testObj_lm.StartLevel();
        Assert.AreEqual(1, testObj_lm.lvlState);
        Assert.IsTrue(testObj_lm.countdownText.activeInHierarchy);

        yield return new WaitForSeconds(4.1f);
        Assert.AreEqual(2, testObj_lm.lvlState);
        Assert.IsFalse(testObj_lm.countdownText.activeInHierarchy);

    }

    // EndLevel should change the lvlState to 4 instantly if passed 0
    [UnityTest]
    public IEnumerator LevelManagerEndLevel0()
    {
        testObj_lm.EndLevel(0f);
        yield return null;
        Assert.AreEqual(4, testObj_lm.lvlState);
        Assert.IsTrue(testObj_lm.outroText.activeInHierarchy);
    }

    // EndLevel should change the lvlState to 3 and a second later to 4 if passed 1
    [UnityTest]
    public IEnumerator LevelManagerEndLevel1()
    {
        testObj_lm.EndLevel(1f);
        Assert.AreEqual(3, testObj_lm.lvlState);

        yield return new WaitForSeconds(1f);
        Assert.AreEqual(4, testObj_lm.lvlState);
        Assert.IsTrue(testObj_lm.outroText.activeInHierarchy);
    }

    // EndLevel should change the lvlState to 4 instantly if passed a negative number
    [UnityTest]
    public IEnumerator LevelManagerEndLevelNeg()
    {
        testObj_lm.EndLevel(-1f);
        yield return null;
        Assert.AreEqual(4, testObj_lm.lvlState);
        Assert.IsTrue(testObj_lm.outroText.activeInHierarchy);
    }
}
