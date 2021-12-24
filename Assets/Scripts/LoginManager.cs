using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login(string url, string data, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    private static extern void SendData(string url, string data, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    private static extern void GetData(string url, string token, string objectName, string callback, string fallback);

    public static LoginManager instance;

    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TextMeshProUGUI alertText;
    [SerializeField] LoadingCircle loadingCircle;

    string serverUrl = "http://localhost:3000/";
    string jwtToken;

    class GLCredential
    {
        public string email;
        public string password;
    }

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

    bool IsUserInputValid(string username, string password)
    {
        if (username.Length == 0 || password.Length == 0)
        {
            // Show error text
            alertText.enabled = true;
            alertText.text = "Username and password must not be empty";
            return false;
        }

        return true;
    }

    public void OnLoginClicked()
    {
        if (IsUserInputValid(usernameInput.text, passwordInput.text))
        {
            // Authenticate user account on server
            StartCoroutine(AuthenticateUserAccount(usernameInput.text, passwordInput.text));
            loadingCircle.gameObject.SetActive(true);
        }
    }

    IEnumerator AuthenticateUserAccount(string username, string password)
    {
        GLCredential credential = new GLCredential();
        credential.email = username;
        credential.password = password;

        string data = JsonUtility.ToJson(credential);
        // Using javascript
        Login(serverUrl + "api/login", data, gameObject.name, "OnLoginSuccess", "OnLoginFailed");
        yield return 1;

        // Using UnityWebRequest
        // UnityWebRequest www = UnityWebRequest.Get(serverUrl);
        // yield return www.SendWebRequest();
        // Debug.Log("Trung2");
        // if (www.result != UnityWebRequest.Result.Success)
        // {
        //     Debug.Log(www.error);
        // }
        // else
        // {
        //     Debug.Log(www.downloadHandler.text);
        // }
    }

    void OnLoginSuccess(string data)
    {
        jwtToken = data;
        alertText.enabled = true;
        alertText.text = data;
    }

    void OnLoginFailed(string data)
    {
        alertText.enabled = true;
        alertText.text = data;
    }

    void OnRequestSuccess(string data)
    {
        loadingCircle.gameObject.SetActive(false);
        Debug.Log(data);
    }

    void OnRequestFailed(string data)
    {
        Debug.Log(data);
    }

    public void GetAllCustomer()
    {
        GetData(serverUrl + "api/customers", jwtToken, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
    }
}
