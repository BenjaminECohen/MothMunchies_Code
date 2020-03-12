using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour
{
    private Animator anim;
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void setInfo(string name)
    {
        info.text = name;
    }

    public void TriggerAnimation()
    {
        anim.SetBool("Activate", true);
    }
    
    public void setAnimBoolFalse()
    {
        anim.SetBool("Activate", false);
    }


}
