using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnectionToInternet : MonoBehaviour
{

    public GameObject m_ErrorConnectionMessage;

    void Start()
    {
        CheckInternetConnection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckInternetConnection()//comprueba si hay conexion a internet
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            m_ErrorConnectionMessage.SetActive(true);
            Debug.Log("Error. Please Check your internet connection!");
        }
        else
        {
            Debug.Log("Connected to Network");
        }
    }

}
