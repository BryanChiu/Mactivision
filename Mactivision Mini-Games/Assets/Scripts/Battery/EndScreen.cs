using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EndScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Battery.Instance.EndBattery();
        StartCoroutine(Post("BatteyConfig.json", Battery.Instance.SerializedConfig()));
    }

    IEnumerator Post(string filename, string data)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        byte[] test = Encoding.UTF8.GetBytes(data);
        form.Add(new MultipartFormFileSection("file", test, filename, "text/plain"));

        using (var www = UnityWebRequest.Post("http://127.0.0.1:8000/post", form))
        {
            yield return www.SendWebRequest();
    
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Post Successful.");
            }
        }
    }
}
