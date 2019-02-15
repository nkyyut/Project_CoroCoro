using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerCounter : MonoBehaviour
{

    [SerializeField]
    Image[] images;

    [SerializeField]
    Sprite[] numberSprites = new Sprite[10];

    public float timeCount { get; private set; }

    bool isTimeOver;

    void Start()
    {
        // 初期値
        SetTime(3);
        isTimeOver = false;
    }

    public void SetTime(float time)
    {
        timeCount = time;
        isTimeOver = false;
        StartCoroutine(TimerStart());
    }

    void SetNumbers(int sec, int val1, int val2)
    {
        string str = String.Format("{0:00}", sec);
        images[val1].sprite = numberSprites[Convert.ToInt32(str.Substring(0, 1))];
        images[val2].sprite = numberSprites[Convert.ToInt32(str.Substring(1, 1))];
    }

    IEnumerator TimerStart()
    {
        while (timeCount >= 0)
        {
            int sec = Mathf.FloorToInt(timeCount % 60);
            SetNumbers(sec, 2, 3);
            int minu = Mathf.FloorToInt((timeCount - sec) / 60);
            SetNumbers(minu, 0, 1);
            yield return new WaitForSeconds(1.0f);
            timeCount -= 1.0f;
        }
        TimeOver();
    }

    void TimeOver()
    {
        isTimeOver = true;
    }

    // アクセス関数
    public bool IsTimeOver()
    {
        return isTimeOver;
    }
}