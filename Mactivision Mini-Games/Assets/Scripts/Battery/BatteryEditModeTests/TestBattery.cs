using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBattery
{

    // Battery is mostly a wrapper
    // But we need to test when methods are called without 
    // Config being loaded first.

    [Test]
    public void TestBatteryWhenConfigNotLoaded()
    {
        Battery.Instance.Reset();

        // If it failed it would throw NullReferenceException
        Assert.DoesNotThrow(() => Battery.Instance.EndBattery());
        Assert.DoesNotThrow(() => Battery.Instance.StartBattery());
        Assert.DoesNotThrow(() => Battery.Instance.GetCurrentConfig());
        Assert.DoesNotThrow(() => Battery.Instance.GetGameName());
        Assert.DoesNotThrow(() => Battery.Instance.WriteExampleConfig());
        Assert.DoesNotThrow(() => Battery.Instance.GetGameList());
        Assert.DoesNotThrow(() => Battery.Instance.LoadNextScene());
    }
}
