using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace JuJu {

public class PauseMenuScript : MonoBehaviour
{
///<summary>
///you can press Escape to pause the game. When the game is paused, you can press Escape to resume. Restart restarts the level and Quit sends you to the main menu.
///</summary>


    [SerializeField]
    GameObject PauseMenu;

    static bool gameIsPaused;

    //method for the Input System
    public void InputEscape(InputAction.CallbackContext context){
        if(context.performed){

        if(gameIsPaused != true){
            Pause();
        }
        else{Resume();}
        }
    }

    public void Pause(){
        gameIsPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume(){
        gameIsPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //sends you back to the main menu
    public void Quit(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}

}
