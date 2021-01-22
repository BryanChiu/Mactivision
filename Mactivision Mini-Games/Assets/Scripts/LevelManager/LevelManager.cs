using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

// This class provides methods to inherited classes for pre and post-game features. It starts the
// scene with a blurred game scene with an introductory text, then a countdown, and a end game text.
public abstract class LevelManager : MonoBehaviour
{
    public PostProcessVolume postprocess;   // graphical effects, used for blurring the scene
    public TMP_Text introText;              // text object displayed before actual game begins
    public TMP_Text outroText;              // text object displayed after game ends
    public TMP_Text countdownText;          // text object displaying countdown to begin actual game
    public string countDoneText = "Start!"; // text instead of "0" after "3, 2, 1"

    public GameObject textBG;               // parent group for graphics displayed behind text
    public RectTransform textBG_Main;
    public RectTransform textBG_Edge;
    public RectTransform textBG_LArm;
    public RectTransform textBG_RArm;

    public AudioSource sound;               // countdown chime

    public int lvlState;                    // 0: intro; 1: countdown; 2: gameplay; 3: game ending; 4: game end

    public string outputPath;               // output path of metric json data

    // Must be added to Start() method of inherited classes.
    // Blurs the scene and displays the intro graphic/text.
    public void Setup()
    {
        textBG.SetActive(true);
        introText.enabled = true;
        countdownText.enabled = false;
        outroText.enabled = false;
        ResizeTextBG(GetRect(introText));
        ChangeBlur(2f);
        sound = gameObject.GetComponent<AudioSource>();
        lvlState = 0;
        outputPath = "Logs/";
    }

    // Call this to begin countdown and actual level.
    // Hides intro text and displays countdown text and plays countdown sound.
    public void StartLevel()
    {
        lvlState = 1;
        introText.enabled = false;
        countdownText.enabled = true;
        ResizeTextBG(GetRect(countdownText));
        sound.PlayDelayed(0.0f);
        StartCoroutine(CountDown());
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
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = countDoneText;
        yield return new WaitForSeconds(1);
        lvlState = 2;
        countdownText.enabled = false;
        textBG.SetActive(false);
        ChangeBlur(10f);
    }

    // Blurs the scene and displays the outro graphic/text
    IEnumerator WaitBeforeShowingOutro(float delay) 
    {
        yield return new WaitForSeconds(delay);
        ChangeBlur(2f);
        textBG.SetActive(true);
        outroText.enabled = true;
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
    public Rect GetRect(TMP_Text text) 
    {
        return text.gameObject.GetComponent<RectTransform>().rect;
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
}
