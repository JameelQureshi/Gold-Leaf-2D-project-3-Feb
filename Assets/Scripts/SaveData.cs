using System;
using System.Collections.Generic;

public class SaveData
{
    public List<DatatoSend> Items;
}

[Serializable]
public class DatatoSend
{
    public string ActCode;
    public string Field1;
    public string Field2;
    public string Field3 ;
    public string Field4;
    public string Field5;
    public string Field6;
    public string Num1;
    public string Num2;
    public string CreatedOn;
}
