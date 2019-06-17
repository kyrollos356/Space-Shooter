using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    
    public float width = 20f;
    public float hight = 5f;
    public GameObject enemyPrefab;
    public float speed = 3f;
    public float Timebetween = 1f;

    private bool moveright = true;
    private  float xminimum;
    private  float xmaximum;

    void Start()
    {
        float zdistance = this.transform.position.z - Camera.main.transform.position.z;
        Vector3 maxleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zdistance));
        Vector3 maxright = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zdistance));
        xminimum = maxleft.x+0.5f;
        xmaximum = maxright.x-0.5f;   
        Create_Enemies();
    }
    void Update()
    {
        if (moveright)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float RightEdgeOfFormation = transform.position.x + 0.5f * width;
        float LeftEdgeOfFormation = transform.position.x - 0.5f * width;
        if (RightEdgeOfFormation > xmaximum || LeftEdgeOfFormation < xminimum) moveright = !moveright;
        SpawnUntilFull();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, hight, 0));
    }
    //to check if there is one enemy dead "free position"
    Transform NextFreePosition() {
        foreach (Transform childPositionObject in transform)
            if (childPositionObject.childCount == 0)
        {
            return childPositionObject;
        } 
    return null;
}
    // to check that all members are dead
    bool AllMembersAreDead()
    {
        foreach (Transform childPositionObject in transform)
            if (childPositionObject.childCount > 0)
            {
                return false;
            }
        return true;
    }
    // to recreate enemies in positions
    void Create_Enemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
    //to create enemies in every free position
    void SpawnUntilFull() {
        Transform Free_Position = NextFreePosition();
        if(Free_Position)
        {
            GameObject enemy = Instantiate(enemyPrefab, Free_Position.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = Free_Position;
        }
        if (NextFreePosition()) Invoke("SpawnUntilFull" , Timebetween);
    }

}