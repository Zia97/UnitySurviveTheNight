//Author : Qasim Ziauddin

//Static class for user profile
using UnityEngine;

public static class UserProfile  {

    static int _coins = PlayerPrefs.GetInt("Coins");
    static string _primaryWeapon = "";
    static string _secondaryWeapon = "";
  
    public static void setCoins(int _newCoins)
    {
        _coins = _newCoins;
        PlayerPrefs.SetInt("Coins", _coins);
    }

    public static void addCoins(int _newCoins)
    {
        _coins = _coins +_newCoins;
        PlayerPrefs.SetInt("Coins", _coins);
    }

    public static void setPrimaryWeapon(string primaryWeapon)
    {
        _primaryWeapon = primaryWeapon;
    }

    public static void setSecondaryWeapon(string secondaryWeapon)
    {
        _secondaryWeapon = secondaryWeapon;
    }

    public static string getPrimaryWeapon()
    {
        return _primaryWeapon;
    }

    public static string getSecondaryWeapon()
    {
        return _secondaryWeapon;
    }

    public static int getCoins()
    {
        return _coins;
    }

    public static void decreaseCoins(int _newCoins)
    {
        _coins = _coins -_newCoins;
        PlayerPrefs.SetInt("Coins", _coins);
    }

}
