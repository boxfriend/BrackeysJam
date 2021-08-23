using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Boxfriend.Player;

namespace JuJu {

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool gameIsPaused = false;

    private void Awake() {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }


public void EscapePress(InputAction.CallbackContext context) {
    if(context.performed)
        if(!gameIsPaused){
            Pause();
        }
        else{Resume();}
    
}
public void Pause(){
    gameIsPaused = true;
    Time.timeScale = 0;
    SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
}

#region PauseMenu

public void Resume(){
    gameIsPaused = false;
    Time.timeScale = 1;
    SceneManager.UnloadSceneAsync("PauseMenu");
}

public void Restart(){
    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

public void BackToMainMenu(){
    Time.timeScale = 1;
    SceneManager.LoadScene("MainMenu");
}
#endregion

#region MainMenu

public void PlayLevel1(){
    StartCoroutine(LoadLevel(1));
}

public IEnumerator LoadLevel(int levelIndex){
    yield return new WaitForSeconds(0.3f);
    SceneManager.LoadScene(levelIndex);
}

public void Quit(){
    Application.Quit();
}
}

#endregion

}
