using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVariation : MonoBehaviour
{
    public AudioClip[] clipArray;
    public AudioSource effectSource;
    private int clipIndex;
    public float pitchMin, pitchMax, volumeMin, volumeMax;
    // Start is called before the first frame update
    void Start()
    {
       //  PlayRoundRobin();
         PlayRandom2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayRoundRobin()
    {
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);

        if (clipIndex < clipArray.Length)
        {
            effectSource.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }

        else
        {
            clipIndex = 0;
            effectSource.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }
    }

    void PlayRandom()
    {
        clipIndex = Random.Range(0, clipArray.Length);
        effectSource.PlayOneShot(clipArray[clipIndex]);
    }

    int RepeatCheck(int previousIndex, int range)
    {
        int index = Random.Range(0, range);

        while (index == previousIndex)
        {
            index = Random.Range(0, range);
        }
        return index;
    }

    void PlayRandom2()
    {
        effectSource.pitch = Random.Range(pitchMin, pitchMax);
        effectSource.volume = Random.Range(volumeMin, volumeMax);
        clipIndex = RepeatCheck(clipIndex, clipArray.Length);
        effectSource.PlayOneShot(clipArray[clipIndex]);
    }
}
