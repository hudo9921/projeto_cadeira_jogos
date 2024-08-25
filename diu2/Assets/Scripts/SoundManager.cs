using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

private float normalVolume = 1.0f; 
private float pausedVolume = 0.0f;

private bool  isMuted=false;

public GameObject SoundButton;
public Sprite soundOn;
public Sprite soundOff;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Som(){
        if(isMuted)
        {
            Desmutar();
        }
        else{
            Mutar();
        }
    }
    void Mutar(){
        AudioListener.volume = pausedVolume;
        isMuted=true;
        SoundButton.GetComponent<Image>().sprite=soundOff;
    }

    void Desmutar(){
        AudioListener.volume = normalVolume;
        isMuted=false;
        SoundButton.GetComponent<Image>().sprite=soundOn;
    }
}
