using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginMenuManager : MonoBehaviour
{
    // LoginMenuManager singleton
    public static LoginMenuManager Instance = null;

    // Panels to apply in editor
    [SerializeField] private GameObject LoginPanel;

    // UI Buttons & Input Fields
    [SerializeField] Button LoginButton;
    [SerializeField] Toggle StayLoggedToggle;
    [SerializeField] TMP_InputField UsernameTextField;
    [SerializeField] TMP_InputField PasswordTextField;

    GameObject IndicatorName;
    GameObject IndicatorPass;
    TextMeshProUGUI FeedbackMessage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this.gameObject.GetComponent<LoginMenuManager>();
    }

    void Start()
    {
        // Finding all components
        LoginButton = GameObject.Find("Login Button").GetComponent<Button>();
        StayLoggedToggle = GameObject.Find("Stay logged Toggle").GetComponent<Toggle>();
        UsernameTextField = GameObject.Find("User InputField").GetComponent<TMP_InputField>();
        PasswordTextField = GameObject.Find("Password InputField").GetComponent<TMP_InputField>();
        IndicatorName = GameObject.Find("Red Indicator User");
        IndicatorPass = GameObject.Find("Red Indicator Pass");
        FeedbackMessage = GameObject.Find("Feedback Message").GetComponent<TextMeshProUGUI>();

        // Disabling elements
        IndicatorName.SetActive(false);
        IndicatorPass.SetActive(false);
        FeedbackMessage.gameObject.SetActive(false);

        // Setting Listeners
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

    public void SetIndicators(bool state, string message = "")
    {
        IndicatorName.SetActive(state);
        IndicatorPass.SetActive(state);

        FeedbackMessage.text = message;
        FeedbackMessage.gameObject.SetActive(state);
    }


    public void Login_AWS_Procedure()
    {
        // Check if epmty or null Input Field
        var user = UsernameTextField.text;
        var password = PasswordTextField.text;


        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            Instance.SetIndicators(true, "Introduce your user and password");
        } else
        {
            Cognito.Login(user, password);
        }

    }
}