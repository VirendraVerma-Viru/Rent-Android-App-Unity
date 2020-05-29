using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyPropertyData
{
    public int Id;
    public string PropertyName;
    public string OnlyFor;
    public int RentPrice;
    public string RentPriceSelect;
    public string Localaddress;
    public string Cityaddress;
    public string Countryaddress;
    public string Landmark;
    public string Furnishing;
    public int Rooms;
    public int Kitchen;
    public int Bathroom;
    public int Balcony;
    public int Carparking;
    public int BikeParking;
    public string WaterAvalibility;
    public string Electricity;
    public string Flooring;
    public int MaintainenceCharge;
    public string MaintainenceChargeSelect;
    public int SecurityCharge;
    public string SecurityChargeSelect;
    public string Facing;
    public string Overlooking;
    public int Floor;
    public string Description;
    public string Pic1;
    public string Pic2;
    public string Pic3;
    public string Pic4;
    public string Pic5;
    

    public MyPropertyData(int id,string propertyName,string onlyFor,
       int rentPrice,
       string rentPriceSelect,
       string localaddress,
       string cityaddress,
       string countryaddress,
       string landmark,
       string furnishing,
        int rooms,
       int kitchen,
       int bathroom,
       int balcony,
       int carparking,
       int bikeParking,
       string waterAvalibility,
       string electricity,
       string flooring,
       int maintainenceCharge,
       string maintainenceChargeSelect,
       int securityCharge,
       string securityChargeSelect,
       string facing,
       string overlooking,
       int floor,
       string description,string pic1,string pic2,string pic3,string pic4,string pic5)
    {
        Id = id;
        PropertyName = propertyName;
        OnlyFor = onlyFor;
        RentPrice = rentPrice;
        RentPriceSelect = rentPriceSelect;
        Localaddress = localaddress;
        Cityaddress = cityaddress;
        Countryaddress = countryaddress;
        Landmark = landmark;
        Furnishing = furnishing;
        Rooms = rooms;
        Kitchen = kitchen;
        Bathroom = bathroom;
        Balcony = balcony;
        Carparking = carparking;
        BikeParking = bikeParking;
        WaterAvalibility = waterAvalibility;
        Electricity = electricity;
        Flooring = flooring;
        MaintainenceCharge = maintainenceCharge;
        MaintainenceChargeSelect = maintainenceChargeSelect;
        SecurityCharge = securityCharge;
        SecurityChargeSelect = securityChargeSelect;
        Facing = facing;
        Overlooking = overlooking;
        Floor = floor;
        Description = description;
        Pic1 = pic1;
        Pic2 = pic2;
        Pic3 = pic3;
        Pic4 = pic4;
        Pic5 = pic5;
    }
}
