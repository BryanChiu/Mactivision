using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Battery.Instance.EndBattery();        
    }
}
