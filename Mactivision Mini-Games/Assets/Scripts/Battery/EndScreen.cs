using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Battery.Instance.EndBattery();
        StartCoroutine(Post("BatteryConfig.json", Battery.Instance.SerializedConfig()));
    }

    public IEnumerator Post(string filename, string data)
    {
        var post = new UnityWebRequest ("http://127.0.0.1:8000/output?filename=" + filename + "&token=" + Battery.Instance.GetToken(), "POST");
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        post.uploadHandler = new UploadHandlerRaw(bytes);
        post.downloadHandler = new DownloadHandlerBuffer();
        post.SetRequestHeader("Content-Type", "application/json");
        yield return post.SendWebRequest();

        if (post.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Network Error\n" + post.error);
        }
        else
        {
            Debug.Log("Post Success!");
        }
    }
}
