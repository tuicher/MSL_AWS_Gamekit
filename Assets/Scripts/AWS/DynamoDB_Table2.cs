
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

public class DynamoDB_Table2 : MonoBehaviour
{

    private AmazonDynamoDBClient client;
    private Table table;

    void Start()
    {
        AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
        clientConfig.RegionEndpoint = RegionEndpoint.USEast1;
        client = new AmazonDynamoDBClient(clientConfig);
        table = Table.LoadTable(client, "prueba_MSL_Table");
    }

    async void GetItemAsync()
    {
        var request = new GetItemRequest
        {
            TableName = "prueba_MSL_Table",
            Key = new Dictionary<string, AttributeValue> {
                { "email", new AttributeValue { S = "YourPrimaryKeyValue" } }
            }
        };
        var response = await client.GetItemAsync(request);
        if (response.Item != null)
        {
            var document = Document.FromAttributeMap(response.Item);
            // Process the retrieved item here
        }
    }
}