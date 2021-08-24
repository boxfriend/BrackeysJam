using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JuJu{

public class UIManager : MonoBehaviour
{
    public Animator transition;

    public void Resume(){
        GameManager.instance.Resume();
    }

    public void Restart(){
        GameManager.instance.Restart();
    }

    public void BackToMenu(){
        //transition.SetTrigger("Start");
        GameManager.instance.BackToMainMenu();
    }

    public void PlayLevel(){
        transition.SetTrigger("Start");
        GameManager.instance.PlayLevel1();
    }

    public void Quit(){
        Application.Quit();
    }

}

}
