using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ManagerHotelData : MonoBehaviour
{
	List<HotelData> hotelData = new List<HotelData>();
    int ID;
    string HotelName;
    int Price=0;
    string PriceSelect;
    string Address;
    string Landmark;
    string Rooms;
    string ParkingFacility;
    string FreeWifi;
    string CardPayment;
    string BanquitHall;
    string TV;
    string AC;
    string ConferenceHall;
    string CCTVCamera;
    string Description;
    string Pic1;
    string Pic2;
    string Pic3;
    string Pic4;
    string Pic5;



    #region save button pressed

    public InputField HotelnameInfutField;
    public InputField PriceInputfield;
    public Dropdown PriceSelectInputField;
    public InputField AddressInputField;
    public InputField LandMarkInputField;
    public InputField RoomInputField;
    public InputField DescriptionInputField;

    #region checkbox form buttons

    public Sprite checkBoxTicSprite;
	public Sprite checkBoxEmptySprite;

	public Image ParkingImage1;
	public Image FreeWifiImage2;
	public Image CardPaymentImage3;
	public Image BanquitHallImage4;
	public Image TVImage5;
	public Image ACImage6;
	public Image ConferenceRoomImage7;
	public Image CCTVCameraImage8;

	 bool parking = false;
	public void ParkingFacilityButtonPressed()
	{
        //change icon
        if (parking == false)
        {
            parking = true;
            ParkingImage1.sprite = checkBoxTicSprite;
            ParkingFacility = "yes";
        }
        else
        {
            parking = false;
            ParkingImage1.sprite = checkBoxEmptySprite;
            ParkingFacility = "no";
        }
	}

     bool wifi = false;
    public void FreeWifiButtonPressed()
    {
        //change icon
        if (wifi == false)
        {
            wifi = true;
            FreeWifiImage2.sprite = checkBoxTicSprite;
            FreeWifi = "yes";
        }
        else
        {
            wifi = false;
            FreeWifiImage2.sprite = checkBoxEmptySprite;
            FreeWifi = "no";
        }
    }

     bool cardPayment = false;
    public void CardPaymentButtonPressed()
    {
        //change icon
        if (cardPayment == false)
        {
            cardPayment = true;
            CardPaymentImage3.sprite = checkBoxTicSprite;
            CardPayment = "yes";
        }
        else
        {
            cardPayment = false;
            CardPaymentImage3.sprite = checkBoxEmptySprite;
            CardPayment = "no";
        }
    }

     bool banquitHall = false;
    public void BanquitHallButtonPressed()
    {
        //change icon
        if (banquitHall == false)
        {
            banquitHall = true;
            BanquitHallImage4.sprite = checkBoxTicSprite;
            BanquitHall = "yes";
        }
        else
        {
            banquitHall = false;
            BanquitHallImage4.sprite = checkBoxEmptySprite;
            BanquitHall = "no";
        }
    }

     bool tv = false;
    public void TVButtonPressed()
    {
        //change icon
        if (tv == false)
        {
            tv = true;
            TVImage5.sprite = checkBoxTicSprite;
            TV = "yes";
        }
        else
        {
            tv = false;
            TVImage5.sprite = checkBoxEmptySprite;
            TV = "no";
        }
    }

     bool ac = false;
    public void ACVButtonPressed()
    {
        //change icon
        if (ac == false)
        {
            ac = true;
            ACImage6.sprite = checkBoxTicSprite;
            AC = "yes";
        }
        else
        {
            ac = false;
            ACImage6.sprite = checkBoxEmptySprite;
            AC = "no";
        }
    }

     bool conferenceroom = false;
    public void ConferenceRoomButtonPressed()
    {
        //change icon
        if (conferenceroom == false)
        {
            conferenceroom = true;
            ConferenceRoomImage7.sprite = checkBoxTicSprite;
            ConferenceHall = "yes";
        }
        else
        {
            conferenceroom = false;
            ConferenceRoomImage7.sprite = checkBoxEmptySprite;
            ConferenceHall = "no";
        }
    }

     bool cctvCamera = false;
    public void CCTVCameraButtonPressed()
    {
        //change icon
        if (cctvCamera == false)
        {
            cctvCamera = true;
            CCTVCameraImage8.sprite = checkBoxTicSprite;
            CCTVCamera = "yes";
        }
        else
        {
            cctvCamera = false;
            CCTVCameraImage8.sprite = checkBoxEmptySprite;
            CCTVCamera = "no";
        }
    }

    #endregion

    public void SaveButtonPressed()
   {
        HotelName = HotelnameInfutField.text;
        if (PriceInputfield.text != "")
            Price = Convert.ToInt32(PriceInputfield.text);
        PriceSelect= PriceSelectInputField.options[PriceSelectInputField.value].text;
        Address = AddressInputField.text;
        Landmark = LandMarkInputField.text;
        Rooms = RoomInputField.text;
        Description = DescriptionInputField.text;

        

        hotelData.Add(new HotelData(ID, HotelName, Price, PriceSelect, Address, Landmark, Rooms,
                    ParkingFacility, FreeWifi, CardPayment, BanquitHall, TV, AC,
                    ConferenceHall, CCTVCamera, Description, Pic1, Pic2, Pic3, Pic4, Pic5));

    }

   #endregion

}
