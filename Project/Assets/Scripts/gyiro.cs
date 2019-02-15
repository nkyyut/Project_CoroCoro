using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyiro : MonoBehaviour
{

    //Rigidbody rb;
    //float moveSpeed = 3f;

    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion gattitude = Input.gyro.attitude;
        gattitude.x *= -1;
        gattitude.y *= -1;
        transform.localRotation =
            Quaternion.Euler(90, 0, 0) * gattitude;
        Debug.Log(gattitude);

    }

    void FixedUpdate()
    {
        //プレイヤーの方向から、XYZ平面の単位ベクトルを取得
        Vector3 boxForward = Vector3.Scale(this.transform.forward, new Vector3(1, 1, 1)).normalized;

        //移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        //rb.velocity = boxForward * moveSpeed;

    }
}
