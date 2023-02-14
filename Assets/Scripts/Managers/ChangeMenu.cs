using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject registerMenu;
    public GameObject loginMenu;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoToRegisterUser()
    {
        registerMenu.SetActive(true);
        loginMenu.SetActive(false);
    }

    public void GoToSignIn()
    {
        loginMenu.SetActive(true);
        registerMenu.SetActive(false);
    }
}
