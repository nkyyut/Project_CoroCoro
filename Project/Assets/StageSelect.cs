using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    // Inspectorからシーン上のボタンを登録しておく
    public Button[] ButtonArray;

    void Start()
    {
        // countの値を取得
        int count = 0;

        for (int loop = 0; loop < ButtonArray.Length; loop++)
        {
            if (loop < count)
            {
                // ボタンの有効化
                ButtonArray[loop].gameObject.SetActive(true);

                // ボタン押下時の処理
                int stageNo = loop;
                ButtonArray[loop].onClick.AddListener(() => { OnStartClicked(stageNo); });
            }
            else
            {
                // ボタンの無効化
                //ButtonArray[loop].gameObject.SetActive(false);
            }
        }
    }

    public void OnStartClicked(int stageNo)
    {

        SceneManager.LoadScene("Stage" + stageNo);
    }
}
