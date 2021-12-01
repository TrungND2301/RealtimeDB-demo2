using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    [SerializeField] InputField userEmail;
    [SerializeField] InputField userPassword;

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
        LoginScreen();
    }

    void Update()
    {

    }

    public void Login()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection(userEmail.text + "&" + userData.text));

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:3000/", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

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
