using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat : MonoBehaviour
{
    public Transform posA, posB;
    public int Speed;
    Vector3 targetPos;
    

    // Start is called before the first frame update
    public void Start()
    {
        targetPos = posB.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if(Vector2.Distance(transform.position, posA.position)<.1f) targetPos = posB.position;

        if(Vector2.Distance(transform.position, posB.position)<.1f) targetPos = posA.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}