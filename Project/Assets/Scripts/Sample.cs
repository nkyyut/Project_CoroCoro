using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    NumberImageRender numberImageRender = null;

    // Use this for initialization
    void Start()
    {
        numberImageRender = GetComponent<NumberImageRender>();
    }

    // Update is called once per frame
    void Update()
    {
        // 浮動小数点
        numberImageRender.Render(100.0);

        // 整数
        //numberImageRenderer.Render(100);
    }
}