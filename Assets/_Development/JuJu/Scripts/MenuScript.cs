using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JuJu
{
public class MenuScript : MonoBehaviour
{
    public void PlayLevel1(){
        StartCoroutine(LoadScene(1));
    }

    public void Quit(){
        Application.Quit();
    }

    IEnumerator LoadScene(int levelIndex){
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }

}

}
