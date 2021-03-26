using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EndScreen : MonoBehaviour
{
    ClientServer Client;

    // Start is called before the first frame update
    void Start()
    {
        Battery.Instance.EndBattery();
        Client = new ClientServer();
        StartCoroutine(Client.PostFinished("BatteryConfig.json", Battery.Instance.SerializedConfig()));
    }
}
