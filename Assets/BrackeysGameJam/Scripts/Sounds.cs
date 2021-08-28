using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds  
{
    ///<summary>
    ///defines the properties for a sound object, which can be assigned in the inspector in the Audiomanager GameObject. 
    ///</summary>


    public string name;
    public AudioClip clip;

    [Range(0,1)]
    public float volume = 1;

    [HideInInspector]
    public AudioSource source;

    public bool loop = false;
}
