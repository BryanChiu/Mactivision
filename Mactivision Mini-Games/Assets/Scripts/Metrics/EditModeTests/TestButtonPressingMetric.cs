using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Newtonsoft.Json.Linq;

public class TestButtonPressingMetric
{
    
    /*
     * Test creation of ButtonPressingEvent objects.
     */
    [Test]
    public void TestButtonPressingEvent()
    {
        // ButtonPressingEvent constructor arguments do not allow null values to be passed
        //   which is enforced by the compiler, no need to test these cases.

        // Test creating button pressing event with reasonable inputs
        System.DateTime dt1 = new System.DateTime(1977, 5, 25);
        KeyCode kc1 = KeyCode.Underscore;
        bool kd1 = false;
        ButtonPressingEvent bpe1 = new ButtonPressingEvent(dt1, kc1, kd1);
        Assert.IsNotNull(bpe1);
        Assert.AreEqual(new System.DateTime(1977, 5, 25), bpe1.eventTime);
        Assert.AreEqual(KeyCode.Underscore, bpe1.keyCode);
        Assert.IsFalse(bpe1.keyDown);

        // Test creating button pressing event with other reasonable inputs
        System.DateTime dt2 = new System.DateTime(2021, 2, 1, 4, 13, 12, 140);
        KeyCode kc2 = KeyCode.M;
        bool kd2 = true;
        ButtonPressingEvent bpe2 = new ButtonPressingEvent(dt2, kc2, kd2);
        Assert.IsNotNull(bpe2);
        Assert.AreEqual(new System.DateTime(2021, 2, 1, 4, 13, 12, 140), bpe2.eventTime);
        Assert.AreEqual(KeyCode.M, bpe2.keyCode);
        Assert.IsTrue(bpe2.keyDown);

        // Test creating button pressing event with minimum datetime
        System.DateTime dtmin = System.DateTime.MinValue;
        ButtonPressingEvent bpe3 = new ButtonPressingEvent(dtmin, KeyCode.A, true);
        Assert.IsNotNull(bpe3);
        Assert.AreEqual(System.DateTime.MinValue, bpe3.eventTime);

        // Test creating button pressing event with maximum datetime
        System.DateTime dtmax = System.DateTime.MaxValue;
        ButtonPressingEvent bpe4 = new ButtonPressingEvent(dtmax, KeyCode.Joystick8Button19, true);
        Assert.IsNotNull(bpe4);
        Assert.AreEqual(System.DateTime.MaxValue, bpe4.eventTime);

        // Test creating button pressing event with KeyCode.None (should throw exception)
        System.DateTime dt5 = new System.DateTime(1980, 5, 17);
        KeyCode kcnone = KeyCode.None;
        bool kd5 = false;

        bool exceptionRaised5 = false;
        ButtonPressingEvent bpe5;
        try {
            bpe5 = new ButtonPressingEvent(dt5, kcnone, kd5);
        } catch (InvalidKeyCodeException e) {
            exceptionRaised5 = true;
            Assert.AreEqual("ButtonPressingEvent cannot be created with KeyCode.None", e.Message);
        } finally {
            Assert.IsTrue(exceptionRaised5);
        }
    }

    /*
     * Test creation of a ButtonPressingMetric object.
     */
    [Test]
    public void TestConstructor()
    {
        ButtonPressingMetric bpm = new ButtonPressingMetric();
        Assert.IsFalse(bpm.isRecording);
        Assert.IsNotNull(bpm.eventList);
        Assert.IsEmpty(bpm.eventList);
    }

    /*
     * Test the startRecording() and finishRecording() methods of ButtonPressingMetric objects.
     */
    [Test]
    public void TestStartFinishRecording()
    {
        ButtonPressingMetric bpm = new ButtonPressingMetric();
        Assert.IsFalse(bpm.isRecording);    // isRecording start out false

        bpm.startRecording();
        Assert.IsTrue(bpm.isRecording);     // Should be true now

        bpm.finishRecording();
        Assert.IsFalse(bpm.isRecording);    // Should be false now

        bpm.startRecording();
        Assert.IsTrue(bpm.isRecording);
        bpm.startRecording();
        Assert.IsTrue(bpm.isRecording);     // Redundant calls to startRecording still keep it true
        
        bpm.finishRecording();
        Assert.IsFalse(bpm.isRecording);
        bpm.finishRecording();
        Assert.IsFalse(bpm.isRecording);    // Redundant calls to finishRecording still keep is false
    }

    /*
     * Test recordEvent() method of ButtonPressingMetric
     */
    [Test]
    public void TestRecordEvent()
    {
        // Test adding null event (state of bpm1 shouldn't change)
        ButtonPressingMetric bpm1 = new ButtonPressingMetric();
        bpm1.startRecording();
        Assert.IsEmpty(bpm1.eventList);
        Assert.IsTrue(bpm1.isRecording);
        
        bpm1.recordEvent(null);
        Assert.IsEmpty(bpm1.eventList);

        // Test adding events to a ButtonPressingMetric which has not started recording
        ButtonPressingMetric bpm2 = new ButtonPressingMetric();
        Assert.IsFalse(bpm2.isRecording);
        Assert.IsEmpty(bpm2.eventList);

        bpm2.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 1), KeyCode.Joystick8Button4, true));
        Assert.IsEmpty(bpm2.eventList);

        // Test adding valid events to a ButtonPressingMetric which has started recording
        ButtonPressingMetric bpm3 = new ButtonPressingMetric();
        bpm3.startRecording();
        Assert.IsTrue(bpm3.isRecording);
        Assert.IsEmpty(bpm3.eventList);

        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 1), KeyCode.Joystick8Button4, true));
        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 2), KeyCode.Delete, false));
        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 3), KeyCode.E, false));
        
        Assert.AreEqual(3, bpm3.eventList.Count);
        Assert.AreEqual(new System.DateTime(2021, 2, 1), bpm3.eventList[0].eventTime);
        Assert.AreEqual(KeyCode.Joystick8Button4, bpm3.eventList[0].keyCode);
        Assert.IsTrue(bpm3.eventList[0].keyDown);

        Assert.AreEqual(new System.DateTime(2021, 2, 2), bpm3.eventList[1].eventTime);
        Assert.AreEqual(KeyCode.Delete, bpm3.eventList[1].keyCode);
        Assert.IsFalse(bpm3.eventList[1].keyDown);

        Assert.AreEqual(new System.DateTime(2021, 2, 3), bpm3.eventList[2].eventTime);
        Assert.AreEqual(KeyCode.E, bpm3.eventList[2].keyCode);
        Assert.IsFalse(bpm3.eventList[2].keyDown);
    }


    /*
     * Test getJSON() method of ButtonPressingMetric
     */
    [Test]
    public void TestGetJSON()
    {
        // Test getJSON() when metric has no records
        ButtonPressingMetric bpm1 = new ButtonPressingMetric();
        Assert.IsEmpty(bpm1.eventList);

        JObject json1 = bpm1.getJSON();
        Assert.IsNotNull(json1);
        Assert.IsNotNull(json1["metricName"]);      // JSON entry "buttonPressing" should be empty, as nothing has been recorded
        Assert.IsEmpty(json1["eventList"]);

        // Test getJSON() with one record
        ButtonPressingMetric bpm2 = new ButtonPressingMetric();
        bpm2.startRecording();
        Assert.IsTrue(bpm2.isRecording);
        Assert.IsEmpty(bpm2.eventList);

        bpm2.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 1), KeyCode.B, true));
        bpm2.finishRecording();

        JObject json2 = bpm2.getJSON(); 
        Assert.IsNotNull(json2);
        Assert.IsNotNull(json2["metricName"]);
        
        JArray eventList2 = (JArray) json2["eventList"];
        Assert.AreEqual("buttonPressing", json2["metricName"].ToString());
        Assert.AreEqual(1, eventList2.Count);
        Assert.AreEqual("2021-02-01 12:00:00 AM", eventList2[0]["eventTime"].ToString());
        Assert.AreEqual(KeyCode.B, eventList2[0]["keyCode"].ToObject<KeyCode>());
        Assert.AreEqual(true, eventList2[0]["keyDown"].ToObject<bool>());

        // Test getJSON() with multiple records
        ButtonPressingMetric bpm3 = new ButtonPressingMetric();
        bpm3.startRecording();
        Assert.IsTrue(bpm3.isRecording);
        Assert.IsEmpty(bpm3.eventList);

        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 7, 13, 12, 15), KeyCode.A, true));
        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2021, 2, 7, 13, 12, 16), KeyCode.A, false));
        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2022, 4, 1), KeyCode.LeftAlt, true));
        bpm3.finishRecording();
        bpm3.recordEvent(new ButtonPressingEvent(new System.DateTime(2023, 1, 3), KeyCode.M, false));    // record an event after finishing recording
        Assert.IsFalse(bpm3.isRecording);
        
        JObject json3 = bpm3.getJSON();
        Assert.IsNotNull(json3);
        Assert.IsNotNull(json3["metricName"]);

        JArray eventList3 = (JArray) json3["eventList"];
        Assert.AreEqual(3, eventList3.Count);

        Assert.AreEqual("2021-02-07 1:12:15 PM", eventList3[0]["eventTime"].ToString());
        Assert.AreEqual(KeyCode.A, eventList3[0]["keyCode"].ToObject<KeyCode>());
        Assert.AreEqual(true, eventList3[0]["keyDown"].ToObject<bool>());
        
        Assert.AreEqual("2021-02-07 1:12:16 PM", eventList3[1]["eventTime"].ToString());
        Assert.AreEqual(KeyCode.A, eventList3[1]["keyCode"].ToObject<KeyCode>());
        Assert.AreEqual(false, eventList3[1]["keyDown"].ToObject<bool>());
        
        Assert.AreEqual("2022-04-01 12:00:00 AM", eventList3[2]["eventTime"].ToString());
        Assert.AreEqual(KeyCode.LeftAlt, eventList3[2]["keyCode"].ToObject<KeyCode>());
        Assert.AreEqual(true, eventList3[2]["keyDown"].ToObject<bool>());
    }
}
