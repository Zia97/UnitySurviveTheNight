using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultySelector  {

    static double difficulty = 0;

    public static void setDifficulty(double selecteddifficulty)
    {
        difficulty = selecteddifficulty;
    }

    public static double getDifficulty()
    {
        return difficulty;
    }

}
