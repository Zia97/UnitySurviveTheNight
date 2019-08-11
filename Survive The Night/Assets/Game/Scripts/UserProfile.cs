//Author : Qasim Ziauddin

//Static class for user profile
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserProfile  {

    static int _coins = PlayerPrefs.GetInt("Coins");

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
