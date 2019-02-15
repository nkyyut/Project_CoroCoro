using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TagsHitControl : MonoBehaviour
{
    [Tooltip("このタグたちのどれかにHitしたときにhit_eventを呼ぶ\n同時に複数のタグに当たってもhit_eventが呼ばれるのは1回のみ")]
    public string[] m_target_tags;
    public UnityEvent m_hit_event;

    //何かのオブジェクト(Rigidbody持ち)とヒットしたとき
    void OnCollisionEnter(Collision other)
    {
        foreach(string str in m_target_tags)
        {
            if(other.gameObject.tag == str)
            {
                //指定タグのどれかにヒットしたら関数実行
                m_hit_event.Invoke();
                break;
            }
        }
    }

    public void DebugLogHit()
    {
        Debug.Log("Hit");
    }

    public void DebugSceneChange(string change_scene_name)
    {
        SceneManager.LoadScene(change_scene_name);
    }

    public void DebugSceneChange(int change_scene_buildlndex)
    {
        SceneManager.LoadScene(change_scene_buildlndex);
    }
}
