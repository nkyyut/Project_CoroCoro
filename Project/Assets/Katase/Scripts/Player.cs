using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // ダメージ関係の変数
    public const float MAX_HP = 5.0f; // 衝突していい最大数
    private float hp;             // 当たったときのカウント
    public float damageRange = 20.0f;   // これ以上の速度だとダメージを受ける

    // 移動用変数
    public float speed = 2.0f; // 初期値20
    Quaternion inputGyro;
    Rigidbody rigid;
    float oldMagnitude;

    // 開始処理時動けなくする変数
    private bool bind;

    // 最初に一度だけ呼ばれる
    void Start()
    {
        bind = true;    // バインドする
        hp = MAX_HP;    // HPを初期化する
        rigid = this.GetComponent<Rigidbody>();
        //force = new Vector3(); //移動速度の初期化
        Input.gyro.enabled = true;  // ジャイロ回転を有効
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        if (bind) return;   // バインドされている間処理をスキップ

        Move();
    }
    // 
    private void Move()
    {
        oldMagnitude = rigid.velocity.magnitude;
        // 小さい値の入力は
        Vector3 gravity = new Vector3();
        gravity.x = Input.gyro.gravity.x;
        gravity.z = Input.gyro.gravity.y;
        rigid.AddForce(gravity * speed, ForceMode.Force);
        Debug.Log("magnitude" + rigid.velocity.magnitude);
        /*
        inputGyro = Input.gyro.attitude;        // ジャイロの取得
        force = new Vector3();  // 移動ベクトルの初期化
        if (Mathf.Abs(inputGyro.x) > 0.1f)  // ベクトルの長さが一定以上の時
            force.x = -inputGyro.y; // 力を代入
        if (Mathf.Abs(inputGyro.x) > 0.1f)  // ベクトルの長さが一定以上の時
            force.z = inputGyro.x;  // 力を代入
        rigid.AddForce(force.normalized * speed, ForceMode.Force);          // 正規化した移動ベクトルとスピードをかける
        */
    }

    // ダメージを受けるときに呼ばれる うまくいった
    public void Damage()
    {
        Debug.Log("Damage関数の呼び出し");

        // ダメージを受ける状態の時
        if (oldMagnitude >= damageRange)
        {
            this.hp--;   // HPを減らす
            // 死んだとき
            if (isDead()) {
                //Destroy(gameObject);
            }
            Debug.Log(hp); // HPのデバッグ
        }
    }

    // キャラクタの移動を無効化
    public void Bind()
    {
        bind = true;   // バインドする
        rigid.isKinematic = true;   // 他から有効にされない
    }

    // キャラクタの移動を有効化
    public void UnBind()
    {
        bind = false;   // バインドを解除
    }

    // 死亡の判定
    public bool isDead()
    {
        if( hp > 0 )
            return false;
        return true;
    }

    // アクセス関数
    public float GetHitPoint()
    {
        Debug.Log("GetHitPoint"+ hp);
        return hp;
    }
    public float GetMaxHitPoint()
    {
        return MAX_HP;
    }

}
