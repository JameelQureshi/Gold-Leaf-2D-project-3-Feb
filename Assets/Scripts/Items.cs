using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{

    public Text No;
    public Text Name;
    public Text CNIC;
    public Text Score;
    public Text Time;

   public void Init(string no, string name, string cnic, string score, string time)
    {
        No.text = no;
        Name.text = name;
        CNIC.text = cnic;
        Score.text = score;
        Time.text = time;
    }
}
