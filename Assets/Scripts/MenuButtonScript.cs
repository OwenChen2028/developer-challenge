using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    public string type;
    private bool mouseOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && mouseOver)
        {
            if (type == "start")
            {
                SceneManager.LoadScene("Main");
            }
            else if (type == "quit")
            {
                Application.Quit();
            }
        }
    }

    void OnMouseEnter()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }
}
