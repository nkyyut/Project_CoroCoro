using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

//参考サイト
//https://www.f-sp.com/entry/2016/08/16/211214
public class RigidbodMove : MonoBehaviour
{
    public enum MoveMode
    {
        POSITION,
        VELOCITY,
        ADD_FORCE_FORCE,
        ADD_FORCE_IMPULSE,
        ADD_FORCE_ACCELERATION,
        ADD_FORCE_VELOCITY_CHANGE,
    }
    [Tooltip("Rigidbodyの移動方法")]
    public MoveMode move_mode;
    public float speed;
    [Tooltip("位置情報を0,0,0にする。デバック用")]
    public bool pos_reset = false;
    [Tooltip("デバック用の移動処理")]
    public bool debug_move_flag = true;
    //移動情報(入力情報)
    private Vector3 move;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //デバックモードなら移動をできるようにする
        if (debug_move_flag)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            move = new Vector3(x, 0.0f, y);
        }

        //移動モードによってRigidbodyの移動を変更する
        if (move_mode== MoveMode.POSITION)
            MovePosition();
        else if (move_mode == MoveMode.VELOCITY)
            MoveVelocity();
        else if (move_mode == MoveMode.ADD_FORCE_FORCE)
            MoveAddForce(ForceMode.Force);
        else if (move_mode == MoveMode.ADD_FORCE_IMPULSE)
            MoveAddForce(ForceMode.Impulse);
        else if (move_mode == MoveMode.ADD_FORCE_ACCELERATION)
            MoveAddForce(ForceMode.Acceleration);
        else if (move_mode == MoveMode.ADD_FORCE_VELOCITY_CHANGE)
            MoveAddForce(ForceMode.VelocityChange);

        //デバック用の位置情報初期化処理
        if(pos_reset)
        {
            rigidbody.position = new Vector3();
            transform.position = new Vector3();
            transform.Rotate(new Vector3());
        }
    }

    void MovePosition()
    {
        rigidbody.position = move * speed;
    }

    void MoveVelocity()
    {
        rigidbody.velocity = move * speed;
    }

    void MoveAddForce(ForceMode force_mode = ForceMode.Force)
    {
        rigidbody.AddForce(move * speed,force_mode);
    }

    //引数に移動情報を設定すると移動の情報を格納
    public void SetMoveVector(Vector3 _move)
    {
        move = _move;
    }
}
