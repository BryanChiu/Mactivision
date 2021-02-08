using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Newtonsoft.Json.Ling;

public class TestLinearVariableMetric
{
    [Test]
    public void TestLinearVariableEvent()
    {
        System.DateTime dt1 = new System.DateTime(1977, 5, 25);
        int cv1 = 10;
        int vc1 = 5;
        int ri1 = 0;
        LinearVariableEvent lve1 = new LinearVariableEvent(dt1, cv1, vc1, ri1);
        Assert.IsNotNull(lve1);
        Assert.AreEqual(new System.DateTime(1977, 5, 25), lve1.eventTime);
        Assert.AreEqual(10, lve1.currentValue);
        Assert.AreEqual(5, lve1.valueChange);
        Assert.AreEqual(0, lve1.reasonIndex);

        // Test creating position event with other reasonable inputs
        System.DateTime dt2 = new System.DateTime(2021, 2, 1, 4, 13, 12, 140);
        int cv2 = 20;
        int vc2 = 10;
        int ri2 = 1;
        LinearVariableEvent lve2 = new LinearVariableEvent(dt2, cv2, vc2, ri2);
        Assert.IsNotNull(lve2);
        Assert.AreEqual(new System.DateTime(2021, 2, 1, 4, 13, 12, 140), lve2.eventTime);
        Assert.AreEqual(20, lve1.currentValue);
        Assert.AreEqual(10, lve1.valueChange);
        Assert.AreEqual(1, lve1.reasonIndex);

        // Test creating position event with minimum datetime
        System.DateTime dtmin = System.DateTime.MinValue;
        LinearVariableEvent lve3 = new LinearVariableEvent(dtmin, 0, 0, 0);
        Assert.IsNotNull(lve3);
        Assert.AreEqual(System.DateTime.MinValue, lve3.eventTime);

        // Test creating position event with maximum datetime
        System.DateTime dtmax = System.DateTime.MaxValue;
        LinearVariableEvent lve4 = new LinearVariableEvent(dtmax, 0, 0, 0);
        Assert.IsNotNull(lve4);
        Assert.AreEqual(System.DateTime.MaxValue, lve4.eventTime);
    }

    [Test]
    public void TestConstructor()
    {
        LinearVariableMetric lvm = new LinearVariableMetric();
        Assert.IsFalse(lvm.isRecording);
        Assert.IsNotNull(lvm.eventList);
        Assert.IsEmpty(lvm.eventList);
    }

    [Test]
    public void TestStartFinishRecording()
    {
        LinearVariableMetric lvm = new LinearVariableMetric();
        Assert.IsFalse(lvm.isRecording);

        lvm.startRecording();
        Assert.IsTrue(lvm.isRecording);

        lvm.finishRecording();
        Assert.IsFalse(lvm.isRecording);
    }

    [Test]
    public void TestRecordEvent()
    {
        LinearVariableMetric lvm1 = new LinearVariableMetric();
        lvm1.startRecording();
        Assert.IsEmpty(lvm1.eventList);
        Assert.IsTrue(lvm1.isRecording);
        
        lvm1.recordEvent(null);
        Assert.IsEmpty(lvm1.eventList);

        // Test adding events to a LinearVariableMetric which has not started recording
        LinearVariableMetric lvm2 = new LinearVariableMetric();
        Assert.IsFalse(lvm2.isRecording);
        Assert.IsEmpty(lvm2.eventList);

        lvm2.recordEvent(new LinearVariableEvent(new System.DateTime(2021, 2, 1), 0, 0, 0));
        Assert.IsEmpty(lvm2.eventList);

        // Test adding valid events to a LinearVariableMetric which has started recording
        LinearVariableMetric lvm3 = new LinearVariableMetric();
        lvm3.startRecording();
        Assert.IsTrue(lvm3.isRecording);
        Assert.IsEmpty(lvm3.eventList);

        lvm3.recordEvent(new LinearVariableEvent(new System.DateTime(2021, 2, 1), 0, 10, 1));
        lvm3.recordEvent(new LinearVariableEvent(new System.DateTime(2021, 2, 2), 10, 10, 1));
        lvm3.recordEvent(new LinearVariableEvent(new System.DateTime(2021, 2, 3), 20, 5, 0));
        
        Assert.AreEqual(3, lvm3.eventList.Count);
        Assert.AreEqual(new System.DateTime(2021, 2, 1), lvm3.eventList[0].eventTime);
        Assert.AreEqual(0, lvm3.eventList[0].currentValue);
        Assert.AreEqual(10, lvm3.eventList[0].valueChange);
        Assert.AreEqual(1, lvm3.eventList[0].reasonIndex);

        Assert.AreEqual(new System.DateTime(2021, 2, 2), lvm3.eventList[1].eventTime);
        Assert.AreEqual(10, lvm3.eventList[1].currentValue);
        Assert.AreEqual(10, lvm3.eventList[1].valueChange);
        Assert.AreEqual(1, lvm3.eventList[1].reasonIndex);

        Assert.AreEqual(new System.DateTime(2021, 2, 3), lvm3.eventList[2].eventTime);
        Assert.AreEqual(20, lvm3.eventList[2].currentValue);
        Assert.AreEqual(5, lvm3.eventList[2].valueChange);
        Assert.AreEqual(0, lvm3.eventList[2].reasonIndex);
    }

    [Test]
    public void TestGetJSON()
    {
        // Test getJSON() when metric has no records
        LinearVariableMetric lvm1 = new LinearVariableMetric();
        Assert.IsEmpty(lvm1.eventList);

        JObject json1 = lvm1.getJSON();
        Assert.IsNotNull(json1);
        Assert.IsNotNull(json1["linearVariables"]);      
        Assert.IsEmpty(json1["linearVariables"]);

        // Test getJSON() with one record
        LinearVariableMetric lvm2 = new LinearVariableMetric();
        lvm2.startRecording();
        Assert.IsTrue(lvm2.isRecording);
        Assert.IsEmpty(lvm2.eventList);

        lvm2.recordEvent(new LinearVariableEvent(new System.DateTime(2021, 2, 1), 10, 10, 1));
        lvm2.finishRecording();

        JObject json2 = lvm2.getJSON(); 
        Assert.AreEqual(1, lvm2.eventList.Count);
        Assert.IsNotNull(json2);
        Assert.IsNotNull(json2["linearVariables"]);
        
        JArray json2lv = (JArray) json2["linearVariables"];
        Assert.AreEqual(1, json2lv.Count);
        Assert.AreEqual("2021-02-01 12:00:00 AM", json2lv[0]["eventTime"].ToString());
        Assert.AreEqual(10, json2lv[0]["currentValue"].ToObject<long>());
        Assert.AreEqual(10, json2lv[0]["valueChange"].ToObject<long>());
        Assert.AreEqual(1, json2lv[0]["reasonIndex"].ToObject<int>());
    }
}
