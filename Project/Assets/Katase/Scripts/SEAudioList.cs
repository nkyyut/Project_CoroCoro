using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class SEAudioList : MonoBehaviour
{
    public List<AudioClip> m_audioclip_list;
    //既に生成しているかどうか
    public static bool dont_destroy_enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (dont_destroy_enabled)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioStart(int audio_id)
    {
        if(audio_id < m_audioclip_list.Count || audio_id >= 0 )
        {
            AudioSource audio_source = GetComponent<AudioSource>();
            audio_source.clip = m_audioclip_list[audio_id];
            audio_source.Play();
        }
        else
        {
            Debug.Log("SEデーターがないで！");
        }
    }
}
