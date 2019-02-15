using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public enum GAME_TYPE
    {
        START,
        MAIN,
        CLEAR,
        OVER
    }
    private GAME_TYPE gameStatus;
    private bool flag;  // １回だけ
    private bool clearFlag;
    GameObject[] players;

    FideInOut GameOverScript;
    FideInOut GameClearScript;

    public int limitTime = 10;

    // 最初に一度だけ呼ばれる
    void Start()
    {
        GameOverScript = GameObject.Find("GameOver").GetComponent<FideInOut>(); // ゲームオーバーのオブジェクト名を書くところ
        GameClearScript = GameObject.Find("GameClear").GetComponent<FideInOut>(); // ゲームクリアのオブジェクト名を書くところ
        gameStatus = GAME_TYPE.START; // 初期はスタートから
        flag = false;   // 1回だけ呼び出す用のフラグ
        clearFlag = false;  // クリアかどうか判定する変数
        players = GameObject.FindGameObjectsWithTag("Player");  // プレイヤーのオブジェクトを取得する
        // 
    }

    // 毎フレーム呼ばれる
	void Update ()
    {
        switch (gameStatus)
        {
            case GAME_TYPE.START:
                // 最初の一回だけ呼ぶ
                if (!flag)
                {
                    // キャラクタの移動のバインド ←キャラのStartで行う
                    flag = true;    // 初期化の処理を一回だけ行う処理
                }

                // 制限時間が0の時
                if (GameObject.Find("TimeCounter").GetComponent<TimerCounter>().IsTimeOver())
                {
                    ChangeGameStatus(GAME_TYPE.MAIN);
                    GameObject.Find("TimeCounter").GetComponent<TimerCounter>().SetTime(limitTime); // 制限時間を変更
                    GameObject.Find("TouchStartText").GetComponent<Text>().text = "";   // 中身を空にする
                }
                break;

            case GAME_TYPE.MAIN:
                if (!flag)
                {
                    // 制限時間の変数の初期化 アクティブかどうかの判定をbool型で行う
                    // 全プレイヤの移動解禁
                    foreach (var player in players)
                        player.GetComponent<Player>().UnBind();
                    clearFlag = false;  // ゲームをクリアしてない状態へクリア←
                    flag = true;    // 一回だけ行う処理なので
                }

                // プレイヤーと衝突する
                if (isClear()) ChangeGameStatus(GAME_TYPE.CLEAR);

                // HPが無くなる、制限時間が過ぎる
                if (isOver()) ChangeGameStatus(GAME_TYPE.OVER);

                break;

            case GAME_TYPE.CLEAR:
                if (!flag)
                {
                    foreach (var player in players)
                        player.GetComponent<Player>().Bind();
                }
                // ゲームクリア画面のフェードイン
                GameClearScript.OnFideIn();

                // 画面に文字を表示
                //GameObject.Find("TouchStartText").GetComponent<Text>().text = "GAME CLEAR";   // 中身を空にする
                // 画面のタッチによる画面の遷移
                if ( isTouch())
                {
                    SceneManager.LoadScene("StageSelect");
                }
                break;

            case GAME_TYPE.OVER:
                // 画面に文字を表示
                //GameObject.Find("TouchStartText").GetComponent<Text>().text = "GAME OVER";   // 中身を空にする
                // ゲームオーバーのフェードイン
                GameOverScript.OnFideIn();
                if ( isTouch())
                {
                    SceneManager.LoadScene("StageSelect");
                }
                break;
        }
	}

    // ゲームステータスを変更
    public void ChangeGameStatus( GAME_TYPE gameStatus )
    {
        this.gameStatus = gameStatus;
        flag = false;
    }

    // ゲームクリアの判定
    private bool isClear()
    {
        if (clearFlag)
            return true;   
        return false;
    }
    // セッター
    
    // ゲームオーバーの判定
    private bool isOver()
    {
        // HPが無くなる
        foreach (var player in players)
        {
            // HPが無くなったら
            if( player.GetComponent<Player>().isDead() )
            {
                return true; // ゲームオーバー
            }
        }

        // 制限時間が過ぎたら
        if ( GameObject.Find("TimeCounter").GetComponent<TimerCounter>().IsTimeOver() )
        {
            return true;
        }
        
        return false;
    }

    // タッチしているかどうか
    public bool isTouch()
    {
        // 一回以上タッチしている時
        if( Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // タッチ直後
            if ( touch.phase == TouchPhase.Began)
                return true;
        }
        return false;
    }

    // リトライ用の関数
    public void ReTry(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //---------------------------
    //  アクセス関数
    //---------------------------
    public GAME_TYPE GetGameType()
    {
        return gameStatus;
    }

    public bool ClearFlag
    {
        set { clearFlag = value; }
    }

    public void GameStart()
    {

    }
}