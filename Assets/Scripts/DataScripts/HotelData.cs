using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HotelData
{
   public int ID;
   public string HotelName;
   public int Price;
   public string PriceSelect;
   public string Address;
   public string Landmark;
   public string Rooms;
   public string ParkingFacility;
   public string FreeWifi;
   public string CardPayment;
   public string BanquitHall;
   public string TV;
   public string AC;
   public string ConferenceHall;
   public string CCTVCamera;
   public string Description;
   public string Pic1;
   public string Pic2;
   public string Pic3;
   public string Pic4;
   public string Pic5;

   public HotelData(int id,string hotelName,int price,string priceSelect,string address,string landmark,string rooms,
					string parkingfacility,string freewifi,string cardpayment,string banquithall,string tv,string ac,
					string conferenceHall,string cctvcamera,string description, string pic1,string pic2,string pic3,string pic4,
					string pic5)
					{
						ID=id;
						HotelName=hotelName;
						Price=price;
						PriceSelect=priceSelect;
						Address=address;
						Landmark=landmark; 
						Rooms=rooms;
						ParkingFacility=parkingfacility;
						FreeWifi=freewifi;
						CardPayment=cardpayment;
						BanquitHall=banquithall;
						TV=tv;
						AC=ac;
						ConferenceHall=conferenceHall;
						CCTVCamera=cctvcamera;
						Description=description;
						Pic1=pic1;
						Pic2=pic2;
						Pic3=pic3;
						Pic4=pic4;
						Pic5=pic5;


					}
}
