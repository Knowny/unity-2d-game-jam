using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portal;

    private GameObject player1;
    private GameObject player2;
    
    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player1"){
            player1.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
        }

        if(collision.tag == "Player2"){
            player2.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
        }
    }
}
