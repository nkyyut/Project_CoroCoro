using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarCtrl : MonoBehaviour {
    public GameObject player;
    Player playerScrit;
    Slider _slider;

    void Start()
    {
        //_hp = GameObject.Find("ball").GetComponent<>().Player.hp.get();
        //player = GameObject.Find("Player").GetComponent<GameObject>();
        playerScrit = player.GetComponent<Player>();
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();

    }

    void Update()
    {
        playerScrit = player.GetComponent<Player>();
        float hp = playerScrit.GetHitPoint();
        Debug.Log(playerScrit.GetHitPoint());
        float MAX_HP = playerScrit.GetMaxHitPoint();

        // HPゲージに値を設定
        _slider.value = hp / MAX_HP;
    }
}
