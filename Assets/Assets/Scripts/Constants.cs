
using System.Collections.Generic;

public class Constants {

    public static string HIDE_INDICATION_LAYER = "HideIndication";
    public static string INDICATION_LAYER = "Indication";

    public static string DIR_TURN = "turn";
    public static string DIR_STR = "straight";
    public static string LIGHTS_FREE = "TrafficLight";
    public static string LIGHTS_IN_ROUTE = "TrafficLightInRoute";

    public static int COLOR_DEFAULT = 0;
    public static int COLOR_GREEN = 1;
    public static int COLOR_BLUE = 2;
    public static int COLOR_WHITE = 3;
    public static int COLOR_YELLOW = 4;
    public static int COLOR_YELLOW_FLASH = 5;
    public static int COLOR_YELLOW_TOP_FLASH = 6;

    public static int TC_WAIT = -1;
    public static int TC_DEFAULT = 0;
    public static int TC_OVER = 1;
    public static int TC_USED = 2;

    public static string [][] POSSIBLE_LIGHTS = new []
    {
       //Even part
        new string[] {"NI", "M2", "CH"},
        new string[]{"N2", "M2", "CH"},
        new string[]{"N3", "M2", "CH"},
        new string[]{"N4", "M2", "CH"},
        new string[]{"N5", "M2", "CH"},
        new string[]{"M3", "M2"},
        new string[]{"M2", "N2", "NI", "N3", "N4","N5","M3"},
        new string[]{"CH", "CH2", "CHI", "CH3", "CH4","CH5"},
        //Odd part
        new string[]{"CHI", "M1", "N","M5"},
        new string[]{"CH2", "M1", "N","M5"},
        new string[]{"CH3", "M1", "N","M5"},
        new string[]{"CH4", "M1", "N","M5"},
        new string[]{"CH5", "M1", "N","M5"},
        new string[]{"M1", "CH2", "CHI", "CH3", "CH4","CH5"},
        new string[]{"M5", "CH2", "CHI", "CH3", "CH4","CH5", "M4"},
        new string[]{"M4", "M5"},
        new string[]{"N", "N2", "NI", "N3", "N4","N5"},

    };
    public static List <string[]> CONDUCTOR_ROUTE_ASK = new List<string[]>()
    {
    
        new string[] {"6I", "M2", "NI"},
        new string[] {"62", "M2", "N2"},
        new string[] {"63", "M2", "N3"},
        new string[] {"64", "M2", "N4"},
        new string[] {"65", "M2", "N5"},
        new string[] {"6S", "M2", "M3"},

        new string[] {"8T", "M4", "M5"},

        new string[] {"TI", "M5", "CHI"},
        new string[] {"T2", "M5", "CH2"},
        new string[] {"T3", "M5", "CH3"},
        new string[] {"T4", "M5", "CH4"},
        new string[] {"T5", "M5", "CH5"},
        new string[] {"T8", "M5", "M4"},

        new string[] {"7I", "M1", "CHI"},
        new string[] {"72", "M1", "CH2"},
        new string[] {"73", "M1", "CH3"},
        new string[] {"74", "M1", "CH4"},
        new string[] {"75", "M1", "CH5"},

        new string[] {"I6", "NI", "M2"},
        new string[] {"26", "N2", "M2"},
        new string[] {"36", "N3", "M2"},
        new string[] {"46", "N4", "M2"},
        new string[] {"56", "N5", "M2"},
        new string[] {"S6", "M3", "M2"},

        new string[] {"I7", "CHI", "M1"},
        new string[] {"27", "CH2", "M1"},
        new string[] {"37", "CH3", "M1"},
        new string[] {"47", "CH4", "M1"},
        new string[] {"57", "CH5", "M1"},

        new string[] {"IT", "CHI", "M5"},
        new string[] {"2T", "CH2", "M5"},
        new string[] {"3T", "CH3", "M5"},
        new string[] {"4T", "CH4", "M5"},
        new string[] {"5T", "CH5", "M5"},
    };



}
