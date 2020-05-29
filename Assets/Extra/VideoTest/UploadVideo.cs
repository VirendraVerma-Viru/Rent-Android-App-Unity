using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadVideo : MonoBehaviour
{
   
    public string m_LocalFileName;
    private string m_URL;
    public Text statusText;
    public Text statusText1;

    public string fileName;

    void Start()
    {
        m_LocalFileName = Application.dataPath + "/im.jpg";
        m_URL = "https://createsurvey.000webhostapp.com/Extra/upload.php";
        print(m_LocalFileName);
    }

    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        WWW localFile = new WWW("file://" + m_LocalFileName);
        statusText.text = "file://" + m_LocalFileName;
        yield return localFile;
        statusText.text = statusText.text + "| " + localFile.text;
        if (localFile.error == null)
        {
            Debug.Log("Loaded file successfully");
            statusText1.text = "Loaded file successfully";
        }
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            statusText1.text = "Loaded file error: " + localFile.error;
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        postForm.AddBinaryData("theFile", localFile.bytes, m_LocalFileName, "text/plain");


        
        print("short out");
        print(m_LocalFileName.LastIndexOf("/"));
        int n=m_LocalFileName.LastIndexOf("/");
        
        print(m_LocalFileName.Substring(n+1));
        fileName = m_LocalFileName.Substring(n + 1);

        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done :" + upload.text);
            statusText1.text = "upload done " + upload.text;
            FindObjectOfType<downloadImage1>().RefreshImage();
        }
        else
        {
            Debug.Log("Error during upload: " + upload.error);
            statusText1.text = "Error during upload: " + upload.text;
        }
    }
    public void UploadFile()
    {
        StartCoroutine(UploadFileCo(m_LocalFileName, m_URL));
    }
    /*
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 750, Screen.width, Screen.height));
        
        m_URL = GUILayout.TextField(m_URL);
        if (GUILayout.Button("Upload"))
        {
            //UploadFile(m_LocalFileName, m_URL);
        }
        GUILayout.EndArea();
    }
     * */
}
