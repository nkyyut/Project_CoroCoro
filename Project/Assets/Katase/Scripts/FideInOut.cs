using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;


[RequireComponent(typeof(Image))]

//インスペクタの設定
[CustomEditor(typeof(FideInOut))]
//public class FideEditor : Editor

public class FideEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FideInOut fide = target as FideInOut;

        //フィードの変更モードの情報を取得
        fide.m_fide_change_mode = (FideInOut.FideChangeMode)EditorGUILayout.EnumPopup("フィードの変更モード",fide.m_fide_change_mode);

        //フィード変更モードが色での変更モードなら色の情報を設定できるようにインスペクタ表示する
        if (fide.m_fide_change_mode == FideInOut.FideChangeMode.Color)
        {
            fide.m_fide_color = EditorGUILayout.ColorField("フィード色",fide.m_fide_color);
            fide.m_fide_color.a = 1.0f;
        }
        //フィード変更モードが画像(α)での変更モードなら画像(α)の情報を設定できるようにインスペクタ表示
        else if(fide.m_fide_change_mode == FideInOut.FideChangeMode.Image)
        {
            fide.m_fide_image = (Image)EditorGUILayout.ObjectField("フィード画像(α画像)",fide.m_fide_image,typeof(Image),false);
        }

        //フィードイン～アウトまでの時間
        fide.m_fide_max_time = Mathf.Max(0,EditorGUILayout.FloatField("フィード時間(秒)", fide.m_fide_max_time));
        EditorGUILayout.HelpBox("フィードイン～アウトまでの時間(秒)",MessageType.None);
        EditorUtility.SetDirty(target);
    }
}
#endif
public class FideInOut : MonoBehaviour
{
    //フィードの変更モード
    public enum FideChangeMode
    {
        Color,
        Image
    }

    //フィードの変更モード
    public FideChangeMode m_fide_change_mode;
    //フィード変更モードが色の時の色情報
    public Color m_fide_color;
    //フィード変更モードが画像(α)の時の画像(α)情報
    public Image m_fide_image;

    //フィードイン～アウトまでの時間
    public float m_fide_max_time;

    //フィードのImage情報
    private GameObject m_fide_target;
    //フィードイン～アウトまでの経過時間
    private float m_fide_time;

    // Use this for initialization
    void Start()
    {
        m_fide_target = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFideIn()
    {
        StartCoroutine(FideInCorutine(m_fide_max_time / 2));

        if (m_fide_change_mode == FideChangeMode.Color)
        {

        }
        else if (m_fide_change_mode == FideChangeMode.Image)
        {

        }
    }

    public void OnFideOut()
    {
        StartCoroutine(FideOutCoroutine(m_fide_max_time/2));

        if (m_fide_change_mode == FideChangeMode.Color)
        {
            
        }
        else if (m_fide_change_mode == FideChangeMode.Image)
        {

        }
    }

    private IEnumerator FideInCorutine(float fide_in_max_time)
    {
        Debug.Log("a");
        //画像の色のA値のみを0.0fにする
        ColorAlphaChange(m_fide_target, 0.0f);
        //経過時間格納用
        float fide_in_time = fide_in_max_time;

        if(m_fide_change_mode == FideChangeMode.Color)
        {
            //現在の色情報をフィードするときの色にする
            float alpha = GetColorA(m_fide_target);
            SetColor(m_fide_target, m_fide_color);
            ColorAlphaChange(m_fide_target, alpha);

            while(fide_in_time > 0)
            {
                fide_in_time -= Time.deltaTime;

                ColorAlphaChange(m_fide_target, 1.0f - (fide_in_time / fide_in_max_time));

                yield return null;
            }
        }
        else if(m_fide_change_mode == FideChangeMode.Image)
        {

        }
    }

    private IEnumerator FideOutCoroutine(float fide_out_max_time)
    {
        //画像の色のA値のみを1.0にする
        //m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b,1.0f);
        ColorAlphaChange(m_fide_target, 1.0f);
        float fide_out_time = fide_out_max_time;

        if (m_fide_change_mode == FideChangeMode.Color)
        {
            //現在の色情報をフィードするときの色にする
            float alpha = GetColorA(m_fide_target);
            SetColor(m_fide_target, m_fide_color);
            ColorAlphaChange(m_fide_target, alpha);

            while (fide_out_time > 0)
            {
                fide_out_time -= Time.deltaTime;

                ColorAlphaChange(m_fide_target, fide_out_time / fide_out_max_time);

                yield return null;
            }
        }
        else if (m_fide_change_mode == FideChangeMode.Image)
        {

        }
    }

    //子もすべて変更する
    private void ColorAlphaChange(GameObject obj,float a)
    {
        Image obj_image = obj.GetComponent<Image>();
        Text obj_text = obj.GetComponent<Text>();

        if(obj_image != null)
        {
            obj_image.color = new Color(obj_image.color.r, obj_image.color.g, obj_image.color.b, a);
        }
        if(obj_text != null)
        {
            obj_text.color = new Color(obj_text.color.r, obj_text.color.g, obj_text.color.b, a);
        }

        foreach (Transform child in obj.transform)
        {
            Image child_image = child.GetComponent<Image>();
            Text child_text = child.GetComponent<Text>();

            if (child_image != null)
            {
                child_image.color = new Color(child_image.color.r, child_image.color.g, child_image.color.b, a);
            }
            if (child_text != null)
            {
                child_text.color = new Color(child_text.color.r, child_text.color.g, child_text.color.b, a);
            }

        }
    }

    private float GetColorA(GameObject obj)
    {
        Image obj_image = obj.GetComponent<Image>();
        Text obj_text = obj.GetComponent<Text>();

        if (obj_image != null)
        {
            return obj_image.color.a;
        }
        if (obj_text != null)
        {
            return obj_text.color.a;
        }

        return 0.0f;
    }

    private void SetColor(GameObject obj,Color color)
    {
        Image obj_image = obj.GetComponent<Image>();
        Text obj_text = obj.GetComponent<Text>();

        if (obj_image != null)
        {
            obj_image.color = color;
        }
        if (obj_text != null)
        {
            obj_text.color = color;
        }

        //foreach (Transform child in obj.transform)
        //{
        //    Image child_image = child.GetComponent<Image>();
        //    Text child_text = child.GetComponent<Text>();

        //    if (child_image != null)
        //    {
        //        child_image.color = color;
        //    }
        //    if (child_text != null)
        //    {
        //        child_text.color = color;
        //    }

        //}
    }
}
