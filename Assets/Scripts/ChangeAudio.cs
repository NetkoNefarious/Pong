using UnityEngine;

public class ChangeAudio : MonoBehaviour
{
    public void ChangeVol(float newValue)
    {
        float newVol = AudioListener.volume;
        newVol = newValue;
        AudioListener.volume = newVol;
    }
}