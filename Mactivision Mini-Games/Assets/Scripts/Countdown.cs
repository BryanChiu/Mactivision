using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float CurrentTime = 3f;

    public Text CountdownText;

    void Start()
    {
        CountdownText.text = CurrentTime.ToString("0");
        InvokeRepeating("UpdateCounter", 0f, 1.0f);
    }

    void UpdateCounter()
    {
        if (CurrentTime < 0)
        {
            Battery.Instance.LoadNextScene();
        }
        else 
        {
            CountdownText.text = CurrentTime.ToString("0");
            CurrentTime = CurrentTime - 1;
        }
    }
}
