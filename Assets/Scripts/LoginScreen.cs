using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{
    [SerializeField] Button loginButton;
    [SerializeField] Text loginText;
    [SerializeField] Text passwordText;

    private void Awake()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }
    void OnLoginButtonClick()
    {
        NetworkManager.instance.ConnectToServer(loginText.text, passwordText.text);
    }
}
