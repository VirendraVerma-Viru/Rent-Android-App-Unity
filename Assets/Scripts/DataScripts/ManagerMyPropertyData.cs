using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class ManagerMyPropertyData : MonoBehaviour
{
    [Header("Dash Bord Status Text")]
    public Text StatusText;
    public Text DashBordStatusText;
    private string rentInsertURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/rentInsert.php";
    private string rentGetDataURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/rentgetdata.php";
    private string rentUpdateURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/rentupdate.php";
    private string rentDeleteURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/rentdelete.php";
    
    private bool getmoreimages = true;

    WWW rentlocalfile1;
    WWW rentlocalfile2;
    WWW rentlocalfile3;
    WWW rentlocalfile4;
    WWW rentlocalfile5;

    GameObject[] rentimages;
    string[] imagenames;
    public Camera camera;

    List<MyPropertyData> myproperty = new List<MyPropertyData>();
    int id = 0;
    string propertyname="No Name";
       string onlyFor;
       int rentPrice=0;
       string rentPriceSelect;
       string localaddress="No Address";
       string cityaddress;
       string countryaddress;
       string landmark="No Landmark";
       string furnishing;
       int rooms=1;
       int kitchen=0;
       int bathroom=0;
       int balcony=0;
       int carparking=0;
       int bikeParking=0;
       string waterAvalibility;
       string electricity;
       string flooring;
       int maintainenceCharge=0;
       string maintainenceChargeSelect;
       int securityCharge=0;
       string securityChargeSelect;
       string facing;
       string overlooking;
       int floor=0;
       string description="No Description";
       string pic1 = " ";
       string pic2 = " ";
       string pic3 = " ";
       string pic4 = " ";
       string pic5 = " ";
    
    

    //---------------------------variables for rent form-----------
    [Header("Rent Form Page Details")]
       public InputField PropertyNameInputField;
       public Dropdown OnlyForDropDown;
       public InputField RentPriceInputField;
       public Dropdown RentPriceDropDown;
       public InputField AddressInputField;
       public Dropdown CityDropDown;
       public Dropdown CountryDropDown;
       public InputField LandMarkInputField;
       public Dropdown FurnishingDropDown;
       public Dropdown RoomsDropDown;
       public InputField KitchenInputField;
       public InputField BathroomInputField;
       public InputField BalconyInputField;
       public InputField CarParkingInputField;
       public InputField BikeParkingInputField;
       public Dropdown WaterAvalabilityDropDown;
       public Dropdown ElectricityDropDown;
       public Dropdown FloringDropDown;
       public InputField MaintainenceInputField;
       public Dropdown MaintainenceDropDown;
       public InputField SecurityChargeInputField;
       public Dropdown SecurityDropDown;
       public Dropdown FacingDropDown;
       public Dropdown OverLookingDropDown;
       public InputField FloorInputField;
       public InputField DescriptionInputField;

       public GameObject SaveButton;

    //-------------------some extra variables------------------
       Vector2 mouseStartPos;
       Vector2 mouseEndPos;

    void Start()
    {
        StatusText.text = " ";
        StatusText.color = Color.red;
        imagesURL = "https://kreasarapps.000webhostapp.com/images/";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)&&(gameObject.GetComponent<MainAppManager>().DashbordPannel))
        {
            mouseEndPos = Input.mousePosition;

            if (gameObject.GetComponent<MainAppManager>().DashbordPannel.activeSelf && Vector2.Distance(mouseEndPos, mouseStartPos) < 20)
            {
                //RaycastHit hit;
                //Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                PointerEventData cursor = new PointerEventData(EventSystem.current);                            // This section prepares a list for all objects hit with the raycast
                cursor.position = Input.mousePosition;
                List<RaycastResult> objectsHit = new List<RaycastResult>();
                EventSystem.current.RaycastAll(cursor, objectsHit);
                int count = objectsHit.Count;
                int x = 0;

                var goname = objectsHit[0];
                //print(goname.ToString());

                string a = GetDataValue(goname.ToString(), "Name:");

                foreach (MyPropertyData m in myproperty)
                {
                    
                    if (a.Contains(m.PropertyName))
                    {
                       
                        //show the details on other page
                        //open property detail pannel
                        gameObject.GetComponent<MainAppManager>().AddPropertyButton();
                        //now show the information
                        EditPropertyDetails(m.PropertyName);
                    }
                }

            }
            // print(goname);
        }
    }

    public void OnFormPageEnabel()
    {

        id = 0;
        propertyname = "No Name";
        rentPrice = 0;
        localaddress = "No Address";
        landmark = "No Landmark";
        rooms = 1;
        kitchen = 0;
        bathroom = 0;
        balcony = 0;
        carparking = 0;
        bikeParking = 0;
        maintainenceCharge = 0;
        securityCharge = 0;
        floor = 0;
        description = "No Description";
        pic1 = " ";
        pic2 = " ";
        pic3 = " ";
        pic4 = " ";
        pic5 = " ";

        //reset all the input field 
        PropertyNameInputField.text = "";
        RentPriceInputField.text = "";
        AddressInputField.text = "";
        LandMarkInputField.text = "";
        KitchenInputField.text = "";
        BathroomInputField.text = "";
        BalconyInputField.text = "";
        CarParkingInputField.text = "";
        BikeParkingInputField.text = "";
        MaintainenceInputField.text = "";
        SecurityChargeInputField.text = "";
        FloorInputField.text = "";
        DescriptionInputField.text = "";
        SaveButton.SetActive(true);

    }

    #region save button pressed

    public void SaveRentForm()// when save button clicked
    {
       
        propertyname = PropertyNameInputField.text;
        onlyFor = OnlyForDropDown.options[OnlyForDropDown.value].text;
        if (RentPriceInputField.text!="")
        rentPrice = Convert.ToInt32(RentPriceInputField.text);
        rentPriceSelect = RentPriceDropDown.options[RentPriceDropDown.value].text;
        if (AddressInputField.text!="")
        localaddress = AddressInputField.text;
        cityaddress = CityDropDown.options[CityDropDown.value].text;
        countryaddress = CountryDropDown.options[CountryDropDown.value].text;
        if (LandMarkInputField.text != "")
        landmark = LandMarkInputField.text;
        furnishing = FurnishingDropDown.options[FurnishingDropDown.value].text;
        if (RoomsDropDown.options[RoomsDropDown.value].text != "")
            rooms = Convert.ToInt32(RoomsDropDown.options[RoomsDropDown.value].text);
        if(KitchenInputField.text!="")
        kitchen =Convert.ToInt32(KitchenInputField.text);
        if (BathroomInputField.text!="")
        bathroom = Convert.ToInt32(BathroomInputField.text);
        if (BalconyInputField.text!="")
        balcony = Convert.ToInt32(BalconyInputField.text);
        if (CarParkingInputField.text!="")
        carparking = Convert.ToInt32(CarParkingInputField.text);
        if (BikeParkingInputField.text!="")
        bikeParking = Convert.ToInt32(BikeParkingInputField.text);
        waterAvalibility = WaterAvalabilityDropDown.options[WaterAvalabilityDropDown.value].text;
        electricity = ElectricityDropDown.options[ElectricityDropDown.value].text;
        flooring = FloringDropDown.options[FloringDropDown.value].text;
        if (MaintainenceInputField.text!="")
        maintainenceCharge = Convert.ToInt32(MaintainenceInputField.text);
        maintainenceChargeSelect = MaintainenceDropDown.options[MaintainenceDropDown.value].text;
        if (SecurityChargeInputField.text!="")
        securityCharge = Convert.ToInt32(SecurityChargeInputField.text);
        securityChargeSelect = SecurityDropDown.options[SecurityDropDown.value].text;
        facing = FacingDropDown.options[FacingDropDown.value].text;
        overlooking = OverLookingDropDown.options[OverLookingDropDown.value].text;
        if (FloorInputField.text!="")
        floor = Convert.ToInt32(FloorInputField.text);
        if (DescriptionInputField.text != "")
        description = DescriptionInputField.text;


        string tempPropertyName = propertyname;
        bool foundname = false;
        foreach (MyPropertyData property in myproperty)
        {
            if (tempPropertyName == property.PropertyName)
            {
                foundname = true;
            }
        }

        if (id == 0)
        {
            print("insreting");
            myproperty.Add(new MyPropertyData(id, propertyname, onlyFor,
            rentPrice,
            rentPriceSelect,
            localaddress,
            cityaddress,
            countryaddress,
            landmark,
            furnishing,
            rooms,
            kitchen,
            bathroom,
            balcony,
            carparking,
            bikeParking,
            waterAvalibility,
            electricity,
            flooring,
            maintainenceCharge,
            maintainenceChargeSelect,
            securityCharge,
            securityChargeSelect,
            facing,
            overlooking,
            floor,
            description, pic1, pic2, pic3, pic4, pic5));

            //open dashbord and update to server
            SaveButton.SetActive(false);
            StatusText.text = "Connecting...";
            StatusText.color = Color.green;
            StartCoroutine(InsertRentData());
            

        }
        else
        {
            print("Updating");
            //update
            StatusText.text = "Connecting...";
            StatusText.color = Color.green;
            StartCoroutine(UpdateRentData());
        }
         
        print(id+" "+propertyname + " " + onlyFor + " " +
        rentPrice + " " +
        rentPriceSelect + " " +
        localaddress + " " +
        cityaddress + " " +
        countryaddress + " " +
        landmark + " " +
        furnishing + " " +
        kitchen + " " +
        bathroom + " " +
        balcony + " " +
        carparking + " " +
        bikeParking + " " +
        waterAvalibility + " " +
        electricity + " " +
        flooring + " " +
        maintainenceCharge + " " +
        maintainenceChargeSelect + " " +
        securityCharge + " " +
        securityChargeSelect + " " +
        facing + " " +
        overlooking + " " +
        floor + " " +
        description + " " + pic1 + " " + pic2 + " " + pic3 + " " + pic4 + " " + pic5);
         
    }

    #region for pics upload

    [Header("For Uploading Pics Image")]
    public GameObject PicImagePlace1;
    public GameObject PicImagePlace2;
    public GameObject PicImagePlace3;
    public GameObject PicImagePlace4;
    public GameObject PicImagePlace5;

    private GameObject go;
    public GameObject ChooseButton;

    private string m_LocalFileName = " ";
    private string m_URL;
    private string imagesURL;
    public string fileName;
    private int picNumber = 1;

    public void PickImageFromGalaryButtonRentForm()
    {
        imagesURL = "https://kreasarapps.000webhostapp.com/images/";
        m_URL = "https://kreasarapps.000webhostapp.com/PropertyBazar/uploadprofilepic.php";


        if (pic1 == " "||pic1=="No Pic")
        {
            go = PicImagePlace1;
            picNumber = 1;
        }
        else if (pic2 == " " || pic2 == "No Pic")
        {
            go = PicImagePlace2;
            picNumber = 2;
        }
        else if (pic3 == " " || pic3 == "No Pic")
        {
            go = PicImagePlace3;
            picNumber = 3;
        }
        else if (pic4 == " " || pic4 == "No Pic")
        {
            go = PicImagePlace4;
            picNumber = 4;
        }
        else if (pic5 == " " || pic5 == "No Pic")
        {
            go = PicImagePlace5;
            picNumber = 5;
            getmoreimages = false;
            ChooseButton.SetActive(false);
        }


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
            print("short out");
            print(m_LocalFileName.LastIndexOf("/"));
            int n = m_LocalFileName.LastIndexOf("/");

            print(m_LocalFileName.Substring(n + 1));
            fileName = m_LocalFileName.Substring(n + 1);

            if (picNumber == 1)
            {
                pic1 = m_LocalFileName;
            }
            else if (picNumber == 2)
            {
                pic2 = m_LocalFileName;
            }
            else if (picNumber == 3)
            {
                pic3 = m_LocalFileName;
            }
            else if (picNumber == 4)
            {
                pic4 = m_LocalFileName;
            }
            else if (picNumber == 5)
            {
                pic5 = m_LocalFileName;
            }

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
                go.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

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
                //UploadFile();

            }
        }, "Select a PNG image", "image/png", maxSize);

        //Debug.Log("Permission result: " + permission);
    }


    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        WWW localFile = new WWW("file://" + m_LocalFileName);
        //statusText.text = "file://" + m_LocalFileName;
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
        postForm.AddField("picnumber", picNumber);
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
            StatusText.text = upload.text;
            RefreshImage();
        }
        else
        {
            Debug.Log("Error during upload: " + upload.error);
            StatusText.text = upload.text;
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
        saveload.profilepicName = saveload.id + fileName;
        saveload.Save();
        fileName = saveload.profilepicName;
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            byte[] byteArray = File.ReadAllBytes(Application.persistentDataPath + "/" + fileName);
            Texture2D texture = new Texture2D(500, 600);
            //texture.LoadImage(byteArray);
            //print("file already exist:" + Application.persistentDataPath + "/"+uploadfileScript.fileName);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            go.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);




        }
        else
        {

            print("ImageURL:" + imagesURL + fileName);
            WWW www = new WWW(imagesURL + fileName);
            yield return www;
            Texture2D texture = www.texture;
            //this.GetComponent<Renderer>().material.mainTexture = texture;
            //byte[] bytes = texture.EncodeToJPG();
            //File.WriteAllBytes(Application.persistentDataPath + uploadfileScript.fileName, bytes);

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            go.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

        }
    }

    #endregion

    IEnumerator InsertRentData()
    {
        WWWForm form1 = new WWWForm();
        saveload.Load();
        form1.AddField("id", saveload.id);
        form1.AddField("propertyName", propertyname);
        form1.AddField("onlyFor", onlyFor);
        form1.AddField("rentprice", rentPrice);
        form1.AddField("rentPriceSelect", rentPriceSelect);
        form1.AddField("localAddress", localaddress);
        form1.AddField("cityAddress", cityaddress);
        form1.AddField("countryAddress", countryaddress);
        form1.AddField("landmark", landmark);
        form1.AddField("furnishing", furnishing);
        form1.AddField("rooms", rooms);
        form1.AddField("kitchen", kitchen);
        form1.AddField("bathroom", bathroom);
        form1.AddField("balcony", balcony);
        form1.AddField("carParking", carparking);
        form1.AddField("bikeParking", bikeParking);
        form1.AddField("waterAvalability", waterAvalibility);
        form1.AddField("electricity", electricity);
        form1.AddField("flooring", flooring);
        form1.AddField("maintainence", maintainenceCharge);
        form1.AddField("maintainenceSelect", maintainenceChargeSelect);
        form1.AddField("security", securityCharge);
        form1.AddField("securitySelect", securityChargeSelect);
        form1.AddField("facing", facing);
        form1.AddField("overlooking", overlooking);
        form1.AddField("floor", floor);
        form1.AddField("description", description);


        //update pic details
        if (pic1 != "No Pic")
        {
            rentlocalfile1 = new WWW("file:///" + pic1);
            yield return rentlocalfile1;
            form1.AddBinaryData("pic1", rentlocalfile1.bytes, pic1, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile2 = new WWW("file:///" + pic2);
            yield return rentlocalfile2;
            form1.AddBinaryData("pic2", rentlocalfile2.bytes, pic2, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile3 = new WWW("file:///" + pic3);
            yield return rentlocalfile2;
            form1.AddBinaryData("pic3", rentlocalfile3.bytes, pic3, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile4 = new WWW("file:///" + pic4);
            yield return rentlocalfile4;
            form1.AddBinaryData("pic4", rentlocalfile4.bytes, pic4, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile5 = new WWW("file:///" + pic5);
            yield return rentlocalfile5;
            form1.AddBinaryData("pic5", rentlocalfile5.bytes, pic5, "text/plain");
        }
        

        WWW www = new WWW(rentInsertURL, form1);
        yield return www;

        print(www.text);

        if (www.text == "success")
        {
            gameObject.GetComponent<MainAppManager>().DashbordButton();
        }
        else if (www.text == "error")
        {
            StatusText.text = "Server is in maintainence";
            StatusText.color = Color.red;
        }
        else if (www.text == "")
        {
            StatusText.text = "Check the connection";
            StatusText.color = Color.red;
        }
        else
        {
            StatusText.text = "Server is in maintainence";
            StatusText.color = Color.red;
        }
    }

    IEnumerator UpdateRentData()
    {

        WWWForm form1 = new WWWForm();
        saveload.Load();
        form1.AddField("id", id);
        form1.AddField("propertyName", propertyname);
        form1.AddField("onlyFor", onlyFor);
        form1.AddField("rentprice", rentPrice);
        form1.AddField("rentPriceSelect", rentPriceSelect);
        form1.AddField("localAddress", localaddress);
        form1.AddField("cityAddress", cityaddress);
        form1.AddField("countryAddress", countryaddress);
        form1.AddField("landmark", landmark);
        form1.AddField("furnishing", furnishing);
        form1.AddField("rooms", rooms);
        form1.AddField("kitchen", kitchen);
        form1.AddField("bathroom", bathroom);
        form1.AddField("balcony", balcony);
        form1.AddField("carParking", carparking);
        form1.AddField("bikeParking", bikeParking);
        form1.AddField("waterAvalability", waterAvalibility);
        form1.AddField("electricity", electricity);
        form1.AddField("flooring", flooring);
        form1.AddField("maintainence", maintainenceCharge);
        form1.AddField("maintainenceSelect", maintainenceChargeSelect);
        form1.AddField("security", securityCharge);
        form1.AddField("securitySelect", securityChargeSelect);
        form1.AddField("facing", facing);
        form1.AddField("overlooking", overlooking);
        form1.AddField("floor", floor);
        form1.AddField("description", description);

        

        //update pic details
        if (pic1 != "No Pic")
        {
            rentlocalfile1 = new WWW("file:///" + pic1);
            yield return rentlocalfile1;
            form1.AddBinaryData("pic1", rentlocalfile1.bytes, pic1, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile2 = new WWW("file:///" + pic2);
            yield return rentlocalfile2;
            form1.AddBinaryData("pic2", rentlocalfile2.bytes, pic2, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile3 = new WWW("file:///" + pic3);
            yield return rentlocalfile2;
            form1.AddBinaryData("pic3", rentlocalfile3.bytes, pic3, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile4 = new WWW("file:///" + pic4);
            yield return rentlocalfile4;
            form1.AddBinaryData("pic4", rentlocalfile4.bytes, pic4, "text/plain");
        }
        if (pic1 != "No Pic")
        {
            rentlocalfile5 = new WWW("file:///" + pic5);
            yield return rentlocalfile5;
            form1.AddBinaryData("pic5", rentlocalfile5.bytes, pic5, "text/plain");
        }
        

        

        
        WWW www = new WWW(rentUpdateURL, form1);
        yield return www;

        

        if (www.text == "success")
        {
            gameObject.GetComponent<MainAppManager>().DashbordButton();
        }
        else if (www.text == "error")
        {
            StatusText.text = "Server is in maintainence";
            StatusText.color = Color.red;
        }
        else if (www.text == "")
        {
            StatusText.text = "Check the connection";
            StatusText.color = Color.red;
        }
        else
        {
            StatusText.text = "Server is in maintainence";
            StatusText.color = Color.red;
        }
    }

    #endregion

    #region update dashbord

    [Header("Dashbord variables")]
    public GameObject PropertyBar;
   
    public Transform ContentPlace;

    public void UpdateDashBord()
    {
        
        GameObject[] temp;
        temp=GameObject.FindGameObjectsWithTag("PropertyData");
        foreach (GameObject go in temp)
        {
            Destroy(go);
        }
        //first of all upate from the server
        

        //check locally
        if (myproperty.Count > 0)
        {
            rentimages = new GameObject[myproperty.Count];
            imagenames = new string[myproperty.Count];

            int d=0;
            //means property found
            foreach (MyPropertyData property in myproperty)
            {
                // print(guy.cityName + " " + guy.population);

                GameObject go = Instantiate(PropertyBar);
                go.transform.SetParent(ContentPlace.transform);
                go.transform.localScale = Vector3.one;
                go.transform.Find("Details").transform.Find("PropertyName").GetComponent<Text>().text = property.PropertyName;
                go.transform.Find("Details").transform.Find("PropertyPrice").GetComponent<Text>().text = "Price:" + property.RentPrice.ToString();

                string description = " ";
                if (property.Rooms > 0)
                    description += property.Rooms.ToString() + "Room";
                if (property.Kitchen > 0)
                    description += "," + property.Kitchen.ToString() + "Kitchen";
                if (property.Bathroom > 0)
                    description += "," + property.Bathroom.ToString() + "Bathroom";
                if (property.Balcony > 0)
                    description += "," + property.Balcony.ToString() + "Balcony";
                if (property.Carparking > 0)
                    description += "," + property.Carparking.ToString() + "Carparking";
                if (property.BikeParking > 0)
                    description += "," + property.BikeParking.ToString() + "BikeParking";


                go.transform.Find("Details").transform.Find("PropertyAddress").GetComponent<Text>().text = property.Localaddress + "," + property.Cityaddress + "," + property.Countryaddress;
                go.transform.Find("Details").transform.Find("PropertyDescriptiion").GetComponent<Text>().text = description;
                go.transform.Find("ButtonName").GetComponent<Image>().name = property.PropertyName;
                //update pic of rents
                if(property.Pic1!="No Pic")
                {
                    //rentimages[d] = go;
                    //imagenames[d] = property.Pic1;
                    //d++;
                    RefreshDashBordRentImage(go, property.Pic1);
                }
            }
            //RefreshDashBordRentImage();

        }
        
    }

    public void RefreshDashBordRentImage(GameObject tempGO,string name)
    {
        StartCoroutine(RefreshRentImage(tempGO, name));
    }

    IEnumerator RefreshRentImage(GameObject tempGO, string name)
    {

        //for(int i=0;i<imagenames.Length;i++)
        //{

        //fileName = imagenames[i];
        fileName = name;
        //print("ImageURL:" + imagesURL + fileName);
        WWW www = new WWW(imagesURL + fileName);
        yield return www;
        Texture2D texture = www.texture;
        //this.GetComponent<Renderer>().material.mainTexture = texture;
        //byte[] bytes = texture.EncodeToJPG();
        //File.WriteAllBytes(Application.persistentDataPath + uploadfileScript.fileName, bytes);

        Rect rec = new Rect(0, 0, texture.width, texture.height);
        tempGO.transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        //rentimages[i].transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        //}

    }

    #region get rent data from server

    public void GetRentDataFromServer()
    {
        DashBordStatusText.text = "Loading...";
        DashBordStatusText.color = Color.green;
        StartCoroutine(GetRentData());
    }

    IEnumerator GetRentData()
    {
        
        WWWForm form1 = new WWWForm();
        saveload.Load();
        form1.AddField("id", saveload.id);
        WWW www = new WWW(rentGetDataURL, form1);
        yield return www;

        DashBordStatusText.text = " ";
        DashBordStatusText.color = Color.green;
        
        myproperty.Clear();

        string[] items;
        items = www.text.Split(';');

        for (int i = 0; i < items.Length-1; i++)
        {
            id = Convert.ToInt32(GetDataValue(items[i], "Id:"));
            propertyname =GetDataValue(items[i],"PropertyName:");
            onlyFor = GetDataValue(items[i], "OnlyFor:");
            rentPrice = Convert.ToInt32(GetDataValue(items[i], "RentPrice:"));
            rentPriceSelect = GetDataValue(items[i], "RentPriceSelect:");
            localaddress = GetDataValue(items[i], "LocalAddress:");
            cityaddress = GetDataValue(items[i], "CityAddress:");
            countryaddress = GetDataValue(items[i], "CountryAddress:");
            landmark = GetDataValue(items[i], "Landmark:");
            furnishing = GetDataValue(items[i], "Furnishing:");
            rooms = Convert.ToInt32(GetDataValue(items[i], "Rooms:"));
            kitchen = Convert.ToInt32(GetDataValue(items[i], "Kitchen:"));
            bathroom = Convert.ToInt32(GetDataValue(items[i], "Bathroom:"));
            balcony = Convert.ToInt32(GetDataValue(items[i], "Balcony:"));
            carparking = Convert.ToInt32(GetDataValue(items[i], "CarParking:"));
            bikeParking = Convert.ToInt32(GetDataValue(items[i], "BikeParking:"));
            waterAvalibility = GetDataValue(items[i], "WaterAvalablity:");
            electricity = GetDataValue(items[i], "Electricity:");
            flooring = GetDataValue(items[i], "Flooring:");
            maintainenceCharge = Convert.ToInt32(GetDataValue(items[i], "Maintenance:"));
            maintainenceChargeSelect = GetDataValue(items[i], "MaintenanceSelect:");
            securityCharge = Convert.ToInt32(GetDataValue(items[i], "Security:"));
            securityChargeSelect = GetDataValue(items[i], "SecuritySelect:");
            facing = GetDataValue(items[i], "Facing:");
            overlooking = GetDataValue(items[i], "OverLooking:");
            floor = Convert.ToInt32(GetDataValue(items[i], "Floor:"));
            description = GetDataValue(items[i], "Description:");
            pic1 = GetDataValue(items[i], "Pic1:");
            pic2 = GetDataValue(items[i], "Pic2:");
            pic3 = GetDataValue(items[i], "Pic3:");
            pic4 = GetDataValue(items[i], "Pic4:");
            pic5 = GetDataValue(items[i], "Pic5:");
            //------------------------------------------------------------------------------add update detaiils from server pic5--------------------
            
            myproperty.Add(new MyPropertyData(id,propertyname, onlyFor,
            rentPrice,
            rentPriceSelect,
            localaddress,
            cityaddress,
            countryaddress,
            landmark,
            furnishing,
            rooms,
            kitchen,
            bathroom,
            balcony,
            carparking,
            bikeParking,
            waterAvalibility,
            electricity,
            flooring,
            maintainenceCharge,
            maintainenceChargeSelect,
            securityCharge,
            securityChargeSelect,
            facing,
            overlooking,
            floor,
            description,pic1,pic2,pic3,pic4,pic5));

            

        }

       
        UpdateDashBord();
       

    }

    #endregion

    #endregion

    #region dashbord edit

            
            void EditPropertyDetails(string name)
            {
                
                List<string> list;
                int i;
                StatusText.text = " ";
                StatusText.color = Color.red;
                string value;

                foreach (MyPropertyData m in myproperty)
                {
                    
                    if (m.PropertyName == name)
                    {
                        id = m.Id;
                        print("Editid:" + id);
                        //--propertyname--
                        PropertyNameInputField.text=m.PropertyName;
                        propertyname = m.PropertyName;

                        //--only for dropdown--
                        onlyFor = m.OnlyFor;
                        list = new List<string> { "Family", "Boys","Girls","Anyone" };
                        i=list.IndexOf(m.OnlyFor);
                        list.RemoveAt(i);
                        list.Insert(0, m.OnlyFor);
                        var dropdown = OnlyForDropDown;
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //---rent price
                        rentPrice = m.RentPrice;
                        RentPriceInputField.text =m.RentPrice.ToString();

                        //rent price dropdown
                        rentPriceSelect = m.RentPriceSelect;
                        list = new List<string> { "Monthly", "Daily", "Yearly" };
                        value = m.RentPriceSelect;
                        dropdown = RentPriceDropDown;
                        
                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();


                        //---address
                        localaddress = m.Localaddress;
                        AddressInputField.text = m.Localaddress;

                        //----Landmark
                        landmark = m.Landmark;
                        LandMarkInputField.text = m.Landmark;

                        //Furnishing--dropdown
                        furnishing = m.Furnishing;
                        list = new List<string> { "Unfurnished", "Furnished", "Semi-furnished" };
                        value = m.Furnishing;
                        dropdown = FurnishingDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //rooms dropdown
                        rooms = m.Rooms;
                        list = new List<string> { "1", "2", "3","4","5" };
                        value = m.Rooms.ToString();
                        dropdown = RoomsDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //kitchen
                        kitchen = m.Kitchen;
                        KitchenInputField.text = m.Kitchen.ToString();

                        //bathroom
                        bathroom = m.Bathroom;
                        BathroomInputField.text = m.Bathroom.ToString();

                        //bathroom
                        balcony = m.Balcony;
                        BalconyInputField.text = m.Balcony.ToString();

                        //bathroom
                        carparking = m.Carparking; 
                        CarParkingInputField.text = m.Carparking.ToString();

                        //bathroom
                        bikeParking = m.BikeParking;
                        BikeParkingInputField.text = m.BikeParking.ToString();


                        //water availability dropdown
                        waterAvalibility = m.WaterAvalibility;
                        list = new List<string> { "24 hours", "20 hours", "18 hours", "Sometime" };
                        value = m.WaterAvalibility;
                        dropdown = WaterAvalabilityDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //Electricity dropdown
                        electricity = m.Electricity;
                        list = new List<string> { "Rare power cut", "20 hours", "18 hours", "Come Sometime" };
                        value = m.Electricity;
                        dropdown = ElectricityDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //Flooring dropdown
                        flooring = m.Flooring;
                        list = new List<string> { "Marble", "Tiles", "Cemented", "Ground" };
                        value = m.Flooring;
                        dropdown = FloringDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //maintainence charge
                        maintainenceCharge = m.MaintainenceCharge;
                        MaintainenceInputField.text = m.MaintainenceCharge.ToString();

                        //maintainence charge dropdown
                        maintainenceChargeSelect = m.MaintainenceChargeSelect;
                        list = new List<string> { "Monthly", "Daily", "Yearly", "Once" };
                        value = m.MaintainenceChargeSelect;
                        dropdown = MaintainenceDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();


                        //maintainence charge
                        securityCharge = m.SecurityCharge;
                        SecurityChargeInputField.text = m.SecurityCharge.ToString();

                        //maintainence charge dropdown
                        securityChargeSelect = m.SecurityChargeSelect;
                        list = new List<string> { "Monthly", "Daily", "Yearly", "Once" };
                        value = m.SecurityChargeSelect;
                        dropdown = SecurityDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();


                        //Facing dropdown
                        facing = m.Facing;
                        list = new List<string> { "South-East", "South", "South-West", "West", "North-West", "North", "North-East", "East" };
                        value = m.Facing;
                        dropdown = FacingDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //Overlooking dropdown
                        overlooking = m.Overlooking;
                        list = new List<string> { "Park", "Main-Road", "Appartment", "Shop", "Building", "Farm", "Jungle" };
                        value = m.Overlooking;
                        dropdown = OverLookingDropDown;

                        i = list.IndexOf(value);
                        list.RemoveAt(i);
                        list.Insert(0, value);
                        dropdown.options.Clear();
                        foreach (string option in list)
                        {
                            dropdown.options.Add(new Dropdown.OptionData(option));
                        }
                        list.Clear();

                        //floor
                        floor = m.Floor;
                        FloorInputField.text = m.Floor.ToString();

                        //description
                        description = m.Description;
                        DescriptionInputField.text = m.Description;


                        //for images
                        //now fetching images of property
                        pic1 = m.Pic1;
                        pic2= m.Pic2;
                        pic3 = m.Pic3;
                        pic4 = m.Pic4;
                        pic5 = m.Pic5;
                        
                        if (m.Pic1 != "No Pic")
                        {
                            StartCoroutine(DownloadImage(PicImagePlace1, m.Pic1));
                        }
                        if (m.Pic2 != "No Pic")
                        {
                            StartCoroutine(DownloadImage(PicImagePlace2, m.Pic2));
                        }
                        if (m.Pic3 != "No Pic")
                        {
                            StartCoroutine(DownloadImage(PicImagePlace3, m.Pic3));
                        }
                        if (m.Pic4 != "No Pic")
                        {
                            StartCoroutine(DownloadImage(PicImagePlace4, m.Pic4));
                        }
                        if (m.Pic5 != "No Pic")
                        {
                            StartCoroutine(DownloadImage(PicImagePlace5, m.Pic5));
                        }

                        //PicImagePlace1

                    }
                }
            }

   #endregion

    #region delete form page

            public void DeleteRentFormPageButtonPressed()
            {
                
                if (id > 0)
                {
                    StartCoroutine(DeleteFormPage());
                }
                else
                {
                    gameObject.GetComponent<MainAppManager>().DashbordButton();
                }
            }

            IEnumerator DeleteFormPage()
            {
                WWWForm form1 = new WWWForm();
                form1.AddField("id", id);
                 WWW www = new WWW(rentDeleteURL, form1);
                yield return www;

                
                print(www.text);

                if (www.text == "success")
                {
                    gameObject.GetComponent<MainAppManager>().DashbordButton();
                }
                else if (www.text == "error")
                {
                    StatusText.text = "Server is in maintainence";
                    StatusText.color = Color.red;
                }
                else if (www.text == "")
                {
                    StatusText.text = "Check the connection";
                    StatusText.color = Color.red;
                }
                else
                {
                    StatusText.text = "Server is in maintainence";
                    StatusText.color = Color.red;
                }

            }

            #endregion

    public void DeleteImageButton(int picid)
            {
                if (picid == 1)
                {
                    pic1 = "No Pic";
                    PicImagePlace1.GetComponent<Image>().sprite = null;
                }
                else if (picid == 2)
                {
                    pic2 = "No Pic";
                    PicImagePlace2.GetComponent<Image>().sprite = null;
                }
                else if (picid == 3)
                {
                    pic3 = "No Pic";
                    PicImagePlace3.GetComponent<Image>().sprite = null;
                }
                else if (picid == 4)
                {
                    pic4 = "No Pic";
                    PicImagePlace4.GetComponent<Image>().sprite = null;
                }
                else if (picid == 5)
                {
                    pic5= "No Pic";
                    PicImagePlace5.GetComponent<Image>().sprite = null;
                }
            }

    IEnumerator DownloadImage(GameObject tempGO, string name)
            {

                //for(int i=0;i<imagenames.Length;i++)
                //{

                //fileName = imagenames[i];
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
