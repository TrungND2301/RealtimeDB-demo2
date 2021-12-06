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

    public static LoginManager instance;

    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TextMeshProUGUI alertText;

    string serverUrl = "http://localhost:3000/";

    class GLCredential
    {
        public string Email;
        public string Password;
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
        }
    }

    IEnumerator AuthenticateUserAccount(string username, string password)
    {
        GLCredential credential = new GLCredential();
        credential.Email = username;
        credential.Password = password;

        string data = JsonUtility.ToJson(credential);
        // Using javascript
        Login(serverUrl + "api/login", data, gameObject.name, "OnRequestSuccess", "OnRequestFailed");
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

    void OnRequestSuccess(string data)
    {
        alertText.enabled = true;
        alertText.text = data;
    }

    void OnRequestFailed(string data)
    {
        alertText.enabled = true;
        alertText.text = data;
    }
}
