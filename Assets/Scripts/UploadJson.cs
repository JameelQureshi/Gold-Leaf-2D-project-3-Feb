using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadJson : MonoBehaviour
{
    public Text Status;
    public FileOperations referecne;

    public  void Upload()
    {
        StartCoroutine(UploadUserData());
    }


    IEnumerator UploadUserData()
    {
       
        WWWForm form = new WWWForm();
        string path = Application.persistentDataPath + "/SaveData3.json";
        byte[] bytes = File.ReadAllBytes(path);
        form.AddBinaryData("file", bytes,"SaveData3.json");

        UnityWebRequest webRequest = UnityWebRequest.Post("https://tron.vbth.app/service/act-data/upload-data", form);

        webRequest.SendWebRequest();

        while (!webRequest.isDone)
        {
            yield return null;
           Status.text = "Uploading...";
        }


        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
            Status.text = "Error";
        }
        else
        {
            Debug.Log("Request Done!:" + webRequest.downloadHandler.text);
            //UploadFileManager.RemoveDoneFileName(fileName);
            Status.text = "Success";
            referecne.ClearJasonandUPdateUserData();
        }
    }
}
