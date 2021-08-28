using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    ///<summary>
    /// On Awake, the Audiomanager creates an audiosource for each sound object that was before assigned in the inspector.
    ///</summary>


    public static AudioManager instance;

    public Sounds[] sounds; 

    private void Awake() {
        if(instance == null){
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else{Destroy(gameObject);}

//loops through each sound object in the array and assigns their values, which were assigned previously in the inspector, to the newly instantiated Audiosource
        foreach (Sounds currentSound in sounds)
        {
            currentSound.source = gameObject.AddComponent<AudioSource>();

            currentSound.source.clip = currentSound.clip;
            currentSound.source.volume = currentSound.volume;

            currentSound.source.playOnAwake = false;
            currentSound.source.loop = currentSound.loop;
        }
    }

    //searches the String of Sound objects for a sound with the fitting name and then plays it
    public void PlaySound(string name){
        foreach (Sounds sound in sounds)
        {
            if(sound.name == name){
                sound.source.Play();
            }
        }
    }

}
