using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public abstract class LevelManager : MonoBehaviour
{
    public PostProcessVolume postprocess;
    public Text intro;
    public Text outro;
    public Text countDownText;

    public float countDown;
    public string countDown0Text = "Start!";
    public int lvlState; // 0: intro screne; 1: countdown; 2: gameplay; 3: game end

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
        CountDown();
    }

    // must be added to Start() method of subclasses
    public void Setup()
    {
        lvlState = 0;
        countDown = 5f;
        ChangeBlur(2f);
        intro.enabled = true;
        countDownText.enabled = false;
        outro.enabled = false;
    }

    public void StartLevel()
    {
        lvlState = 1;
        countDown = 4f;
        intro.enabled = false;
        countDownText.enabled = true;
    }

    public void EndLevel()
    {
        lvlState = 3;
        Invoke("PauseAndEnd", 5);
    }

    void CountDown()
    {
        if (lvlState==1 && countDown<=4) {
            if (countDown<=0) {
                lvlState = 2;
                countDownText.enabled = false;
                ChangeBlur(10f);
            } else if (countDown<=1) {
                countDown -= Time.deltaTime;
                countDownText.text = countDown0Text;
            } else if (countDown<=4) {
                countDown -= Time.deltaTime;
                countDownText.text = Mathf.FloorToInt(countDown).ToString();
            }
        }
    }

    void PauseAndEnd() {
        ChangeBlur(2f);
        outro.enabled = true;
    }

    void ChangeBlur(float dist)
    {
        if (postprocess) {
            DepthOfField pr;
            
            if (postprocess.profile.TryGetSettings<DepthOfField>(out pr)){
                pr.focusDistance.value = dist;
            }
         }
    }
}
