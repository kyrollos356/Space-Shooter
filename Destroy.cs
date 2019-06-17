using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public GameObject EnemyProjectile; 
    public float health1 = 100f;
    public float ShotPerSecond = 0.5f;
    int ScoreValue =1;
    public Score score;
    public AudioClip ProjSound;
    public AudioClip DestSound;


    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();   
    }
    void Update()
    {
        float probability = Time.deltaTime * ShotPerSecond;
        if (Random.value < probability) {
            AudioSource.PlayClipAtPoint(ProjSound , transform.position);
            firing();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health1 -= missile.GetDamage();
            if (health1 <= 0) { Destroy(gameObject); AudioSource.PlayClipAtPoint(DestSound , transform.position); } 
            Destroy(collision.gameObject);
        }
        score.ScoreGained(ScoreValue);
    }
     void firing()
    {   
        GameObject Enemylaser =  Instantiate(EnemyProjectile , transform.position , Quaternion.identity) as GameObject;
        Enemylaser.transform.parent = transform.parent;
        Enemylaser.transform.position = new Vector3(transform.position.x , transform.position.y-1 , 0);
    }
}
