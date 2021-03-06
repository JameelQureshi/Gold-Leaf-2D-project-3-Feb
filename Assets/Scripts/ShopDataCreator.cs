﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ShopDataCreator : MonoBehaviour
{

    public GameObject prefab;
    public GameObject canvas;
  
    public static ShopDataCreator instance;

    public  List<GameObject> shopList;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void Populate( SaveData userdata_obj)
    {
        GameObject item; // Create GameObject instance
           
           
        try
        {
            for (int i = 0; i < userdata_obj.Items.Count; i++)
            {
                item = Instantiate(prefab, transform);

                item.GetComponent<Items>().Init(i.ToString(), userdata_obj.Items[i].Field2, userdata_obj.Items[i].Field4, userdata_obj.Items[i].Num1, userdata_obj.Items[i].Num2);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
               
        }

        float width = canvas.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width, 100);
        GetComponent<GridLayoutGroup>().cellSize = newSize;

    }

}




