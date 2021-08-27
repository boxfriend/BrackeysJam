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
    SceneManager.LoadSceneAsync(levelIndex);
    SceneManager.LoadSceneAsync("BlankScene", LoadSceneMode.Additive);
    yield return new WaitForSeconds(0.3f);
    SceneManager.UnloadSceneAsync("BlankScene");
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
