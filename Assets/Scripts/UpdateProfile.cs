using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateProfile : MonoBehaviour
{
    string updateprofileURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/updateprofile.php";

    public InputField NameInputfield;
    public InputField EmailInputfield;
    public InputField AddressInputfield;
    public InputField PhoneInputfield;

    public Text statusText;
    public GameObject ProfileImageGO;

    public void OnProfileSaveButtonClicked()
    {
        //update the profile details to server
        saveload.Load();
        int id = saveload.id;
        string name = NameInputfield.text;
        string email = EmailInputfield.text;
        string address = AddressInputfield.text;
        string phone = PhoneInputfield.text;
        ProfileImageGO.GetComponent<PickProfieImage>().UploadFile();
        StartCoroutine(UpdateProfileDetails(id,name,email,address,phone));
    }

    IEnumerator UpdateProfileDetails(int id,string name,string email,string address,string phone)
    {
        WWWForm form1 = new WWWForm();
        

        form1.AddField("id", id);
        form1.AddField("name", name);
        form1.AddField("email", email);
        form1.AddField("address", address);
        form1.AddField("phone", phone);

        WWW www = new WWW(updateprofileURL, form1);
        yield return www;


        if (www.text == "sucess")
        {
            statusText.text = "Sucessfully Updated";
            statusText.color = Color.green;

            //upload profile pic to server if any



            //updated succesfully
            saveload.username = name;
            saveload.email = email;
            saveload.address = address;
            saveload.phone = phone;
            saveload.Save();
            gameObject.GetComponent<MainAppManager>().UpdateTopDetails();
            UpdateInputFields();

        }
        else if (www.text == "")
        {
            statusText.text = "Check your Connection";
            statusText.color = Color.red;
        }
        else if (www.text == "error")
        {
            statusText.text = "Server is in maintainence";
            statusText.color = Color.red;
        }
        else if (www.text == "Account does not exist")
        {
            statusText.text = "Account does not exist";
            statusText.color = Color.red;
        }
        else
        {
            statusText.text = "Server is in maintainence";
            statusText.color = Color.red;
        }

    }



    public void UpdateInputFields()
    {
        NameInputfield.text = saveload.username;
        EmailInputfield.text = saveload.email;
        AddressInputfield.text = saveload.address;
        PhoneInputfield.text = saveload.phone;
    }
}
