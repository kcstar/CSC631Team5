using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EscapeMenu : MonoBehaviour
{
    bool menuVisible = false;
    bool escapePressedLastFrame = false;
    Canvas menu;
    public GameObject eventSystemTemplate;
    AsyncOperation sceneLoader;

    void Start() {
        menu = gameObject.GetComponent<Canvas>();
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
        sceneLoader = SceneManager.LoadSceneAsync("MainMenu");
        sceneLoader.allowSceneActivation = false;
    }

    void FixedUpdate() {
        bool escapePressed = Input.GetKey(KeyCode.Escape);
        if (escapePressed && !escapePressedLastFrame) {
            ToggleMenu();
        }
        escapePressedLastFrame = escapePressed;
    }

    void ShowMenu() {
        menuVisible = true;
        menu.enabled = menuVisible;
    }

    void HideMenu() {
        menuVisible = false;
        menu.enabled = menuVisible;
    }

    void ToggleMenu() {
        menuVisible = !menuVisible;
        menu.enabled = menuVisible;
    }

    public void OnResumePressed() {
        HideMenu();
    }
    
    public void OnLeavePressed() {
        sceneLoader.allowSceneActivation = true;
    }
}
