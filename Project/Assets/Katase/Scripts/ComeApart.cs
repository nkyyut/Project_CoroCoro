using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//参考サイト
//https://gomafrontier.com/unity/1789#i

//オブジェクトをバラバラにする
//子にRigidbodyが無ければバラバラにならないよ
public class ComeApart : MonoBehaviour {

    public float m_delete_time = 0.0f; //破片が飛び散り、消えるまでの時間(値が０の時は破棄しないようになる)
    public float m_random_min;  //ランダムで力を加えたり、回転したりするときの最小値(この値を含む)
    public float m_random_max;  //ランダムで力を加えたり、回転したりするときの最大値(この値を含む)
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void destroyObject()
    {
        //子供たちのRigidbody情報を取得
        Rigidbody[] child_rigidbodys = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody r in child_rigidbodys)
        {
            r.isKinematic = false;  //物理エンジンによって駆動するようにする
            r.transform.SetParent(null);    //親情報を解除
            //一定時間経過すると破棄する
            if(m_delete_time != 0.0f)
                Destroy(r.gameObject, m_delete_time);      //一定時間経過するとオブジェクトを破棄するようにする
            var vect = RandomVector3(m_random_min,m_random_max);    //ランダムな値を決める(Yの値は０未満にならない)
            r.AddForce(vect, ForceMode.Impulse);    //一瞬ランダムな方向に加速度を与える
            r.AddTorque(vect, ForceMode.Impulse);   //ランダムな方向に回転を与える
        }
        Destroy(gameObject);
    }

    //Y値は０未満にならない
    Vector3 RandomVector3(float min,float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(0.0f, max), Random.Range(min, max));
    }
}
