using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class downloadImage1 : MonoBehaviour
{
    public UploadVideo uploadfileScript;
    
    public GameObject profileImage;

    void Start()
    {
        
        
    }

    public void RefreshImage()
    {
        StartCoroutine(refresh());
    }

    IEnumerator refresh()
    {
        print(Application.persistentDataPath);
        print(uploadfileScript.fileName);

        if (File.Exists(Application.persistentDataPath + "/"+uploadfileScript.fileName))
        {
            byte[] byteArray = File.ReadAllBytes(Application.persistentDataPath + "/"+uploadfileScript.fileName);
            Texture2D texture = new Texture2D(500, 600);
            //texture.LoadImage(byteArray);
            //print("file already exist:" + Application.persistentDataPath + "/"+uploadfileScript.fileName);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            profileImage.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        }
        else
        {
            
            print("ImageURL:" + "https://createsurvey.000webhostapp.com/ima/" + uploadfileScript.fileName);
            WWW www = new WWW("https://createsurvey.000webhostapp.com/ima/" + uploadfileScript.fileName);
            yield return www;
            Texture2D texture = www.texture;
            //this.GetComponent<Renderer>().material.mainTexture = texture;
            //byte[] bytes = texture.EncodeToJPG();
            //File.WriteAllBytes(Application.persistentDataPath + uploadfileScript.fileName, bytes);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            profileImage.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

        }
    }
}
