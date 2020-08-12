using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class PlayerController2 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rig;
    private AudioSource audioSource;
    

    void Awake() {
        // Get the rigidbody component
        rig = GetComponent<Rigidbody>(); //To get the component of the rigidbody
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1.0f;
    }

    void Update() {
        
        Move(); //Update each frame with move method.

        if(Input.GetButtonDown("Jump")) { //Checking if the Jump button is pressed
            TryJump(); //Calling out the TryJump method
        }

        if(GameManager.instance.paused) 
            return;
    }

    void Move() {
        //Getting our inputs
        float xInput = Input.GetAxis("Horizontal"); //Getting the horizontal input
        float zInput = Input.GetAxis("Vertical");   //Getting the vertical output

       Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed; //To apply the velocity of the movement
       dir.y = rig.velocity.y;

       rig.velocity = dir; //To assign the velocity to Rigidbody's velocity

       Vector3 facingDir = new Vector3(xInput, 0, zInput) * moveSpeed; //To apply the rotation of the movement.

       if(facingDir.magnitude > 0) { //To make the change more smooth.
            transform.forward = facingDir; //To change the transformation of the facingDir.
       }
    }

    void TryJump() { //Initiating the TryJump method.
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down); //Initiating the new ray class
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down); //Initiating the new ray class
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down); //Initiating the new ray class
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down); //Initiating the new ray class

        bool cast1 = Physics.Raycast(ray1, 0.7f);       // Checking whether the ray is below 0.7f
        bool cast2 = Physics.Raycast(ray2, 0.7f);       // Checking whether the ray is below 0.7f
        bool cast3 = Physics.Raycast(ray3, 0.7f);       // Checking whether the ray is below 0.7f
        bool cast4 = Physics.Raycast(ray4, 0.7f);       // Checking whether the ray is below 0.7f

        if(cast1 || cast2 || cast3 || cast4) {                      
            rig.AddForce(Vector3.up*jumpForce, ForceMode.Impulse); //Add the force in impulse mode so that it jumps.
        }
        
    }

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Enemy")) {
            GameManager2.instance.GameOver();
        }
        if(other.CompareTag("Coin")) {
            GameManager2.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();
        }
        //If we hit a goal
        if(other.CompareTag("Goal")) {
            GameManager2.instance.LevelEnd();   
        }
    }   

}
