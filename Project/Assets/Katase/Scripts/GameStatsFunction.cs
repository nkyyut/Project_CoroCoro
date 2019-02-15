using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

//[CustomEditor(typeof(GameStatsFunction))]
//public class GameStatsFunctionEditor : Editor
//{
    
//}
//ゲームステータスごとの関数呼び出し
public class GameStatsFunction : MonoBehaviour
{

    //ゲームマネージャーの情報
    [HeaderAttribute("ゲームマネージャーの情報")]
    public GameManager m_game_manager;
    [HeaderAttribute("ゲームスタート時実行関数")]
    public UnityEvent m_start_event;
    [HeaderAttribute("ゲームメイン時実行関数")]
    public UnityEvent m_main_event;
    [HeaderAttribute("ゲームクリア時実行関数")]
    public UnityEvent m_clear_event;
    [HeaderAttribute("ゲームオーバー時実行関数")]
    public UnityEvent m_over_event;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //各ゲームステータスごとに関数を実行
    public void FunctionExecution()
    {
        //ゲームマネージャーのゲームステータスごとに実行する関数を変更
        /*
        switch(*m_game_manager.(ここにステータス情報を取得する関数を呼ぶ　戻り値はGameManager.GAME_TYPE))
        {
            case GameManager.GAME_TYPE.START:
                {
                    m_start_event.Invoke();
                    break;
                }
            case GameManager.GAME_TYPE.MAIN:
                {
                    m_main_event.Invoke();
                    break;
                }
            case GameManager.GAME_TYPE.CLEAR:
                {
                    m_clear_event.Invoke();
                    break;
                }
            case GameManager.GAME_TYPE.OVER:
                {
                    m_over_event.Invoke();
                    break;
                }
        }
       */
    }
}
