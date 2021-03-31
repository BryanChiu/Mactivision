using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Newtonsoft.Json.Linq;

public class TestMemoryChoice
{
    /*
     * Test creation of MemoryChoiceEvent objects.
     */
    [Test]
    public void TestMemoryChoiceEvent()
    {
        // MemoryChoiceEvent constructor arguments do not allow null values to be passed
        //   which is enforced by the compiler, no need to test these cases.

        // Test creating memory choice event with reasonable inputs
        System.DateTime dt1 = new System.DateTime(1977, 5, 25, 4, 14, 57);
        List<string> os1 = new List<string>(new string[] { "apple", "banana", "pear" });
        string o1 = "banana";
        bool c1 = true;
        System.DateTime ct1 = new System.DateTime(1977, 5, 25, 4, 14, 58);
        MemoryChoiceEvent mce1 = new MemoryChoiceEvent(dt1, os1, o1, c1, ct1);
        Assert.IsNotNull(mce1);
        Assert.AreEqual(new System.DateTime(1977, 5, 25, 4, 14, 57), mce1.eventTime);
        Assert.IsNotEmpty(mce1.objectsSet);
        Assert.AreEqual(3, mce1.objectsSet.Count);
        Assert.AreEqual("apple", mce1.objectsSet[0]);
        Assert.AreEqual("banana", mce1.objectsSet[1]);
        Assert.AreEqual("pear", mce1.objectsSet[2]);
        Assert.AreEqual("banana", mce1._object);
        Assert.IsTrue(mce1.choice);
        Assert.AreEqual(new System.DateTime(1977, 5, 25, 4, 14, 58), mce1.choiceTime);

        // Test creating memory choice event with other reasonable inputs
        System.DateTime dt2 = new System.DateTime(2021, 2, 7, 3, 43, 21);
        List<string> os2 = new List<string>(new string[] { "orange", "pear" });
        string o2 = "apple";
        bool c2 = false;
        System.DateTime ct2 = new System.DateTime(2021, 2, 7, 3, 43, 23);
        MemoryChoiceEvent mce2 = new MemoryChoiceEvent(dt2, os2, o2, c2, ct2);
        Assert.IsNotNull(mce2);
        Assert.AreEqual(new System.DateTime(2021, 2, 7, 3, 43, 21), mce2.eventTime);
        Assert.IsNotEmpty(mce2.objectsSet);
        Assert.AreEqual(2, mce2.objectsSet.Count);
        Assert.AreEqual("orange", mce2.objectsSet[0]);
        Assert.AreEqual("pear", mce2.objectsSet[1]);
        Assert.AreEqual("apple", mce2._object);
        Assert.IsFalse(mce2.choice);
        Assert.AreEqual(new System.DateTime(2021, 2, 7, 3, 43, 23), mce2.choiceTime);

        // Test creating memory choice event with choiceTime less than eventTime (should throw an exception)
        System.DateTime dt3 = new System.DateTime(1234567);
        System.DateTime ct3 = new System.DateTime(1234566);
        List<string> os3 = new List<string>(new string[] { "grapes" });
        string o3 = "bread";
        bool c3 = true;
        MemoryChoiceEvent mce3 = null;
        string exceptionMessage3 = "";
        try {
            mce3 = new MemoryChoiceEvent(dt3, os3, o3, c3, ct3);
        } catch (InvalidChoiceTimeException e) {
            exceptionMessage3 = e.Message;
        } finally {
            Assert.AreEqual("MemoryChoiceEvent cannot be created: choiceTime cannot be earlier than eventTime", exceptionMessage3);
            Assert.IsNull(mce3);
        }

        // Test creating memory choice event with choiceTime == eventTime (should NOT throw exception)
        System.DateTime dt4 = new System.DateTime(928374656329);
        System.DateTime ct4 = new System.DateTime(928374656329);
        List<string> os4 = new List<string>(new string[] { "grapes" });
        string o4 = "bread";
        bool c4 = true;
        MemoryChoiceEvent mce4 = null;
        try {
            mce4 = new MemoryChoiceEvent(dt4, os4, o4, c4, ct4);
        } catch {
            Assert.Fail("Exception should not be thrown!");
        } finally {
            Assert.IsNotNull(mce4);
            Assert.AreEqual(new System.DateTime(928374656329), mce4.eventTime);
            Assert.AreEqual(new System.DateTime(928374656329), mce4.choiceTime);
            Assert.IsNotEmpty(mce4.objectsSet);
            Assert.AreEqual(1, mce4.objectsSet.Count);
            Assert.AreEqual("grapes", mce4.objectsSet[0]);
            Assert.AreEqual("bread", mce4._object);
            Assert.IsTrue(mce4.choice);
        }

        // Test creating memory choice event with empty objects set (should be fine)
        System.DateTime dt5 = new System.DateTime(2021, 2, 7, 3, 43, 21);
        List<string> os5 = new List<string>();
        string o5 = "apple";
        bool c5 = true;
        System.DateTime ct5 = new System.DateTime(2021, 2, 7, 3, 43, 23);
        MemoryChoiceEvent mce5 = new MemoryChoiceEvent(dt5, os5, o5, c5, ct5);
        Assert.IsNotNull(mce5);
        Assert.AreEqual(new System.DateTime(2021, 2, 7, 3, 43, 21), mce5.eventTime);
        Assert.IsEmpty(mce5.objectsSet);
        Assert.AreEqual(0, mce5.objectsSet.Count);
        Assert.AreEqual("apple", mce5._object);
        Assert.IsTrue(mce5.choice);
        Assert.AreEqual(new System.DateTime(2021, 2, 7, 3, 43, 23), mce5.choiceTime);
    }

    /*
     * Test creation of a MemoryChoiceMetric object.
     */
    [Test]
    public void TestConstructor()
    {
        MemoryChoiceMetric mcm = new MemoryChoiceMetric();
        Assert.IsFalse(mcm.isRecording);
        Assert.IsNotNull(mcm.eventList);
        Assert.IsEmpty(mcm.eventList);
    }

    /*
     * Test the startRecording() and finishRecording() methods of MemoryChoiceMetric objects.
     */
    [Test]
    public void TestStartFinishRecording()
    {
        MemoryChoiceMetric mcm = new MemoryChoiceMetric();
        Assert.IsFalse(mcm.isRecording);    // isRecording start out false

        mcm.startRecording();
        Assert.IsTrue(mcm.isRecording);     // Should be true now

        mcm.finishRecording();
        Assert.IsFalse(mcm.isRecording);    // Should be false now

        mcm.startRecording();
        Assert.IsTrue(mcm.isRecording);
        mcm.startRecording();
        Assert.IsTrue(mcm.isRecording);     // Redundant calls to startRecording still keep it true
        
        mcm.finishRecording();
        Assert.IsFalse(mcm.isRecording);
        mcm.finishRecording();
        Assert.IsFalse(mcm.isRecording);    // Redundant calls to finishRecording still keep is false
    }

    /*
     * Test recordEvent() method of MemoryChoiceMetric
     */
    [Test]
    public void TestRecordEvent()
    {
        // Test adding null event (state of bpm1 shouldn't change)
        MemoryChoiceMetric mcm1 = new MemoryChoiceMetric();
        mcm1.startRecording();
        Assert.IsEmpty(mcm1.eventList);
        Assert.IsTrue(mcm1.isRecording);
        
        mcm1.recordEvent(null);
        Assert.IsEmpty(mcm1.eventList);

        // Test adding events to a MemoryChoiceEvent which has not started recording
        MemoryChoiceMetric mcm2 = new MemoryChoiceMetric();
        Assert.IsFalse(mcm2.isRecording);
        Assert.IsEmpty(mcm2.eventList);

        mcm2.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 1), new List<string>(new string[]{"apple"}), "apple", true, new System.DateTime(2021, 2, 2)));
        Assert.IsEmpty(mcm2.eventList);

        // Test adding valid events to a MemoryChoiceMetric which has started recording
        MemoryChoiceMetric mcm3 = new MemoryChoiceMetric();
        mcm3.startRecording();
        Assert.IsTrue(mcm3.isRecording);
        Assert.IsEmpty(mcm3.eventList);

        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 1), new List<string>(new string[] {"apple", "orange"}), "banana", true, new System.DateTime(2021, 2, 1)));
        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 2), new List<string>(new string[] {"apple", "orange"}), "apple", true, new System.DateTime(2021, 2, 2)));
        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 3), new List<string>(new string[] {"apple", "orange", "pear"}), "banana", false, new System.DateTime(2021, 2, 3)));
        mcm3.finishRecording();

        Assert.AreEqual(3, mcm3.eventList.Count);
        Assert.AreEqual(new System.DateTime(2021, 2, 1), mcm3.eventList[0].eventTime);
        Assert.IsNotEmpty(mcm3.eventList[0].objectsSet);
        Assert.AreEqual(2, mcm3.eventList[0].objectsSet.Count);
            Assert.AreEqual("apple", mcm3.eventList[0].objectsSet[0]);
            Assert.AreEqual("orange", mcm3.eventList[0].objectsSet[1]);
        Assert.AreEqual("banana", mcm3.eventList[0]._object);
        Assert.IsTrue(mcm3.eventList[0].choice);
        Assert.AreEqual(new System.DateTime(2021, 2, 1), mcm3.eventList[0].choiceTime);

        Assert.AreEqual(new System.DateTime(2021, 2, 2), mcm3.eventList[1].eventTime);
        Assert.IsNotEmpty(mcm3.eventList[1].objectsSet);
        Assert.AreEqual(2, mcm3.eventList[1].objectsSet.Count);
            Assert.AreEqual("apple", mcm3.eventList[1].objectsSet[0]);
            Assert.AreEqual("orange", mcm3.eventList[1].objectsSet[1]);
        Assert.AreEqual("apple", mcm3.eventList[1]._object);
        Assert.IsTrue(mcm3.eventList[1].choice);
        Assert.AreEqual(new System.DateTime(2021, 2, 2), mcm3.eventList[1].choiceTime);

        Assert.AreEqual(new System.DateTime(2021, 2, 3), mcm3.eventList[2].eventTime);
        Assert.IsNotEmpty(mcm3.eventList[2].objectsSet);
        Assert.AreEqual(3, mcm3.eventList[2].objectsSet.Count);
            Assert.AreEqual("apple", mcm3.eventList[2].objectsSet[0]);
            Assert.AreEqual("orange", mcm3.eventList[2].objectsSet[1]);
            Assert.AreEqual("pear", mcm3.eventList[2].objectsSet[2]);
        Assert.AreEqual("banana", mcm3.eventList[2]._object);
        Assert.IsFalse(mcm3.eventList[2].choice);
        Assert.AreEqual(new System.DateTime(2021, 2, 3), mcm3.eventList[2].choiceTime);
    }

    /*
     * Test getJSON() method of MemoryChoiceMetric
     */
    [Test]
    public void TestGetJSON()
    {
        // Test getJSON() when metric has no records
        MemoryChoiceMetric mcm1 = new MemoryChoiceMetric();
        Assert.IsEmpty(mcm1.eventList);

        JObject json1 = mcm1.getJSON();
        Assert.IsNotNull(json1);      // JSON entry "memoryChoice" should be empty, as nothing has been recorded

        // Test getJSON() with one record
        MemoryChoiceMetric mcm2 = new MemoryChoiceMetric();
        mcm2.startRecording();
        Assert.IsTrue(mcm2.isRecording);
        Assert.IsEmpty(mcm2.eventList);

        mcm2.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 1), new List<string>(new string[] {"apple"}), "pear", false, new System.DateTime(2021, 2, 1, 1, 2, 3)));
        mcm2.finishRecording();

        JObject json2 = mcm2.getJSON(); 
        Assert.IsNotNull(json2);
        
        JArray json2mc = (JArray) json2["eventList"];
        Assert.AreEqual(1, json2mc.Count);
        Assert.AreEqual("2/1/2021 12:00:00 AM", json2mc[0]["eventTime"].ToString());
        Debug.Log(json2);
        JArray json2mc0os = (JArray) json2mc[0]["objectsSet"];
        Assert.IsNotEmpty(json2mc0os);
        Assert.AreEqual(1, json2mc0os.Count);
        Assert.AreEqual("apple", json2mc0os[0].ToString());
        Assert.AreEqual("pear", json2mc[0]["_object"].ToString());
        Assert.AreEqual(false, json2mc[0]["choice"].ToObject<bool>());
        Assert.AreEqual("2/1/2021 1:02:03 AM", json2mc[0]["choiceTime"].ToString());


        // Test getJSON() with multiple records
        MemoryChoiceMetric mcm3 = new MemoryChoiceMetric();
        mcm3.startRecording();
        Assert.IsTrue(mcm3.isRecording);
        Assert.IsEmpty(mcm3.eventList);

        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 1), new List<string>(new string[] {"apple", "pear"}), "banana", false, new System.DateTime(2021, 2, 1, 1, 2, 3)));
        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 2), new List<string>(new string[] {"apple"}), "pear", false, new System.DateTime(2021, 2, 2, 1, 2, 3)));
        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 3), new List<string>(new string[] {"orange"}), "grapes", true, new System.DateTime(2021, 2, 3, 1, 2, 3)));
        mcm3.finishRecording();
        mcm3.recordEvent(new MemoryChoiceEvent(new System.DateTime(2021, 2, 4), new List<string>(new string[] {"apple"}), "pear", false, new System.DateTime(2021, 2, 4, 1, 2, 3)));    // record an event after finishing recording
        Assert.IsFalse(mcm3.isRecording);
        
        JObject json3 = mcm3.getJSON();
        Assert.IsNotNull(json3);

        JArray json3mc = (JArray) json3["eventList"];
        Assert.AreEqual(3, json3mc.Count);

        Assert.AreEqual("2/1/2021 12:00:00 AM", json3mc[0]["eventTime"].ToString());
        JArray json3mc0os = (JArray) json3mc[0]["objectsSet"];
        Assert.IsNotEmpty(json3mc0os);
        Assert.AreEqual(2, json3mc0os.Count);
            Assert.AreEqual("apple", json3mc0os[0].ToString());
            Assert.AreEqual("pear", json3mc0os[1].ToString());
        Assert.AreEqual("banana", json3mc[0]["_object"].ToString());
        Assert.AreEqual(false, json3mc[0]["choice"].ToObject<bool>());
        Assert.AreEqual("2/1/2021 1:02:03 AM", json3mc[0]["choiceTime"].ToString());

        Assert.AreEqual("2/2/2021 12:00:00 AM", json3mc[1]["eventTime"].ToString());
        JArray json3mc1os = (JArray) json3mc[1]["objectsSet"];
        Assert.IsNotEmpty(json3mc1os);
        Assert.AreEqual(1, json3mc1os.Count);
            Assert.AreEqual("apple", json3mc1os[0].ToString());
        Assert.AreEqual("pear", json3mc[1]["_object"].ToString());
        Assert.AreEqual(false, json3mc[1]["choice"].ToObject<bool>());
        Assert.AreEqual("2/2/2021 1:02:03 AM", json3mc[1]["choiceTime"].ToString());

        Assert.AreEqual("2/3/2021 12:00:00 AM", json3mc[2]["eventTime"].ToString());
        JArray json3mc2os = (JArray) json3mc[2]["objectsSet"];
        Assert.IsNotEmpty(json3mc2os);
        Assert.AreEqual(1, json3mc2os.Count);
            Assert.AreEqual("orange", json3mc2os[0].ToString());
        Assert.AreEqual("grapes", json3mc[2]["_object"].ToString());
        Assert.AreEqual(true, json3mc[2]["choice"].ToObject<bool>());
        Assert.AreEqual("2/3/2021 1:02:03 AM", json3mc[2]["choiceTime"].ToString());
    }
}
