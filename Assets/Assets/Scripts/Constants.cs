
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

    public static string[,] POSSIBLE_LIGHTS = new string[,]
  {
       //Even part
        {"NI", "M2", "CH","","","",""},
        {"N2", "M2", "CH","","","",""},
        {"N3", "M2", "CH","","","",""},
        {"N4", "M2", "CH","","","",""},
        {"N5", "M2", "CH","","","",""},
        {"M3", "M2", "", "", "","", ""},
        {"M2", "N2", "NI", "N3", "N4","N5","M3"},
        {"CH", "CH2", "CHI", "CH3", "CH4","CH5", ""},
        //Odd part
        {"CHI", "M1", "N","M5","","",""},
        {"CH2", "M1", "N","M5","","",""},
        {"CH3", "M1", "N","M5","","",""},
        {"CH4", "M1", "N","M5","","",""},
        {"CH5", "M1", "N","M5","","",""},
        {"M1", "CH2", "CHI", "CH3", "CH4","CH5", ""},
        {"M5", "CH2", "CHI", "CH3", "CH4","CH5", "M4"},
        {"M4", "M5", "", "", "","",""},
        {"N", "N2", "NI", "N3", "N4","N5", ""},

  };
}
