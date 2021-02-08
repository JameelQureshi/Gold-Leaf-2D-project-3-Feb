using System.Collections.Generic;

public class SaveData
{
    public static List<DatatoSend> Items { get; set; }
}


public class DatatoSend
{
    
    public string ActCode { get; set; }
    public string Field1 { get; set; }
    public string Field2 { get; set; }
    public string Field3 { get; set; }
    public string Field4 { get; set; }
    public string Field5 { get; set; }
    public string Field6 { get; set; }
    public string Num1 { get; set; }
    public string Num2 { get; set; }
    public string CreatedOn { get; set; }
}
