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
    public const string identityPool = "us-east-1:4011e768-3981-4d25-8923-1a71fb845818";
    public static string userPoolId = "us-east-1_9BC3bZAxJ";
    public static string appClientId = "12qdu1o1f2k1f8u584o2nldppv";

    // Initialize the Amazon Cognito credentials provider
    public static CognitoAWSCredentials credentials = new CognitoAWSCredentials(
        identityPool, region
    );

    // User's Cognito ID once logged in becomes set here
    public static string userid = "";

}
