using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject mainMenu;
    public GameObject mainCamera;
    public GameObject menuCamera;
    public GameObject playCamera;
    GameObject targetCamera;
    AsyncOperation sceneLoader;
    NetworkConfiguration networkConfig;
    bool escapePressedLastFrame = false;

    void Start() {
        targetCamera = menuCamera;
        sceneLoader = SceneManager.LoadSceneAsync("CityRunner");
        sceneLoader.allowSceneActivation = false;
        networkConfig = GameObject.Find("Network Configuration").GetComponent<NetworkConfiguration>();
    }

    void FixedUpdate() {
        bool escapePressed = Input.GetKey(KeyCode.Escape);
        if (escapePressed && !escapePressedLastFrame) {
            if (playMenu.activeSelf) {
                OnBackPressed();
            }
        }
        escapePressedLastFrame = escapePressed;
    }

    void Update() {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCamera.transform.position, Time.deltaTime * 10);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetCamera.transform.rotation, Time.deltaTime * 10);
    }

    public void OnPlayPressed() {
        TweenTo(playCamera);
        playMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void OnExitPressed() {
        Application.Quit();
    }

    public void OnBackPressed() {
        TweenTo(menuCamera);
        playMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void OnLocalPressed() {
        networkConfig.hostIP = Constants.LOCAL_HOST;
        networkConfig.hostPort = Constants.REMOTE_PORT;
        sceneLoader.allowSceneActivation = true;
    }
    public void OnOnlinePressed() {
        networkConfig.hostIP = Constants.REMOTE_HOST;
        networkConfig.hostPort = Constants.REMOTE_PORT;
        sceneLoader.allowSceneActivation = true;
    }

    public void TweenTo(GameObject target) {
        targetCamera = target;
    }

}
