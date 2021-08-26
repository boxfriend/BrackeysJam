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

        public void PlayLevel(){
            transition.SetTrigger("Start");
            AudioManager.instance.PlaySound("Start");
            GameManager.instance.PlayLevel1();
        }

        public void Quit(){
            Application.Quit();
        }

    }

}
