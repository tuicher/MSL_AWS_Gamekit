using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenuManager : MonoBehaviour
{
    // MenuManager singleton
    public static LoginMenuManager Instance = null;

    // Panels to apply in editor
    [SerializeField] private GameObject LoginPanel;
    //[SerializeField] private GameObject SignupPanel;

    // UI Buttons & Input Fields
    //public GameObject BankButtonGameObject;
    public Button LoginButton;
    public Toggle StayLoggedToggle;

    public TMPro.TMP_InputField UsernameTextField;
    public TMPro.TMP_InputField PasswordTextField;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this.gameObject.GetComponent<LoginMenuManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Events.Call_App_Load();

        LoginButton.onClick.AddListener(Login_AWS_Procedure);
    }

    // Functions that set panels active and inactive
    public void Close_Login_Panel()
    {
        LoginPanel.SetActive(false);
    }

    public void Load_Login_Panel()
    {
        LoginPanel.SetActive(true);
    }

    public void Login_AWS_Procedure()
    {
        Cognito.Login(UsernameTextField.text, PasswordTextField.text);
    }
}