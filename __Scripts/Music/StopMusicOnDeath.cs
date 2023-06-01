using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicOnDeath : MonoBehaviour
{
    AudioSource audioSource;

    float targetPitch = 0.38f;
    float targetVolume = 0f;
    float pitchLerpSpeed = 0.005f;
    float volumeLerpSpeed = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
        // Cache audio source
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.GetPlayer() != null && !GameController.Instance.IsPlayerAlive())
        {
            StartCoroutine(UpdateAudio());
        }
        
    }

    IEnumerator UpdateAudio()
    {
        while (true)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, pitchLerpSpeed * Time.deltaTime);

            if (audioSource.pitch < 0.4f)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, volumeLerpSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }
}
