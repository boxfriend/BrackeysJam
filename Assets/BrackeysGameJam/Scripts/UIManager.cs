using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JuJu{

    public class UIManager : MonoBehaviour
    {
        public Animator transition;

        private void Start()
        {
            
        }

        public void Resume(){
            GameManager.instance.Resume();
        }

        public void Restart(){
            AudioManager.instance.PlaySound("UIEnter");
            GameManager.instance.Restart();
        }

        public void BackToMenu(){
            //transition.SetTrigger("Start");
            AudioManager.instance.PlaySound("UIExit");
            GameManager.instance.BackToMainMenu();
        }

        public void PlayLevel(int levelIndex){
            transition.SetTrigger("Start");
            AudioManager.instance.PlaySound("Start");
            GameManager.instance.PlayLevel1(levelIndex);
        }

        public void Quit(){
            Application.Quit();
        }

        public void GoToSettings(){
            AudioManager.instance.PlaySound("UIEnter");
            GameManager.instance.Settings();
        }

        public void PrevScene(){
            AudioManager.instance.PlaySound("UIExit");
            GameManager.instance.BackToPrevScene();
        }
    }

}
