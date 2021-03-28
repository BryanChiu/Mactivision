using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

// This class provides methods to inherited classes for pre and post-game features. It starts the
// scene with a blurred game scene with an introductory text, then a countdown, and a end game text.
public abstract class LevelManager : MonoBehaviour
{
    public PostProcessVolume postprocess;   // graphical effects, used for blurring the scene
    public GameObject introText;            // text object displayed before actual game begins
    public GameObject outroText;            // text object displayed after game ends
    public GameObject countdownText;        // text object displaying countdown to begin actual game
    public string countDoneText = "Start!"; // text instead of "0" after "3, 2, 1"
    public GameObject instructionParent;    // parent group for instructions
    public GameObject[] instructions;       // game instructions displayed before game starts
    public int instructionCount;            // keeps track of which instruction we're on

    public GameObject textBG;               // parent group for graphics displayed behind text
    public RectTransform textBG_Main;
    public RectTransform textBG_Edge;
    public RectTransform textBG_LArm;
    public RectTransform textBG_RArm;

    public AudioSource sound;               // countdown chime

    public int lvlState;                    // 0: intro; 1: countdown; 2: gameplay; 3: game ending; 4: game end

    public float maxGameTime;               // maximum length of the game
    public float gameStartTime;             // game start time

    public string seed;                     // optional manually entered seed
    public System.Random randomSeed;        // seed of the current game

    public string outputPath;               // output path of metric json data

    public ClientServer Client;

    // Must be added to Start() method of inherited classes.
    // Blurs the scene and displays the intro graphic/text.
    public void Setup()
    {
        textBG.SetActive(true);
        countdownText.SetActive(false);
        outroText.SetActive(false);
        ShowInstruction(0);
        instructionCount = 0;
        ChangeBlur(2f);
        lvlState = 0;
        outputPath = "Logs/";
        Client = new ClientServer();

        // TODO: Call standby here
    }

    public void ShowInstruction(int idx)
    {
        for (int i=0; i<instructions.Length; i++) {
            instructions[i].SetActive(i==idx);
        }

        if (idx<instructions.Length) {
            ResizeTextBG(GetRect(instructionParent));
        } else {
            StartLevel();
        }
    }

    // Call this to begin countdown and actual level.
    // Hides intro text and displays countdown text and plays countdown sound.
    public void StartLevel()
    {
        lvlState = 1;
        countdownText.SetActive(true);
        ResizeTextBG(GetRect(countdownText));
        sound.PlayDelayed(0.0f);
        StartCoroutine(CountDown());
        StartCoroutine(Client.UpdateServerGameStarted(maxGameTime));
    }

    // Call this to end level
    public void EndLevel(float delay)
    {
        lvlState = 3;
        StartCoroutine(WaitBeforeShowingOutro(delay)); // delays the end graphic to allow for animations, etc.
    }

    // Displays the countdown before the actual game begins
    IEnumerator CountDown()
    {
        countdownText.GetComponent<TMP_Text>().text = "3";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<TMP_Text>().text = "2";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<TMP_Text>().text = "1";
        yield return new WaitForSeconds(1);
        countdownText.GetComponent<TMP_Text>().text = countDoneText;
        yield return new WaitForSeconds(1);
        lvlState = 2;
        countdownText.SetActive(false);
        textBG.SetActive(false);
        ChangeBlur(10f);
    }

    // Blurs the scene and displays the outro graphic/text
    IEnumerator WaitBeforeShowingOutro(float delay) 
    {
        yield return new WaitForSeconds(delay);
        ChangeBlur(2f);
        textBG.SetActive(true);
        outroText.SetActive(true);
        ResizeTextBG(GetRect(outroText));
        lvlState = 4;
    }

    // Blurs the scene by changing the scene camera's depth of field
    void ChangeBlur(float dist)
    {
        if (postprocess) {
            DepthOfField pr;
            
            if (postprocess.profile.TryGetSettings<DepthOfField>(out pr)){
                pr.focusDistance.value = dist;
            }
         }
    }

    // Returns a text object's bounding box
    public Rect GetRect(GameObject obj) 
    {
        return obj.GetComponent<RectTransform>().rect;
    }

    // Resizes the red background according to text's bounding box
    public void ResizeTextBG(Rect box) 
    {
        float w = box.width+40;
        float h = box.height+40;
        textBG.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
        textBG_Main.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, w);
        textBG_Main.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, h);
        textBG_Edge.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0f, w*1.04255f);
        textBG_Edge.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, h*1.04255f);
        textBG_LArm.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, h*-0.135f, h*0.244082f);
        textBG_LArm.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, h*0.55f);
        textBG_RArm.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, h*-0.05f, h*0.244082f);
        textBG_RArm.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, h*0.55f);
    }

    public IEnumerator Post(string fn, string data)
    {
        return Client.PostGameEnd(fn, data);
    }

    public int Default(int val, string log)
    {
        Debug.LogFormat("Missing or invalid value for {0}, using {1}", log, val.ToString());
        return val;
    }

    public float Default(float val, string log)
    {
        Debug.LogFormat("Missing or invalid value for {0}, using {1}", log, val.ToString());
        return val;
    }

    public bool Default(bool val, string log)
    {
        Debug.LogFormat("Missing or invalid value for {0}, using {1}", log, val.ToString());
        return val;
    }

    public KeyCode Default(KeyCode val, string log)
    {
        Debug.LogFormat("Missing or invalid value for {0}, using {1}", log, val.ToString());
        return val;
    }
}
