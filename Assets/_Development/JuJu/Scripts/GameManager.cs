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

    public Animator transition;

    private void Awake() {
        if(instance == null){
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else{Destroy(gameObject);}
    }


public void EscapePress() { 
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
    PlayerController.Instance.PrevState();
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
    yield return new WaitForSeconds(1);
    SceneManager.LoadScene(levelIndex);
}
#endregion

#region GameOver

public void GameOver(){
    SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
}

#endregion
}




}
