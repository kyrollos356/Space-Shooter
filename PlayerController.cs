using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float firingRate = 0.2f;
        public float projectileSpeed = 10f;
        float xminimum;
        float xmaximum;
        public float speed = 0.01f;
        public GameObject laser;
    public float health1 = 500f;

    public AudioClip ProjSound;
    public AudioClip DestSound;

    // Use this for initialization
    void Start() {
        
        //putting range to movement
        float zdistance = this.transform.position.z - Camera.main.transform.position.z;
        Vector3 maxleft= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zdistance));
        Vector3 maxright = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zdistance));
         xminimum = maxleft.x;
         xmaximum = maxright.x;
    }
    
    // firing

    void fire() {
        Vector3 offset = new Vector3(0 , 5f , 0);
        GameObject laserline = Instantiate(laser, transform.position+offset, Quaternion.identity) as GameObject;
        laserline.transform.parent = transform.parent;
        laserline.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        GameObject destroyed = laserline;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // transform.position += new Vector3(-speed*Time.deltaTime, 0f, 0f);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // transform.position += new Vector3(speed*Time.deltaTime, 0f, 0f);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            // transform.position += new Vector3(speed*Time.deltaTime, 0f, 0f);
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // transform.position += new Vector3(speed*Time.deltaTime, 0f, 0f);
            transform.position += Vector3.down * speed * Time.deltaTime;

        }
        float rangey = Mathf.Clamp(transform.position.y, -5f, 5f);
        float range = Mathf.Clamp(transform.position.x, xminimum, xmaximum);
        transform.position = new Vector3(range, rangey, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(ProjSound , transform.position);
            InvokeRepeating("fire" ,0.000001f , firingRate ); 
        } if (Input.GetKeyUp(KeyCode.Space)) CancelInvoke("fire");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health1 -= missile.GetDamage();
            if (health1 <= 0) { Destroy(gameObject);AudioSource.PlayClipAtPoint(DestSound, transform.position); }
            Destroy(collision.gameObject);
        }
    }
}
