using UnityEngine;
using UnityEngine.UI;

public class ChangeAudio : MonoBehaviour
{
    public void ChangeVol(float newValue)
    {
        float newVol = AudioListener.volume;
        newVol = newValue;
        AudioListener.volume = newVol;
    }
}