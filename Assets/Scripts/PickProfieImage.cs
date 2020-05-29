using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class PickProfieImage : MonoBehaviour
{
    public GameObject ProfilePic;
    public GameObject ProfilePicShort;

    private string m_LocalFileName=" ";
    private string m_URL;
    private string imagesURL;

    public string fileName;
    public Text statusText;

    void Start()
    {
        //m_LocalFileName = Application.dataPath + "/im.jpg";
        imagesURL = "https://kreasarapps.000webhostapp.com/images/";
        m_URL = "https://kreasarapps.000webhostapp.com/PropertyBazar/uploadprofilepic.php";
        //print(m_LocalFileName);
        if (saveload.profilepicName != " ")
        {
            RefreshImage();
        }
    }

    public void PickFromGalaryButton()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        PickImage(512);

    }


    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            //Debug.Log("Image path: " + path);
            m_LocalFileName = path.ToString();
            

            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                /*
                // Assign texture to a temporary quad and destroy it after 5 seconds
                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                quad.transform.forward = Camera.main.transform.forward;
                quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                */

                Rect rec = new Rect(0, 0, texture.width, texture.height);
                ProfilePic.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

                /*
                Material material = quad.GetComponent<Renderer>().material;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;
                */
                //Destroy(quad, 10f);

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                //Destroy(texture, 10f);

                
            }
        }, "Select a PNG image", "image/png", maxSize);

        //Debug.Log("Permission result: " + permission);
    }

    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        WWW localFile = new WWW("file:///" + m_LocalFileName);
        //statusText.text = "file:///" + m_LocalFileName;
        print(m_LocalFileName);
        yield return localFile;
        //statusText.text = statusText.text + "| " + localFile.text;
        if (localFile.error == null)
        {
            Debug.Log("Loaded file successfully");
            //statusText1.text = "Loaded file successfully";
        }
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            //statusText1.text = "Loaded file error: " + localFile.error;
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        saveload.Load();
        postForm.AddField("id", saveload.id);
        postForm.AddBinaryData("theFile", localFile.bytes, m_LocalFileName, "text/plain");
        


        print("short out");
        print(m_LocalFileName.LastIndexOf("/"));
        int n = m_LocalFileName.LastIndexOf("/");

        print(m_LocalFileName.Substring(n + 1));
        fileName = m_LocalFileName.Substring(n + 1);

        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done :" + upload.text);
            statusText.text = "upload done " + upload.text;
            RefreshImage();
        }
        else
        {
            Debug.Log("Error during upload: " + upload.error);
            statusText.text = "Error during upload: " + upload.text;
        }
    }
    public void UploadFile()
    {
        StartCoroutine(UploadFileCo(m_LocalFileName, m_URL));
    }

    public void RefreshImage()
    {
        StartCoroutine(refresh());
    }

    IEnumerator refresh()
    {
        print(Application.persistentDataPath);
        //print(uploadfileScript.fileName);
        saveload.profilepicName = saveload.id + "" + fileName;
        saveload.Save();
        fileName = saveload.profilepicName;



        print("ImageURL:" + imagesURL + fileName);
        WWW www = new WWW(imagesURL + fileName);
        yield return www;
        Texture2D texture = www.texture;
        //this.GetComponent<Renderer>().material.mainTexture = texture;
        //byte[] bytes = texture.EncodeToJPG();
        //File.WriteAllBytes(Application.persistentDataPath + fileName, bytes);

        Rect rec = new Rect(0, 0, texture.width, texture.height);
        ProfilePic.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

        rec = new Rect(0, 0, texture.width, texture.height);
        ProfilePicShort.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

    }

}
