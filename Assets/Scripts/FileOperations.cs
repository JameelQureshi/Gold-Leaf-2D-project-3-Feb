using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;


public class FileOperations : MonoBehaviour
{
    private string fileName;
    private string saveDataJson;
    private SaveData saveData;

    public GameObject UserdataCollection_screen;
    public GameObject showAlluserdata_screen;

    public Text myText;

    public void Awake()
    {
        fileName = Application.persistentDataPath + "/SaveData3.json";
        print(Application.persistentDataPath + "/SaveData3.json");
       
        ChechDataFile();        
    }

    public void Start()
    {
        if(PlayerPrefs.GetString("StartAppend") == "true")
        {
            AppendData();
        }
    }

    private void ChechDataFile()
    {
        try
        {
            if(!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exeption: " + e);
        }
    }

    public  void ReadDataFile()
    {
        saveDataJson = string.Empty;
        
        try
        {
            if(File.Exists(fileName))
            {
                saveDataJson = File.ReadAllText(fileName);

                myText.text = saveDataJson;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exeption: " + e);
        }
    }

    public void AppendData()
    {
        ReadDataFile();

        if (saveDataJson != string.Empty)
        {

            saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

            print(SaveData.Items[0].ActCode);


            //SaveData.Items = JsonUtility.FromJson<SaveData>(saveDataJson);


        }
        else
        {
            SaveData.Items = new List<DatatoSend>();
        }

        SaveData.Items = new List<DatatoSend>();

        DatatoSend mydata;

            mydata = new DatatoSend();
            mydata.ActCode = PlayerPrefs.GetString("ActCode");
            mydata.Field1 = PlayerPrefs.GetString("UserName");
            mydata.Field2 = PlayerPrefs.GetString("Player_UserName");
            mydata.Field3 = PlayerPrefs.GetString("Playerr_Age");
            mydata.Field4 = PlayerPrefs.GetString("Playerr_CNIC");
            mydata.Field5 = PlayerPrefs.GetString("Playerr_Smokerstatus");
            mydata.Field6 = PlayerPrefs.GetString("ShopName");
            mydata.Num1 = PlayerPrefs.GetString("User_Score");
            mydata.Num2 = PlayerPrefs.GetString("User_Time");
            mydata.CreatedOn = System.DateTime.Now.ToString();


        print(mydata.Field1);

        if (SaveData.Items == null)
        {
            print("its null");
        }
        SaveData.Items.Add(mydata);
        
        saveDataJson = JsonUtility.ToJson(saveData);

        try
        {
            File.WriteAllText(fileName, saveDataJson);
        }
        catch (Exception e)
        {
            Debug.Log("Exeption: " + e);
        }

        PlayerPrefs.SetString("StartAppend", "false");

        //LoadGameBtnListener();
    }

    public  void LoadGameBtnListener()
    {

        UserdataCollection_screen.SetActive(false);
        showAlluserdata_screen.SetActive(true);
        ReadDataFile();

        if (saveDataJson != string.Empty)
        {
            saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

        }
        else
        {
            saveData = new SaveData();
            SaveData.Items = new List<DatatoSend>();
        }


        ShopDataCreator.instance.Populate(saveData);
    }

    public void ClearJasonandUPdateUserData()
    {
        File.Delete(Application.persistentDataPath + "/SaveData3.json");
        Awake();
        LoadGameBtnListener();

        SceneManager.LoadScene(0);
    }

}