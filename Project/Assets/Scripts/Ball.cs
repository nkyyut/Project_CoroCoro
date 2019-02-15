using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    //速度
    private Rigidbody _rigidbody;
    //効果音
    private AudioSource roll;   //転がる音
    private AudioSource hit;    //衝突の音
    //判定
    private bool rollflag;  //効果音が流れている判定

    void Start()
    {
        // 対象の
        _rigidbody = this.GetComponent<Rigidbody>();

        //転がる音
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hit = audioSources[0];  //衝突音
        roll = audioSources[1];  //回る音

        rollflag = true;
    }

    void Update()
    {
        //Debug.Log("速度: " + _rigidbody.velocity.magnitude);
        //速度があったら(rollを流す)
        if (_rigidbody.velocity.magnitude > 0)
        {
            if (rollflag)
            {
                roll.PlayOneShot(roll.clip);
                rollflag = false;
            }
        }else
        {
            //違ったら(止める)
            roll.Stop();
            rollflag = true;
        }
        //再生し終わったらtrueにしてもう一度再生。
        if(roll.time == 0.0f && !roll.isPlaying)
        {
            rollflag = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        hit.PlayOneShot(hit.clip);
    }
}