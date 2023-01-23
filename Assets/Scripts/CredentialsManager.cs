using System;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;


public class CredentialsManager
{

    // Region - A game may need multiple region endpoints if services
    // are in multiple regions or different per service
    public static RegionEndpoint region = RegionEndpoint.USEast1; //change this if you are in a different region

    // Cognito Credentials Variables
    public const string identityPool = "us-east-1:8548e239-9b43-4b9c-a596-f2ed5cfe6e1a";
    public static string userPoolId = "us-east-1_HDVt6k38S"; 
    public static string appClientId = "hcon738u70hmgd92eeminc4mt";

    // Initialize the Amazon Cognito credentials provider
    public static CognitoAWSCredentials credentials = new CognitoAWSCredentials(
        identityPool, region
    );

    // User's Cognito ID once logged in becomes set here
    public static string userid = "";

}
