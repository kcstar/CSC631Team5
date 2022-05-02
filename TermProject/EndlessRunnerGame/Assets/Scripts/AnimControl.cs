using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            AkSoundEngine.PostEvent("Play_SFX_Jump", this.gameObject);
            Player.GetComponent<Animator>().Play("6");
        }
    }
}
