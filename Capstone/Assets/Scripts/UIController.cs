using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController: MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 2f;
    private bool fadingToBlack, fadingFromBlack;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            } 
        } else if (fadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }
    }
    public void FadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void FadeFromBlack()
    {
        fadingFromBlack = true;
        fadingToBlack = false;
    }


    
}
