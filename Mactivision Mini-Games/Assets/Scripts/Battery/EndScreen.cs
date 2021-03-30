using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EndScreen : MonoBehaviour
{
    ClientServer Client;

    void Start()
    {
        Battery.Instance.EndBattery();
        Client = new ClientServer(Battery.Instance.GetServerURL());
        StartCoroutine(Client.PostFinished(Battery.Instance.GetConfigFileName(), Battery.Instance.SerializedConfig()));
    }
}
