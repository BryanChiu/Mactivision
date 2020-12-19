using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public abstract class LevelManager : MonoBehaviour
{
    public PostProcessVolume postprocess; // graphical effects, used for blurring the scene
    public TMP_Text introText; // text object displayed before actual game begins
    public TMP_Text outroText; // text object displayed after game ends
    public TMP_Text countdownText; // text object displaying countdown to begin actual game

    public GameObject textBG; // parent group for graphics displayed behind text
    public RectTransform textBG_Main;
    public RectTransform textBG_Edge;
    public RectTransform textBG_LArm;
    public RectTransform textBG_RArm;

    public float countDown;
    public string countDoneText = "Start!";
    public int lvlState; // 0: intro; 1: countdown; 2: gameplay; 3: game end

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //LvlMngr();
    }

    // must be added to Update() method of subclasses
    public void LvlMngr()
    {
        if (lvlState==1) CountDown();
    }

    // must be added to Start() method of subclasses
    // blurs the scene and displays the intro graphic/text
    public void Setup()
    {
        lvlState = 0;
        countDown = 5f;
        ChangeBlur(2f);
        textBG.SetActive(true);
        introText.enabled = true;
        countdownText.enabled = false;
        outroText.enabled = false;
        ResizeTextBG(GetRect(introText));
        sound = gameObject.GetComponent<AudioSource>();
    }

    // call this to begin countdown and actual level
    // hides intro text and displays countdown text and plays countdown sound
    public void StartLevel()
    {
        lvlState = 1;
        countDown = 4f;
        introText.enabled = false;
        countdownText.enabled = true;
        ResizeTextBG(GetRect(countdownText));
        sound.PlayDelayed(0.0f);
    }

    // call this to end level
    public void EndLevel(float delay)
    {
        lvlState = 3;
        Invoke("PauseAndEnd", delay); // delays the end graphic to allow for animations, etc.
    }

    // displays the countdown before the actual game begins
    void CountDown()
    {
        if (countDown<=0) { // after 0, we hide the countdown graphic/text and unblur the scene
            lvlState = 2;
            countdownText.enabled = false;
            textBG.SetActive(false);
            ChangeBlur(10f);
        } else if (countDown<=1) { // after 1, instead of 0, we display "Start!" (can be customized)
            countDown -= Time.deltaTime;
            countdownText.text = countDoneText;
        } else if (countDown<=4) {
            countDown -= Time.deltaTime;
            countdownText.text = Mathf.FloorToInt(countDown).ToString();
        }
    }

    // blurs the scene and displays the outro graphic/text
    void PauseAndEnd() 
    {
        ChangeBlur(2f);
        textBG.SetActive(true);
        outroText.enabled = true;
        ResizeTextBG(GetRect(outroText));
    }

    // blurs the scene by changing the scene camera's depth of field
    void ChangeBlur(float dist)
    {
        if (postprocess) {
            DepthOfField pr;
            
            if (postprocess.profile.TryGetSettings<DepthOfField>(out pr)){
                pr.focusDistance.value = dist;
            }
         }
    }

    // returns a text object's bounding box
    Rect GetRect(TMP_Text text) 
    {
        return text.gameObject.GetComponent<RectTransform>().rect;
    }

    // resizes the red background according to text's bounding box
    void ResizeTextBG(Rect box) 
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
