using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using Boxfriend.Player;

namespace JuJu {

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool gameIsPaused = false;

        [SerializeField]
        private AudioMixer _audioMixer;

    private void Awake() {
        if(instance == null){
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else{Destroy(gameObject);}
    }

        private void Start()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityLevel", 1));

            _audioMixer.SetFloat("MasterVol", AudioLevel(PlayerPrefs.GetFloat("MasterVolume", 0.75f)));
            _audioMixer.SetFloat("MusicVol", AudioLevel(PlayerPrefs.GetFloat("MusicVolume", 0.75f)));
            _audioMixer.SetFloat("EffectVol", AudioLevel(PlayerPrefs.GetFloat("EffectVolume", 0.75f)));
        }

        
        /// <returns>Slider value converted to Decibels</returns>
        private float AudioLevel(float v)
        {
            return Mathf.Log10(v) * 20;
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
    if(!SceneManager.GetSceneByName("Settings").isLoaded){
    gameIsPaused = false;
    PlayerController.Instance.PrevState();
    Time.timeScale = 1;
    SceneManager.UnloadSceneAsync("PauseMenu");
    }
    else {SceneManager.UnloadSceneAsync("Settings");}
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

public void PlayLevel1(int levelIndex){
    StartCoroutine(LoadLevel(levelIndex));
    //SceneManager.UnloadSceneAsync("BlankScene");
}

public IEnumerator LoadLevel(int levelIndex){
    yield return new WaitForSeconds(0.9f);
    //SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
    SceneManager.LoadScene(levelIndex);
    //yield return new WaitForSeconds(1);
    //SceneManager.UnloadSceneAsync(5);
    
}
#endregion

#region GameOver

public void GameOver(){
    SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
}

#endregion

public void Settings(){
    SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
    
}

public void BackToPrevScene(){
    SceneManager.UnloadSceneAsync("Settings");
}
}




}
