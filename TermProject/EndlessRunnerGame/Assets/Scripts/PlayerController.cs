using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public float maxSpeed;
    private float speed;

    public void SetSpeed(float speed) {
        this.speed = speed;
        anim.SetFloat("Horizontal", speed / maxSpeed);
    }

    public float GetSpeed() {
        return speed;
    }
}
