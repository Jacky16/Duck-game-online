using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginScreen : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] Button loginButton;
    [SerializeField] Text loginText;
    [SerializeField] Text passwordText;
    
    [Header("Register")]
    [SerializeField] Button registerButton;
    [SerializeField] Button backButton;
    [SerializeField] Text emailRegisterText;
    [SerializeField] Text nickRegisterText;
    [SerializeField] Text registerPasswordText;

    [SerializeField]GameObject canvasRegister;

    private void Awake()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    

    private void OnRegisterButtonClick()
    {
        if ((emailRegisterText.text != "" && emailRegisterText.text.Contains("@"))
            && nickRegisterText.text != "" && 
            registerPasswordText.text != "")
        {
            NetworkManager.instance.Register(nickRegisterText.text, registerPasswordText.text, emailRegisterText.text);

            DeleteAllFields();

            canvasRegister.SetActive(false);
        }
    }
    
    void OnLoginButtonClick()
    {
        if (loginText.text != "" && passwordText.text != "")
        {
            NetworkManager.instance.LogIn(loginText.text, passwordText.text);
        }
    }
    private void OnBackButtonClick()
    {
        DeleteAllFields();
        canvasRegister.SetActive(false);
    }
    void DeleteAllFields()
    {
        emailRegisterText.text = "";
        nickRegisterText.text = "";
        registerPasswordText.text = "";
    }

   
}
