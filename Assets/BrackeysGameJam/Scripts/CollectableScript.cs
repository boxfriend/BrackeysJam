using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuJu;

public class CollectableScript : MonoBehaviour
{

///<summary>
/// just a quick little script for testing purposes, since I didn't want to mess around with your scripts too much. 
/// increases the player's size when collecting an upgrade and calls the GameOver screen when the velocity is too low. 
///</summary>

[SerializeField] [Tooltip("decides by how much the player is scaled up when picking up an upgrade")]
private float increaseVar;

Rigidbody2D rb;
bool isAlreadyDead = false;

public enum State{
    speedingUp,
    moving,
    dead
}

public static State playerState;

private void Start() {
    rb = GetComponent<Rigidbody2D>();
    playerState = State.speedingUp;
    Debug.Log(playerState);
}

private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Collectable"){
        transform.localScale = transform.localScale * increaseVar;
    }
}

private void FixedUpdate() {
    if(rb.velocity.magnitude > 0.4f){
        playerState = State.moving;
    }

    if(rb.velocity.magnitude <= 0.4f){
        if(playerState == State.moving){
        Debug.Log("lappen");
        if(!isAlreadyDead  ){
            Die();
        }
      }
    }
}

void Die(){
    GameManager.instance.GameOver();
    isAlreadyDead = true;
}

}
