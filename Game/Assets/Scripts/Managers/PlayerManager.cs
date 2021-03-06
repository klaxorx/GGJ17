﻿using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {

    public Room currentRoom { get; private set; }
    public Animator animator;
    public Rigidbody rb;
    public Vector3 moveToObject;

    bool echo
    {
        get { return Input.GetKeyDown("e"); }
    }
    bool foxtrot
    {
        get { return Input.GetKey("f"); }
    }
    bool left
    {
        get { return Input.GetKey("a"); }
    }
    bool right
    {
        get { return Input.GetKey("d"); }
    }
    bool up
    {
        get { return Input.GetKey("w"); }
    }
    bool down
    {
        get { return Input.GetKey("s"); }
    }

    bool sprint
    {
        get { return Input.GetKey("left shift"); }
    }
    bool cooldown;
    public bool inObject;
    public bool action;
    public bool fire;
    public bool alive;
    float speed;
    float baseSpeed;
    float sprintMax;
    float sprintCurrent;
    float sprintingSpeed;
    Vector3 move;
    int x;
    int z;
    
	// Use this for initialization
	void Start () {
        baseSpeed = 2.5f;
        speed = baseSpeed;
        sprintingSpeed = 3.5f;
        sprintMax = 2.0f;
        sprintCurrent = sprintMax;
        move = new Vector3(0, 0);
        x = 0;
        z = 0;
        action = false;
        fire = false;
        inObject = false;
        alive = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            if (echo)
            {
                action = true;
            }
            else
            {
                action = false;
            }
            //Debug.Log(action);
            if (foxtrot)
            {
                fire = true;
            }
            else
            {
                fire = false;
            }
            // movement controlls
            if (left)
            {
                x = -1;
                animator.SetInteger("direction", -90);
            }
            else if (right)
            {
                x = 1;
                animator.SetInteger("direction", 90);
            }
            else
            {
                x = 0;
            }

            if (up)
            {
                z = 1;
            }
            else if (down)
            {
                z = -1;
            }
            else
            {
                z = 0;
            }

            // apply sprint speed if enoght sprint energy (sprintCurrent) is left
            if (sprint && sprintCurrent > 0.0f && !cooldown)
            {
                speed = sprintingSpeed;
                sprintCurrent -= 1.5f * Time.deltaTime;
            }
            else
            {
                speed = baseSpeed;
                if (sprintCurrent < sprintMax)
                {
                    sprintCurrent += 0.25f * Time.deltaTime;
                }
            }

            if (cooldown && sprintCurrent >= 0.5f)
            {
                cooldown = false;
            }

            if (sprintCurrent >= sprintMax)
            {
                sprintCurrent = sprintMax;
            }
            else if (sprintCurrent < 0.0f)
            {
                sprintCurrent = 0.0f;
                cooldown = true;
            }

            // applys movement to player
            move = new Vector3(x, 0, z);
            if (!inObject)
            {
                rb.velocity = new Vector3(move.x, move.y, move.z) * speed;
            }
            else
            {
                rb.position = moveToObject;
            }
        }
    }

    //What happens when the Player is hit by Swat
    public void OnDeath(Transform captor)
    {
        alive = false;
        animator.SetBool("alive", false);
        GetComponentInChildren<Collider>().enabled = false;
        rb.transform.SetParent(captor);
        rb.transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        rb.isKinematic = true;
    }
}
