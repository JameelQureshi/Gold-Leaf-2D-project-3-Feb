using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class PostLoginSignUp : MonoBehaviour
{

    public InputField Username, Password;

    public InputField Shopname, Location, ShopCOntact, OwnerName;

    public InputField UserName, UserAge, UserCNIC, UserSmokerStstus;

    public GameObject Loading;

    public GameObject Login_Screen, Shop_Data_Screen, User_Data_Scren, ShowAllData_Screen, TutorialScreen;

    public GameObject ErrorPopUp;
    public Text ErrorText;

    public DailyLoginManager reference;
    public FileOperations referecen1;

    public GameObject videoplayer1;

    public GameObject LoadingCanvas;

    public SceneLoader loader;

    

    public void Start()
    {

        if (PlayerPrefs.GetString("IslogedIn") =="false")
        {
            Login_Screen.SetActive(true);
        }

        if(PlayerPrefs.GetString("IslogedIn") == "true")
        {
            Login_Screen.SetActive(false);
            if(reference.IsNewDay())
            {
                Shop_Data_Screen.SetActive(true);
            }
            else
            {
                User_Data_Scren.SetActive(true);
            }
        }

        if (PlayerPrefs.GetString("IslogedIn") == "true")
        {
            Login_Screen.SetActive(false);
            if (reference.IsNewDay())
            {
                Shop_Data_Screen.SetActive(true);
            }
            else
            {
                if (PlayerPrefs.GetString("ShopName") == "")
                {
                    Login_Screen.SetActive(false);
                    User_Data_Scren.SetActive(false);
                    Shop_Data_Screen.SetActive(true);
                }
                else
                {
                    Login_Screen.SetActive(false);
                    Shop_Data_Screen.SetActive(false);
                    User_Data_Scren.SetActive(true);

                }
            }
        }

       
    }


    public void SignINFunc()
    {
        
        if(Username.text != "" && Password.text != "")
        {
            StartCoroutine(SignIn(Username.text, Password.text));
            Loading.SetActive(true);
        }
        else
        {
            UpdateErrorMessage("Enter both UserName and password" , Color.red);
        }
    }

    IEnumerator SignIn(string email , string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", email);
        form.AddField("Password", password);

        UnityWebRequest www = UnityWebRequest.Post("https://tron.vbth.app/service/appuser/user-login", form);
        www.uploadHandler.contentType = "application/json";
        yield return www.SendWebRequest();


        if (www.isNetworkError)
        {
             Invoke("offLoaidng", 0.5f);
            Debug.Log(www.downloadHandler.text);
            UpdateErrorMessage("Network Error" , Color.red);
        }
        else if(www.isHttpError)
        {
            if(www.downloadHandler.text == "Invalid email or password")
            {
                 Invoke("offLoaidng", 0.5f);
                UpdateErrorMessage("Username or password is incorrect" , Color.red);
            }
            else
            {
                 Invoke("offLoaidng", 0.5f);
                Handheld.Vibrate();
               
            }
        }
        else
        {
             Invoke("offLoaidng", 0.5f);
            print(www.downloadHandler.text);

            SignInResult SigninResultJason = JsonUtility.FromJson<SignInResult>(www.downloadHandler.text);


            if (SigninResultJason.Status == "ERROR")
            {
                UpdateErrorMessage("Error404", Color.red);
            }

            else if(SigninResultJason.Status == "OK")
            {
                //UpdateErrorMessage("Good to GO", Color.green);

                PlayerPrefs.SetString("UserName", SigninResultJason.Data[0].Username);
                PlayerPrefs.SetString("ActCode", SigninResultJason.Data[0].ActCode);
                PlayerPrefs.SetString("IslogedIn", "true");

                print("User Signed in as:" + SigninResultJason.Data[0].Username);

                if(reference.IsNewDay())
                {
                    Login_Screen.SetActive(false);
                    Shop_Data_Screen.SetActive(true);
                }
                else
                {
                    Login_Screen.SetActive(false);
                    Shop_Data_Screen.SetActive(false);
                    User_Data_Scren.SetActive(true);
                }
               
            }
        }
    }

    public void SendShopData()
    {
        // Called just oonce

        if (Shopname.text != "" && Location.text != "" && ShopCOntact.text != "" && OwnerName.text != "")
        {
            StartCoroutine(SendShopData(Shopname.text, Location.text, ShopCOntact.text, OwnerName.text));
            Loading.SetActive(true);
        }
        else
        {
            UpdateErrorMessage("Enter complete Data", Color.red);
        }
    }

    IEnumerator SendShopData(string shopname, string location, string shopcontact, string ownerName)
    {
        WWWForm form = new WWWForm();
        form.AddField("Username", PlayerPrefs.GetString("UserName"));
        form.AddField("Name", shopname);
        form.AddField("Location", location);
        form.AddField("Contact", shopcontact);
        form.AddField("OwnerName", ownerName);
        form.AddField("ActCode", PlayerPrefs.GetString("ActCode"));


        UnityWebRequest www = UnityWebRequest.Post("https://tron.vbth.app/service/appuser/save-shop", form);
        www.uploadHandler.contentType = "application/json";
        yield return www.SendWebRequest();


        if (www.isNetworkError)
        {
             Invoke("offLoaidng", 0.5f);
            Debug.Log(www.downloadHandler.text);
            UpdateErrorMessage("Network Error", Color.red);
        }
        else if (www.isHttpError)
        {
           
                 Invoke("offLoaidng", 0.5f);
                UpdateErrorMessage("Error Occured", Color.red);
            
        }
        else
        {
             Invoke("offLoaidng", 0.5f);
            print(www.downloadHandler.text);

            ShopData ShopDataResult = JsonUtility.FromJson<ShopData>(www.downloadHandler.text);

            print(ShopDataResult.Status);
            
            if (ShopDataResult.Status == "ERROR")
            {
                UpdateErrorMessage("Error404", Color.red);
            }

            else if (ShopDataResult.Status == "OK")
            {
                PlayerPrefs.SetString("ShopName", ShopDataResult.Data[0].Name);

                // Open ShopData Collection once in a day

                Login_Screen.SetActive(false);
                Shop_Data_Screen.SetActive(false);
                User_Data_Scren.SetActive(true);

                // Dont OPen the shop data collection now for next day
            }

        }
    }


    public void ONUserdataTaken()
    {
        if (UserName.text == string.Empty || UserAge.text == string.Empty || UserCNIC.text == string.Empty || UserSmokerStstus.text == string.Empty)
        {
            UpdateErrorMessage("Fill all fields", Color.red);
            return;
           
        }
        else if ( int.Parse(UserAge.text.ToString()) <18)
        {
            UpdateErrorMessage("Age is less then 18", Color.red);
            return;
        }
        else if ((UserCNIC.text).Length != 13)
        {
            UpdateErrorMessage("CNIC is not correct", Color.red);
            return;
        }
        else
        {
           
            PlayerPrefs.SetString("Player_UserName", UserName.text);
            PlayerPrefs.SetString("Playerr_Age", UserAge.text);
            PlayerPrefs.SetString("Playerr_CNIC", UserCNIC.text);
            PlayerPrefs.SetString("Playerr_Smokerstatus", UserSmokerStstus.text);

            User_Data_Scren.SetActive(false);
            videoplayer1.SetActive(false);
            try
            {
                LoadingCanvas.SetActive(true);
            }
            catch(Exception e)
            {

            }
            
            StartCoroutine(DelayfroVideo());

            
            /// Start Game process
        }
    }

    IEnumerator DelayfroVideo()
    {
        yield return new WaitForSeconds(1.5f);
        loader.LoadScene(1);
    }

    public void Logout_Seesion()
    {
        User_Data_Scren.SetActive(false);
        Login_Screen.SetActive(true);
        PlayerPrefs.SetString("IslogedIn", "false");
    }

    //// PopUps
    public void offLoaidng()
    {
        Loading.SetActive(false);
    }

    private void UpdateErrorMessage(string message , Color errorcolor)
    {
        ErrorPopUp.SetActive(true);
        ErrorText.color = errorcolor;
        ErrorText.text = message;
        Handheld.Vibrate();
        Invoke("ClearErrorMessage", 3f);

       
    }
    private void ClearErrorMessage()
    {
        ErrorText.text = "";
        ErrorPopUp.SetActive(false);
    }
}

[System.Serializable]
public class SinInData
{
    public int ID;
    public string ActCode;
    public string Username;
    public string Password;
    public object CreatedOn;
    public object LoginTime;
    public string Status;
    public object DeviceDetail;
    public object AppName;
}
[System.Serializable]
public class SignInResult
{
    public string Status;
    public object Message;
    public List<SinInData> Data;
}

[System.Serializable]
public class MyShopData
{
    public int ID;
    public string Name;
    public string Area;
    public object City;
    public bool Active;
    public string Field1;
    public string Field2;
    public object Field3;
    public object Field4;
    public DateTime CreatedOn;
}
[System.Serializable]
public class ShopData
{
    public string Status;
    public object Message;
    public List<MyShopData> Data;
}

[System.Serializable]
public class DataToServer
{
    public string ActCode;
    public string Field1;
    public string Field2;
    public string Field3;
    public string Field4;
    public string Field5;
    public string Field6;
    public string Num1;
    public string Num2;
}