using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{

    [Header("References")]

    [SerializeField, Tooltip("The canvas that holds the menu.")]
    GameObject m_Menu = default;


    // Start is called before the first frame update
    void Start()
    {
        m_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;
        if(keyboard.escapeKey.isPressed || gamepad.selectButton.isPressed){
            m_Menu.SetActive(true);
        }
        //if(Input.GetButtonDown("MenuPress")){
            //m_Menu.SetActive(true);
        //}
    }
}
