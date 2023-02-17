using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.CognitoIdentity;
using Amazon;

public class Cognito1 : MonoBehaviour
{
    // UI Buttons & Input Fields
    public Button LoginButton;
    public Button SignupButton;
    public TMP_InputField EmailField;
    public TMP_InputField SignupPasswordField;
    public TMP_InputField SignupUsernameField;
    public TMP_InputField LoginPasswordField;
    public TMP_InputField LoginUsernameField;

    // Token Holder
    public static string jwt;

    static bool loginSuccessful;

    // Create an Identity Provider
    static AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient
        ( new Amazon.Runtime.AnonymousAWSCredentials(), CredentialsManager.region );

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(Login);
        SignupButton.onClick.AddListener(Signup);

        loginSuccessful = false;
    }

    public void Login()
    {
        _ = Login_User();

        // Load Panels
        MenuManager.Instance.Close_Login_Panel();
        MenuManager.Instance.Load_Recommendations_Panel();

    }

    public static void Login(string user, string pass)
    {
        _ = Login_User(user, pass);

    }

    public void Signup()
    {
        _ = Signup_Method_Async();
    }

    //Method that creates a new Cognito user
    private async Task Signup_Method_Async()
    {
        string userName = SignupUsernameField.text;
        string passWord = SignupPasswordField.text;
        string email = EmailField.text;

        SignUpRequest signUpRequest = new SignUpRequest()
        {
            ClientId = CredentialsManager.appClientId,
            Username = userName,
            Password = passWord
        };

        List<AttributeType> attributes = new List<AttributeType>()
        {
            new AttributeType(){Name = "email", Value = email}
        };

        signUpRequest.UserAttributes = attributes;

        try
        {
            SignUpResponse request = await provider.SignUpAsync(signUpRequest);
            Debug.Log("Sign up worked");

            // Send Login Event
            Events.Call_Signup();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
            return;
        }
    }

    //Method that signs in Cognito user 
    private async Task Login_User()
    {
        string userName = LoginUsernameField.text;
        string passWord = LoginPasswordField.text;

        CognitoUserPool userPool = new CognitoUserPool(CredentialsManager.userPoolId, CredentialsManager.appClientId, provider);

        CognitoUser user = new CognitoUser(userName, CredentialsManager.appClientId, userPool, provider);

        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = passWord
        };

        try
        {
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

            GetUserRequest getUserRequest = new GetUserRequest();
            getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;

            Debug.Log("User Access Token: " + getUserRequest.AccessToken);
            jwt = getUserRequest.AccessToken;

            // User is logged in
            loginSuccessful = true;

        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e);
            return;
        }

        if (loginSuccessful == true) {

            string subId = await Get_User_Id();
            CredentialsManager.userid = subId;

            // Send Login Event
            Events.Call_Login();

            // Print UserID
            Debug.Log("Response - User's Sub ID from Cognito: " + CredentialsManager.userid);

        }
    }

    private static async Task Login_User( string username, string pass)
    {

        CognitoUserPool userPool = new CognitoUserPool(CredentialsManager.userPoolId, CredentialsManager.appClientId, provider);

        CognitoUser user = new CognitoUser(username, CredentialsManager.appClientId, userPool, provider);

        InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
        {
            Password = pass
        };

        try
        {
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

            GetUserRequest getUserRequest = new GetUserRequest();
            getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;

            Debug.Log("User Access Token: " + getUserRequest.AccessToken);
            jwt = getUserRequest.AccessToken;

            // User is logged in
            loginSuccessful = true;

        }
        catch (NotAuthorizedException invalidPass)
        {
            Debug.Log("Wrong username or pass: " + invalidPass.ErrorCode);
            LoginMenuManager.Instance.SetIndicators(true, "Wrong username or pass");
            return;
        }
        catch (UserNotConfirmedException unconfirmed)
        {
            Debug.Log("Unconfirmed: " + unconfirmed.ErrorCode);
            LoginMenuManager.Instance.SetIndicators(true, "Check your email to corfirm your account");
            return;
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.GetType());

            return;
        }

        if (loginSuccessful == true)
        {

            string subId = await Get_User_Id();
            CredentialsManager.userid = subId;

            // Send Login Event
            Events.Call_Login();

            // Print UserID
            Debug.Log("Response - User's Sub ID from Cognito: " + CredentialsManager.userid);

        }
    }

    // Gets a User's sub UUID from Cognito
    private static async Task<string> Get_User_Id()
    {
        Debug.Log("Getting user's id...");

        string subId = "";

        Task<GetUserResponse> responseTask =
            provider.GetUserAsync(new GetUserRequest
            {
                AccessToken = jwt
            });

        GetUserResponse responseObject = await responseTask;

        // Set User ID
        foreach (var attribute in responseObject.UserAttributes)
        {
            if (attribute.Name == "sub")
            {
                subId = attribute.Value;
                break;
            }
        }

        return subId;
    }
}
