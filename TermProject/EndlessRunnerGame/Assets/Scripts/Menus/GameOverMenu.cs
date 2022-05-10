using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject mainCamera;
    public GameObject eventSystemTemplate;
    GameObject targetCamera;
    bool escapePressedLastFrame = false;

    void Start()
    {
        targetCamera = mainCamera;
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }

    void Update()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCamera.transform.position, Time.deltaTime * 10);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetCamera.transform.rotation, Time.deltaTime * 10);
    }

    public void OnMainMenuPressed()
    {
        AkSoundEngine.PostEvent("Play_SFX_Menu_Confirm", this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitPressed()
    {
        AkSoundEngine.PostEvent("Play_SFX_Menu_Confirm", this.gameObject);
        Application.Quit();
    }
}
