﻿//Author : Qasim Ziauddin

//Static class holding the difficulty value the user has selected. Used for transition between scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultySelector  {

    static double difficulty = 1;

    public static void setDifficulty(double selecteddifficulty)
    {
        difficulty = selecteddifficulty;
    }

    public static double getDifficulty()
    {
        return difficulty;
    }

}
