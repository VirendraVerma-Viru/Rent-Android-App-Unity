using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

public class saveload : MonoBehaviour
{
    public static int id = 0;
    public static string username = " ";
    public static string email = " ";
    public static string profilepicName = " ";
    public static string address = " ";
    public static string phone = " ";

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/info.dat");
        Finance_Data data = new Finance_Data();


        data.ID = id;
        data.Username = username;
        data.Email = email;
        data.ProfilePicName = profilepicName;
        data.Address = address;
        data.Phone = phone;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/info.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/info.dat", FileMode.Open);
            Finance_Data data = (Finance_Data)bf.Deserialize(file);

            id = data.ID;
            username = data.Username;
            email = data.Email;
            profilepicName = data.ProfilePicName;
            address = data.Address;
            phone = data.Phone;

            file.Close();

        }
        else
            saveload.Save();
    }

}


[Serializable]
class Finance_Data
{
    public int ID;
    public string Username;
    public string Email;
    public string ProfilePicName;
    public string Address;
    public string Phone;
}


