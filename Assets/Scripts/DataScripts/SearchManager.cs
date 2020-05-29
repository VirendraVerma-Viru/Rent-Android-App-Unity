using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Networking;

public class SearchManager : MonoBehaviour
{
    public Text StatusText;
    public Camera camera;

    private string searchURL = "https://kreasarapps.000webhostapp.com/PropertyBazar/showsearchdetail.php";
    private string imageURL = "https://kreasarapps.000webhostapp.com/images/";

    List<MyPropertyData> myproperty = new List<MyPropertyData>();
    int id=0;
    string propertyname = "No Name";
    string onlyFor;
    int rentPrice = 0;
    string rentPriceSelect;
    string localaddress = "No Address";
    string cityaddress;
    string countryaddress;
    string landmark = "No Landmark";
    string furnishing;
    int rooms = 1;
    int kitchen = 0;
    int bathroom = 0;
    int balcony = 0;
    int carparking = 0;
    int bikeParking = 0;
    string waterAvalibility;
    string electricity;
    string flooring;
    int maintainenceCharge = 0;
    string maintainenceChargeSelect;
    int securityCharge = 0;
    string securityChargeSelect;
    string facing;
    string overlooking;
    int floor = 0;
    string description = "No Description";
    string pic1 = " ";
    string pic2 = " ";
    string pic3 = " ";
    string pic4 = " ";
    string pic5 = " ";

    //---------------------------------------some important variables------------------
    public InputField SearchInputField;
    public InputField SearchInputFieldOnSearchPage;

    public GameObject PropertyBarGO;
    public Transform ContentoPlace;

    private string wordToSearch;

    
    private string imagesURL;
    Vector2 mouseStartPos;
    Vector2 mouseEndPos;
    void Start()
    {
        imagesURL = "https://kreasarapps.000webhostapp.com/images/";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)&&(gameObject.GetComponent<MainAppManager>().SearchPannel.activeSelf))
        {
            mouseEndPos= Input.mousePosition;

            if (gameObject.GetComponent<MainAppManager>().SearchPannel.activeSelf && Vector2.Distance(mouseEndPos, mouseStartPos) < 20)
            {
                //RaycastHit hit;
                //Ray ray = camera.ScreenPointToRay(Input.mousePosition);

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
                        gameObject.GetComponent<MainAppManager>().PropertyDetailFromSearch();
                        //now show the information
                        UpdatePropertyDetails(m.PropertyName);
                    }
                }

            }
            // print(goname);
        }
    }

    #region search system

    public void EnterSearchDetailButton()
    {
        StatusText.text = "Searching...";
        StatusText.color = Color.green;

        //get the input to search
        wordToSearch = SearchInputField.text;
        SearchInputFieldOnSearchPage.text = wordToSearch;

        //send the word to server which will find in the server
        //then update all the information in list 
        //then short locally in the phone
        gameObject.GetComponent<MainAppManager>().SearchPannelButton();
        StartCoroutine(GetSearchData());

    }

    IEnumerator GetSearchData()
    {
        WWWForm form1 = new WWWForm();

        form1.AddField("search", wordToSearch);

        WWW www = new WWW(searchURL, form1);
        yield return www;

        StatusText.text = " ";
        StatusText.color = Color.green;

        myproperty.Clear();

        string[] items;
        items = www.text.Split(';');

        for (int i = 0; i < items.Length - 1; i++)
        {
            id = Convert.ToInt32(GetDataValue(items[i], "Id:"));
            propertyname = GetDataValue(items[i], "PropertyName:");
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
            description, pic1, pic2, pic3, pic4, pic5));



        }
        UpdateSearchBord();
        
    }

    void UpdateSearchBord()
    {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("PropertyData");
        foreach (GameObject go in temp)
        {
            Destroy(go);
        }

        //check locally
        if (myproperty.Count > 0)
        {
           
            int d = 0;
            //means property found
            foreach (MyPropertyData property in myproperty)
            {
                // print(guy.cityName + " " + guy.population);

                GameObject go = Instantiate(PropertyBarGO);
                go.transform.SetParent(ContentoPlace.transform);
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

               // Button tempButton = go.GetComponent<Button>();
                

                
                //tempButton.GetComponent<Button>().onClick.AddListener(() => PropertyButonPressed(d));
                d++;
                //update pic of rents
                if (property.Pic1 != "No Pic")
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

    public void RefreshDashBordRentImage(GameObject tempGO, string name)
    {
        StartCoroutine(RefreshRentImage(tempGO, name));
    }

    IEnumerator RefreshRentImage(GameObject tempGO, string name)
    {
        string fileName;
        //for(int i=0;i<imagenames.Length;i++)
        //{

        //fileName = imagenames[i];
        fileName = name;
        //print("ImageURL:" + imagesURL + fileName);
        WWW www = new WWW(imageURL + fileName);
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

    #endregion


    #region open property detail

    public Text PropertyNameText;
    public Text OnlyForText;
    public Text RentPriceText;
    public Text AddressText;
    public Text LandmarkText;
    public Text FurnishingText;
    public Text RoomsText;
    public Text AdditionalText;
    public Text WaterAvailableText;
    public Text ElectricityText;
    public Text FlooringText;
    public Text MaintainanceChargeText;
    public Text SecurityChargeText;
    public Text FacingText;
    public Text OverlookingText;
    public Text FloorText;
    public Text DescriptionText;


    void UpdatePropertyDetails(string propertyname)
    {
        foreach (MyPropertyData m in myproperty)
        {
            if (propertyname == m.PropertyName)
            {
                //delete all images if exist
                GameObject[] temp = GameObject.FindGameObjectsWithTag("PropertyImages");
                foreach (GameObject g in temp)
                {
                    Destroy(g);
                }

                //now fetch and show the details
                PropertyNameText.text = m.PropertyName;
                OnlyForText.text = m.OnlyFor;
                RentPriceText.text = "$" + m.RentPrice.ToString()+" "+m.RentPriceSelect;
                AddressText.text = m.Localaddress;
                LandmarkText.text = m.Landmark;
                FurnishingText.text = m.Furnishing;
                RoomsText.text = m.Rooms.ToString();


                string description = " ";
                if (m.Rooms > 0)
                    description += m.Rooms.ToString() + "Room";
                if (m.Kitchen > 0)
                    description += "," + m.Kitchen.ToString() + "Kitchen";
                if (m.Bathroom > 0)
                    description += "," + m.Bathroom.ToString() + "Bathroom";
                if (m.Balcony > 0)
                    description += "," + m.Balcony.ToString() + "Balcony";
                if (m.Carparking > 0)
                    description += "," + m.Carparking.ToString() + "Carparking";
                if (m.BikeParking > 0)
                    description += "," + m.BikeParking.ToString() + "BikeParking";

                AdditionalText.text = description;
                WaterAvailableText.text = m.WaterAvalibility;
                ElectricityText.text = m.Electricity;
                FlooringText.text = m.Flooring;
                MaintainanceChargeText.text = m.MaintainenceCharge.ToString() +" "+ m.MaintainenceChargeSelect;
                SecurityChargeText.text = m.SecurityCharge.ToString() + " " + m.SecurityChargeSelect;
                FacingText.text = m.Facing;
                OverlookingText.text = m.Overlooking;
                FloorText.text = m.Floor.ToString() + " Floor";
                DescriptionText.text = m.Description;

                //now fetching images of property
                if (m.Pic1 != "No Pic")
                {
                    StartCoroutine(DownloadRentPropertyImages(m.Pic1));
                }
                if (m.Pic2 != "No Pic")
                {
                    StartCoroutine(DownloadRentPropertyImages(m.Pic2));
                }
                if (m.Pic3 != "No Pic")
                {
                    StartCoroutine(DownloadRentPropertyImages(m.Pic3));
                }
                if (m.Pic4 != "No Pic")
                {
                    StartCoroutine(DownloadRentPropertyImages(m.Pic4));
                }
                if (m.Pic5 != "No Pic")
                {
                    StartCoroutine(DownloadRentPropertyImages(m.Pic5));
                }
               

            }
        }
    }

    public GameObject PropertyImagePrefab;
    public Transform ImageContentPlace;

    IEnumerator DownloadRentPropertyImages(string name)
    {
        GameObject tempGO = Instantiate(PropertyImagePrefab);
        tempGO.transform.SetParent(ImageContentPlace);

        //for(int i=0;i<imagenames.Length;i++)
        //{
        string fileName;
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
        
        tempGO.GetComponent<Image>().sprite =   Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f),100);
        //rentimages[i].transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        //}

    }


    #endregion



    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }
}

