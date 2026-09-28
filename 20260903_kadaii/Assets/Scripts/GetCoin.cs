using System.Runtime.InteropServices;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void SendMessageToJS(string message);
    #endif
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
            #if UNITY_WEBGL && !UNITY_EDITOR
            SendMessageToJS("Coin");
            #endif
        }
    }
}