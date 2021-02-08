using System;
using UnityEngine;
using UnityEngine.Events;

public class DailyLoginManager : MonoBehaviour
{
    private string LastLogin
    {
        set
        {
            PlayerPrefs.SetString("LastLogin",value);
        }
        get
        { 
            return PlayerPrefs.GetString("LastLogin","FirstDay");
        }
    }
   
    public bool IsNewDay()
    {
        DateTime dateTime = DateTime.Now;

        if (LastLogin == "FirstDay")
        {
            LastLogin = dateTime.Day + "/" + dateTime.Month;
            PlayerPrefs.SetString("ShopName", "");
            return true;
        }
        else
        {
            string[] lastLogin = LastLogin.Split('/');
            int lastDay = int.Parse(lastLogin[0]);
            int lastMonth = int.Parse(lastLogin[1]);

            int day = int.Parse(dateTime.Day.ToString());
            int month = int.Parse(dateTime.Month.ToString());

            if (month  > lastMonth)
            {
                LastLogin = dateTime.Day + "/" + dateTime.Month;
                PlayerPrefs.SetString("ShopName", "");
                return true;
            }
            else
            {
                if (day > lastDay)
                {
                    LastLogin = dateTime.Day + "/" + dateTime.Month;
                    PlayerPrefs.SetString("ShopName", "");
                    return true;
                }
            }
        }
        return false;
    }    // Start Function Ending

}
