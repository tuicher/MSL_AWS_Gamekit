using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.DynamoDBv2;

public class DynamoDB_Table : MonoBehaviour
{
    void Start()
    {
        AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
        // This client will access the US East 1 region.
        clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
        AmazonDynamoDBClient client = new AmazonDynamoDBClient(clientConfig);
    }
    
    
    //AmazonDynamoDBStreamsClient dynamoDBStreamsClient*/
}