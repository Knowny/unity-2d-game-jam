using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public int P1Life;
    public int P2Life;

    public GameObject p1Wins;
    public GameObject p2Wins;

    public GameObject[] p1Hearts;
    public GameObject[] p2Hearts;

    public AudioSource hurtSound;

    public string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(P1Life <= 0){
            player1.SetActive(false);
            p2Wins.SetActive(true);
        }
        
        if(P2Life <= 0){
            player2.SetActive(false);
            p1Wins.SetActive(true);
        }

        //RESTART THE GAME
        if(p1Wins.activeSelf || p2Wins.activeSelf){
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        //EXIT TO MENU
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(mainMenu);
        }

    }

    // -1HP player1 and deactivate if dead
    public void HurtP1(){
        P1Life -= 1;

        for(int i = 0; i < p1Hearts.Length; i++){
            if(P1Life > i){
                p1Hearts[i].SetActive(true);
            } else {
                p1Hearts[i].SetActive(false);
            }
        }
        hurtSound.Play();
    }

    // -1HP player2 and deactivate if dead
    public void HurtP2(){
        P2Life -= 1;

        for(int i = 0; i < p2Hearts.Length; i++){
            if(P2Life > i){
                p2Hearts[i].SetActive(true);
            } else {
                p2Hearts[i].SetActive(false);
            }
        }
        hurtSound.Play();
    }
}