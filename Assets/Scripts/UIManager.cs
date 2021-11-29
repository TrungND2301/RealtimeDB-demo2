using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void GetJSON(string path, string objectName, string callback, string fallback);

    public static UIManager instance;
    [SerializeField] GameObject loginUI;
    [SerializeField] GameObject logoutUI;
    [SerializeField] Text userData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        };
    }

    void Start()
    {
        Debug.Log(gameObject.name);
        GetJSON("Leaders", gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }

    void Update()
    {

    }
    // realtimedb-demo-a2e19
    // https://realtimedb-demo-a2e19.firebaseio.com/
    // us-central1
    // https://realtimedb-demo-a2e19-default-rtdb.firebaseio.com/

    public void LoginScreen()
    {
        loginUI.SetActive(true);
        logoutUI.SetActive(false);
    }

    public void LogoutScreen()
    {
        loginUI.SetActive(false);
        logoutUI.SetActive(true);
    }

    void OnRequestSuccess(string data)
    {
        userData.text = data;
    }

    void OnRequestFailed(string error)
    {
        userData.color = Color.red;
        userData.text = error;
    }
}
