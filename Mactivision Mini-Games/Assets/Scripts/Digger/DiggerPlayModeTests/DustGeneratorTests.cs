using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DustGeneratorTests
{
    [UnityTest]
    public IEnumerator TestUpdateTransparentAfterLifeSpan()
    {
        GameObject testObj = new GameObject();
        DustGenerator dg = testObj.AddComponent<DustGenerator>() as DustGenerator;
        SpriteRenderer sr = testObj.AddComponent<SpriteRenderer>() as SpriteRenderer;

        yield return new WaitForEndOfFrame();
        Assert.AreEqual(1f, sr.color.a, 0.000001f); // sprite is opaque when first created

        yield return new WaitForSeconds(dg.lifespan);
        Assert.IsTrue(sr.color.a <= 0f); // sprite is transparent after its lifespan
    }
}
