using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JuJu{

public class UIManager : MonoBehaviour
{

    public void Resume(){
        GameManager.instance.Resume();
    }

    public void Restart(){
        GameManager.instance.Restart();
    }

    public void BackToMenu(){
        GameManager.instance.BackToMainMenu();
    }

    public void PlayLevel(){
        GameManager.instance.PlayLevel1();
    }

    public void Quit(){
        Application.Quit();
    }

}

}
