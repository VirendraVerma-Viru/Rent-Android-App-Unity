using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainAppManager : MonoBehaviour
{
    //-------------------------url section--------------
    private string createaccounturl = "https://kreasarapps.000webhostapp.com/PropertyBazar/createaccount.php";
    private string loginaccounturl = "https://kreasarapps.000webhostapp.com/PropertyBazar/login.php";

    //---------------------------pannel section-------------------

    [Header("Top Profile Details")]
    public GameObject NamePannelTop;
    public GameObject LoginButtonTop;
    public GameObject ProfilePicImage;
    public Text NameTop;
    public Sprite ProfilePicTop;

    [Header("Login SignUp Pannels")]
    public GameObject ButtonPannel;
    public GameObject LoginPannel;
    public GameObject SignUpPannel;

    [Header("Basic Pannels of App")]
    public GameObject HomepagePannel;
    public GameObject LoginSignUpPannel;
    public GameObject DashbordPannel;
    public GameObject SearchPannel;
    public GameObject ProfilePannel;
    public GameObject FormPannel;
    public GameObject PropertyDetailOpenFromSearchPannel;

   
    string imagesURL;
    void Start()
    {
        saveload.Load();
        ButtonPannel.SetActive(true);
        ActivatePanel(HomepagePannel.name);

        imagesURL = "https://kreasarapps.000webhostapp.com/images/";
        UpdateTopDetails();
        gameObject.GetComponent<ManagerMyPropertyData>().GetRentDataFromServer();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PropertyDetailOpenFromSearchPannel.activeSelf == true)
            {
                ActivatePanel(SearchPannel.name);
            }
            else if (SearchPannel.activeSelf == true)
            {
                ActivatePanel(HomepagePannel.name);
            }
            else if (DashbordPannel.activeSelf == true)
            {
                ActivatePanel(HomepagePannel.name);
            }
            else if (ProfilePannel.activeSelf == true)
            {
                ActivatePanel(HomepagePannel.name);
            }
            else if (FormPannel.activeSelf == true)
            {
                ActivatePanel(DashbordPannel.name);
            }
        }
 
    }


    #region pannelswitch

    [Header("Button Pannels Effect")]
    public GameObject HomeButtonImage;
    public GameObject DashbordButtonImage;
    public GameObject HomuButtonText;
    public GameObject DashBordButtonText;

    public void AddPropertyButton()
    {
        ActivatePanel(FormPannel.name);
        gameObject.GetComponent<ManagerMyPropertyData>().OnFormPageEnabel();
        RentPannel.SetActive(true);
    }

    

    public void LoginPannelButton()
    {
        LoginPannel.SetActive(true);
        SignUpPannel.SetActive(false);
    }
    public void SignUpPannelButton()
    {
        SignUpPannel.SetActive(true);
        LoginPannel.SetActive(false);
    }

    public void LoginMainPannelButton()
    {
        ActivatePanel(LoginSignUpPannel.name);
        LoginPannel.SetActive(true);
        SignUpPannel.SetActive(false);
    }

    public void SearchPannelButton()
    {
        ActivatePanel(SearchPannel.name);
    }

    public void HomeButton()
    {
        ActivatePanel(HomepagePannel.name);
        HomeButtonImage.GetComponent<Image>().color = Color.white;
        DashbordButtonImage.GetComponent<Image>().color = Color.green;
        HomuButtonText.GetComponent<Text>().color = Color.green;
        DashBordButtonText.GetComponent<Text>().color = Color.white;

    }

    public void DashbordButton()
    {
        if (saveload.username != " ")
        {
            ActivatePanel(DashbordPannel.name);
            gameObject.GetComponent<ManagerMyPropertyData>().UpdateDashBord();
            gameObject.GetComponent<ManagerMyPropertyData>().GetRentDataFromServer();
            HomeButtonImage.GetComponent<Image>().color = Color.green;
            DashbordButtonImage.GetComponent<Image>().color = Color.white;
            HomuButtonText.GetComponent<Text>().color = Color.white;
            DashBordButtonText.GetComponent<Text>().color = Color.green;
        }
        else
        {
            LoginMainPannelButton();
        }
    }

    public void ProfileButton()
    {
        ActivatePanel(ProfilePannel.name);
        gameObject.GetComponent<UpdateProfile>().UpdateInputFields();

    }

    public void PropertyDetailFromSearch()
    {
        ActivatePanel(PropertyDetailOpenFromSearchPannel.name);
    }


    public void ActivatePanel(string panelToBeActivated)
    {
        HomepagePannel.SetActive(panelToBeActivated.Equals(HomepagePannel.name));
        LoginSignUpPannel.SetActive(panelToBeActivated.Equals(LoginSignUpPannel.name));
        DashbordPannel.SetActive(panelToBeActivated.Equals(DashbordPannel.name));
        SearchPannel.SetActive(panelToBeActivated.Equals(SearchPannel.name));
        ProfilePannel.SetActive(panelToBeActivated.Equals(ProfilePannel.name));
        FormPannel.SetActive(panelToBeActivated.Equals(FormPannel.name));
        PropertyDetailOpenFromSearchPannel.SetActive(panelToBeActivated.Equals(PropertyDetailOpenFromSearchPannel.name));
    }

    #endregion

    #region account

        #region createaccount

    [Header("Create Account Variables")]
    public InputField UsernameInputField;
    public InputField EmailInputField;
    public InputField PasswordInputField;

    public Text statusText;

    public void OnCreateAccountButtonClicked()
    {
        string username = UsernameInputField.text;
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        StartCoroutine(CreateAccountFromServer(username, email, password));
    }


    IEnumerator CreateAccountFromServer(string username,string email,string password)
    {
        WWWForm form1 = new WWWForm();

        form1.AddField("username", username);
        form1.AddField("email", email);
        form1.AddField("password", password);

        WWW www = new WWW(createaccounturl, form1);
        yield return www;

        print("message came"+www.text);

        
        if (www.text == "Account Already Exist")
        {
            
            statusText.text = "Account alrady Exist";
            statusText.color = Color.red;
        }
        else if (www.text == "")
        {
            statusText.text = "Check the connection";
            statusText.color = Color.red;
            
        }
        else if (www.text == "Connection Failed")
        {
            statusText.text = "Server is in the maintainence";
            statusText.color = Color.red;
            
        }
        else
        {
            if (www.text.Contains("id:"))
            {
                //account created successfully

                statusText.text = "Account created successfully";
                statusText.color = Color.green;
                
                
                //update name on the top
                int temp = www.text.IndexOf(":");
                string idinstring = www.text.Substring(temp + 1);
                int idnumber = Convert.ToInt32(idinstring);

                saveload.id = idnumber;
                saveload.username = username;
                saveload.email = email;

                saveload.Save();

                ActivatePanel(HomepagePannel.name);
                UpdateTopDetails();
            }
            else
            {
                statusText.text = "Server is in the maintainence";
                statusText.color = Color.red;
                
                
            }
        }

    }

    #endregion

        #region logout

        public void LogOutButton()
        {
            saveload.username = " ";
            saveload.email = " ";
            saveload.profilepicName = " ";
            saveload.id = 0;
            saveload.Save();
            UpdateTopDetails();
            ActivatePanel(HomepagePannel.name);
        }

        #endregion

        #region login

        public InputField EmailLoginInputText;
        public InputField PasswordLoginInputText;

        public void OnLogInButtonClicked()
        {
            string email = EmailLoginInputText.text;
            string password = PasswordLoginInputText.text;

            StartCoroutine(LoginAccountFromServer(email, password));

            
        }


        IEnumerator LoginAccountFromServer(string email, string password)
        {
            WWWForm form1 = new WWWForm();

            form1.AddField("email", email);
            form1.AddField("password", password);

            WWW www = new WWW(loginaccounturl, form1);
            yield return www;

            print("message came" + www.text);

            if (www.text == "Incorrect password")
            {
                statusText.text = "Incorrect password";
                statusText.color = Color.red;
            }
            else if (www.text == "server error")
            {
                statusText.text = "Server is in the maintainence";
                statusText.color = Color.red;
            }
            else if (www.text == "")
            {
                statusText.text = "Check the connection";
                statusText.color = Color.red;

            }
            else
            {
                if (www.text.Contains("id:"))
                {
                    //account created successfully

                    statusText.text = "Account created successfully";
                    statusText.color = Color.green;


                    //update name on the top
                    //extracting username

                    string username = GetDataValue(www.text, "username:");

                    print(username);

                    //extracting id number

                    string idinstring = GetDataValue(www.text, "id:");
                    int idnumber = Convert.ToInt32(idinstring);
                    print(idnumber);
                    saveload.id = idnumber;
                    saveload.username = username;
                    saveload.email = email;

                    saveload.Save();

                    ActivatePanel(HomepagePannel.name);
                    UpdateTopDetails();
                    EmailLoginInputText.text = "";
                    PasswordLoginInputText.text = "";
                }
                else
                {
                    statusText.text = "Server is in the maintainence";
                    statusText.color = Color.red;


                }
            }

        }

        #endregion

    #endregion

    #region topprofiledetails

        
        public void UpdateTopDetails()
        {
        //check if id exist then update name
        saveload.Load();
        if (saveload.username != " ")
        {
            NamePannelTop.SetActive(true);
            LoginButtonTop.SetActive(false);
            NameTop.text = saveload.username;

            if (saveload.profilepicName != " ")
            {
                //upload photo from phone
                print(saveload.profilepicName);
                StartCoroutine(DownloadImage(ProfilePicImage,saveload.profilepicName));
            }
            else
            {
                //use defaultphoto
                print(saveload.profilepicName);
                ProfilePicImage.GetComponent<Image>().sprite = ProfilePicTop;
            }

        }
        else
        {
            NamePannelTop.SetActive(false);
            LoginButtonTop.SetActive(true);
        }
    }
        #endregion

        #region propertytype

    [Header("Form Page Form Selection")]
        public Dropdown FormTypeDropDown;

    public GameObject RentPannel;
    public GameObject HotelPannel;

        public void GetFormTypeDropdown()
        {
            string formType=FormTypeDropDown.options[FormTypeDropDown.value].text;

            if (formType == "Rent / Hostel")
            {
                RentPannel.SetActive(true);
                HotelPannel.SetActive(false);
            }
            else if (formType == "Hotel")
            {
                RentPannel.SetActive(false);
                HotelPannel.SetActive(true);
            }
        }


        #endregion

        IEnumerator DownloadImage(GameObject tempGO, string name)
        {

            //for(int i=0;i<imagenames.Length;i++)
            //{

            //fileName = imagenames[i];
            string fileName;
            print(name);
            fileName = name;
            //print("ImageURL:" + imagesURL + fileName);
            WWW www = new WWW(imagesURL + fileName);
            yield return www;
            Texture2D texture = www.texture;
            //this.GetComponent<Renderer>().material.mainTexture = texture;
            //byte[] bytes = texture.EncodeToJPG();
            //File.WriteAllBytes(Application.persistentDataPath + uploadfileScript.fileName, bytes);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            tempGO.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
            //rentimages[i].transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
            //}

        }

        string GetDataValue(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);
            if (value.Contains("|"))
                value = value.Remove(value.IndexOf("|"));
            return value;
        }

}

// add dropdownOptions
//Maindropdown.options.Add (new Dropdown.OptionData() {text=c});