using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : Singleton<PathHolder>
{
    public  TrackCircuit _tcI_CH;
    public  TrackCircuit _tcI_N;
    public  TrackCircuit _tcI_CH_2;
    public  TrackCircuit _tcI_1_N;
    public  TrackCircuit _tcI_16_15;
    public  TrackCircuit _tc2;
    public  TrackCircuit _tc3;
    public  TrackCircuit _tc4;
    public  TrackCircuit _tc5;
    public  TrackCircuit _tc6;
    public  TrackCircuit _tc7;
    public  TrackCircuit _tc8;
    public  TrackCircuit _tc9;
    public  TrackCircuit _tc10_14_18;
    public  TrackCircuit _tc10_10;
    public  TrackCircuit _tc11;
    public  TrackCircuit _tc12;
    public  TrackCircuit _tc12_12;
    public  TrackCircuit _tc12A;
    public  TrackCircuit _tc13;
    public  TrackCircuit _tc14;
    public  TrackCircuit _tcsw_1_3top;
    public  TrackCircuit _tcsw_1_3bot;
    public  TrackCircuit _tcsw_5_17top;
    public  TrackCircuit _tcsw_5_17bot;
    public  TrackCircuit _tcsw_7_9top;
    public  TrackCircuit _tcsw_7_9bot;
    public  TrackCircuit _tcsw_2_4top;
    public  TrackCircuit _tcsw_2_4bot;
    public  TrackCircuit _tcsw_6_8top;
    public  TrackCircuit _tcsw_6_8bot;
    public  TrackCircuit _tcsw14;
    public  TrackCircuit _tcsw22;
    public  TrackCircuit _tcsw16;
    public  TrackCircuit _tcsw10;
    public  TrackCircuit _tcsw11;
    public  TrackCircuit _tcsw12;
    public  TrackCircuit _tcsw13;
    public  TrackCircuit _tcsw15;
    public  TrackCircuit _tcsw18;
    public  TrackCircuit _tcsw19;
    public  TrackCircuit _tcsw20;
    public  TrackCircuit _tcsw21;

    public static TrackCircuit tcI_CH;
    public static TrackCircuit tcI_N;
    public static TrackCircuit tcI_CH_2;
    public static TrackCircuit tcI_1_N;
    public static TrackCircuit tcI_16_15;
    public static TrackCircuit tc2;
    public static TrackCircuit tc3;
    public static TrackCircuit tc4;
    public static TrackCircuit tc5;
    public static TrackCircuit tc6;
    public static TrackCircuit tc7;
    public static TrackCircuit tc8;
    public static TrackCircuit tc9;
    public static TrackCircuit tc10_14_18;
    public static TrackCircuit tc10_10;
    public static TrackCircuit tc11;
    public static TrackCircuit tc12;
    public static TrackCircuit tc12_12;
    public static TrackCircuit tc12A;
    public static TrackCircuit tc13;
    public static TrackCircuit tc14;
    public static TrackCircuit tcsw_1_3top;
    public static TrackCircuit tcsw_1_3bot;
    public static TrackCircuit tcsw_5_17top;
    public static TrackCircuit tcsw_5_17bot;
    public static TrackCircuit tcsw_7_9top;
    public static TrackCircuit tcsw_7_9bot;
    public static TrackCircuit tcsw_2_4top;
    public static TrackCircuit tcsw_2_4bot;
    public static TrackCircuit tcsw_6_8top;
    public static TrackCircuit tcsw_6_8bot;
    public static TrackCircuit tcsw14;
    public static TrackCircuit tcsw22;
    public static TrackCircuit tcsw16;
    public static TrackCircuit tcsw10;
    public static TrackCircuit tcsw11;
    public static TrackCircuit tcsw12;
    public static TrackCircuit tcsw13;
    public static TrackCircuit tcsw15;
    public static TrackCircuit tcsw18;
    public static TrackCircuit tcsw19;
    public static TrackCircuit tcsw20;
    public static TrackCircuit tcsw21;

    Node nodeICH;
    Node nodeICH2;
    Node nodeIN;
    Node nodeI1N;
    Node nodeI1615;
    Node node2;
    Node node3;
    Node node4;
    Node node5;
    Node node6;
    Node node7;
    Node node8;
    Node node9;
    Node node10_14_18;
    Node node10_10;
    Node node11;
    Node node12;
    Node node12_12;
    Node node12A;
    Node node13;
    Node node14;
    Node nodeSw_1_3top;
    Node nodeSw_1_3bot;
    Node nodeSw_5_17top;
    Node nodeSw_5_17bot;
    Node nodeSw_7_9top;
    Node nodeSw_7_9bot;
    Node nodeSw_2_4top;
    Node nodeSw_2_4bot;
    Node nodeSw_6_8top;
    Node nodeSw_6_8bot;
    Node nodeSw10;
    Node nodeSw12;
    Node nodeSw14;
    Node nodeSw22;
    Node nodeSw16;
    Node nodeSw18;
    Node nodeSw20;
    Node nodeSw15;
    Node nodeSw11;
    Node nodeSw19;
    Node nodeSw13;
    Node nodeSw21;

    public List<Node> nodesList;

    //working IDs

    int EMPTY = 0;    
    int SKIP = 1000;    
    public static int  TC_I_CH_ID = 16;
    public static int  TC_I_CH_2_ID = 18;
    public static int  TC_I_N_ID = 15;
    public static int  TC_I_1_N_ID = 17;
    public static int  TC_I_ID = 1;
    public static int  TC_2_ID = 2;
    public static int  TC_3_ID = 3;
    public static int  TC_4_ID = 4;
    public static int  TC_5_ID = 5;
    public static int  TC_6_ID = 6;
    public static int  TC_7_ID = 7;
    public static int  TC_8_ID = 8;
    public static int  TC_9_ID = 9;
    public static int  TC_10_14_18_ID = 19;
    public static int  TC_12A_ID = 20;
    public static int  TC_12_12_ID = 21;    
    public static int  TC_10_10_ID = 10;
    public static int  TC_11_ID = 11;
    public static int  TC_12_ID = 12;
    public static int  TC_13_ID = 13;
    public static int  TC_14_ID = 14;               
    public static int  TC_SW_2_4_TOP_ID = 23;
    public static int  TC_SW_2_4_BOT_ID = 22;            
    public static int  TC_SW_10_ID = 24;
    public static int  TC_SW_12_ID = 25;
    public static int  TC_SW_14_ID = 26;
    public static int  TC_SW_22_ID = 27;                       
    public static int  TC_SW_20_ID = 28;
    public static int  TC_SW_15_ID = 29;
    public static int  TC_SW_7_9_BOT_ID = 30;
    public static int  TC_SW_7_9_TOP_ID = 31;
    public static int  TC_SW_5_17_BOT_ID = 32;
    public static int  TC_SW_5_17_TOP_ID = 33;
    public static int  TC_SW_19_ID = 34;     
    public static int  TC_SW_6_8_BOT_ID = 50;
    public static int  TC_SW_6_8_TOP_ID = 51;
    public static int  TC_SW_16_ID = 52;
    public static int  TC_SW_18_ID = 53;
    public static int  TC_SW_13_ID = 54;
    public static int  TC_SW_11_ID = 55;            
    public static int  TC_SW_1_3_BOT_ID = 56;
    public static int  TC_SW_1_3_TOP_ID = 57;
    public static int  TC_SW_21_ID = 58;

    public Dictionary<TrackCircuit, int> trackCircuitTC_ID;
    public Dictionary<int, TrackCircuit> trackCircuitID_TC;
    public Dictionary<int, Node> nodesID_ND;

    private void Awake()
    {
        tcI_CH  =      _tcI_CH;
        tcI_N =        _tcI_N;
        tcI_CH_2 =     _tcI_CH_2;
        tcI_1_N =      _tcI_1_N;
        tcI_16_15 =    _tcI_16_15;
        tc2 =          _tc2;
        tc3 =          _tc3;
        tc4 =          _tc4;
        tc5 =          _tc5;
        tc6 =          _tc6;
        tc7 =          _tc7;
        tc8 =          _tc8;
        tc9 =          _tc9;
        tc10_14_18 =   _tc10_14_18;
        tc10_10 =      _tc10_10;
        tc11    =      _tc11;
        tc12    =      _tc12;
        tc12_12 =      _tc12_12;
        tc12A   =      _tc12A;
        tc13 =         _tc13;
        tc14 =         _tc14;
        tcsw_1_3top =  _tcsw_1_3top;
        tcsw_1_3bot =  _tcsw_1_3bot;
        tcsw_5_17top = _tcsw_5_17top;
        tcsw_5_17bot = _tcsw_5_17bot;
        tcsw_7_9top =  _tcsw_7_9top;
        tcsw_7_9bot =  _tcsw_7_9bot;
        tcsw_2_4top =  _tcsw_2_4top;
        tcsw_2_4bot =  _tcsw_2_4bot;
        tcsw_6_8top =  _tcsw_6_8top;
        tcsw_6_8bot =  _tcsw_6_8bot;
        tcsw14 =       _tcsw14;
        tcsw22 =       _tcsw22;
        tcsw16 =       _tcsw16;
        tcsw10 =       _tcsw10;
        tcsw11 =       _tcsw11;
        tcsw12 =       _tcsw12;
        tcsw13 =       _tcsw13;
        tcsw15 =       _tcsw15;
        tcsw18 =       _tcsw18;
        tcsw19 =       _tcsw19;
        tcsw20 =       _tcsw20;
        tcsw21 =       _tcsw21;


        trackCircuitTC_ID = new Dictionary<TrackCircuit, int>
        {
            { tcI_CH, TC_I_CH_ID },
            { tcI_CH_2, TC_I_CH_2_ID },
            { tcI_N, TC_I_N_ID },
            { tcI_1_N, TC_I_1_N_ID },
            { tcI_16_15, TC_I_ID },
            { tc2, TC_2_ID },
            { tc3, TC_3_ID },
            { tc4, TC_4_ID },
            { tc5, TC_5_ID },
            { tc6, TC_6_ID },
            { tc7, TC_7_ID },
            { tc8, TC_8_ID },
            { tc9, TC_9_ID },
            { tc10_10, TC_10_10_ID },
            { tc10_14_18, TC_10_14_18_ID },
            { tc11, TC_11_ID },
            { tc12_12, TC_12_ID },
            { tc12, TC_12_12_ID },
            { tc12A, TC_12A_ID },
            { tc13, TC_13_ID },
            { tc14, TC_14_ID },
            { tcsw_1_3bot, TC_SW_1_3_BOT_ID },
            { tcsw_1_3top, TC_SW_1_3_TOP_ID },
            { tcsw_2_4top, TC_SW_2_4_TOP_ID },
            { tcsw_2_4bot, TC_SW_2_4_BOT_ID },
            { tcsw_6_8bot, TC_SW_6_8_BOT_ID },
            { tcsw_6_8top, TC_SW_6_8_TOP_ID },
            { tcsw_7_9top, TC_SW_7_9_TOP_ID },
            { tcsw_7_9bot, TC_SW_7_9_BOT_ID },
            { tcsw_5_17top, TC_SW_5_17_TOP_ID },
            { tcsw_5_17bot, TC_SW_5_17_BOT_ID },
            { tcsw10, TC_SW_10_ID },
            { tcsw12, TC_SW_12_ID },
            { tcsw14, TC_SW_14_ID },
            { tcsw16, TC_SW_16_ID },
            { tcsw18, TC_SW_18_ID },
            { tcsw20, TC_SW_20_ID },
            { tcsw22, TC_SW_22_ID },
            { tcsw11, TC_SW_11_ID },
            { tcsw13, TC_SW_13_ID },
            { tcsw15, TC_SW_15_ID },
            { tcsw19, TC_SW_19_ID },
            { tcsw21, TC_SW_21_ID }
        };

        trackCircuitID_TC = new Dictionary<int, TrackCircuit>
        {
            { TC_I_CH_ID, tcI_CH },
            { TC_I_CH_2_ID, tcI_CH_2 },
            { TC_I_N_ID, tcI_N },
            { TC_I_1_N_ID, tcI_1_N },
            { TC_I_ID, tcI_16_15 },
            { TC_2_ID, tc2 },
            { TC_3_ID, tc3 },
            { TC_4_ID, tc4 },
            { TC_5_ID, tc5 },
            { TC_6_ID, tc6 },
            { TC_7_ID, tc7 },
            { TC_8_ID, tc8 },
            { TC_9_ID, tc9 },
            { TC_10_10_ID, tc10_10 },
            { TC_10_14_18_ID, tc10_14_18 },
            { TC_11_ID, tc11 },
            { TC_12_12_ID, tc12 },
            { TC_12_ID, tc12_12 },
            { TC_12A_ID, tc12A },
            { TC_13_ID, tc13 },
            { TC_14_ID, tc14 },
            { TC_SW_1_3_BOT_ID, tcsw_1_3bot },
            { TC_SW_1_3_TOP_ID, tcsw_1_3top },
            { TC_SW_2_4_TOP_ID, tcsw_2_4top },
            { TC_SW_2_4_BOT_ID, tcsw_2_4bot },
            { TC_SW_6_8_BOT_ID, tcsw_6_8bot },
            { TC_SW_6_8_TOP_ID, tcsw_6_8top },
            { TC_SW_7_9_TOP_ID, tcsw_7_9top },
            { TC_SW_7_9_BOT_ID, tcsw_7_9bot },
            { TC_SW_5_17_TOP_ID, tcsw_5_17top },
            { TC_SW_5_17_BOT_ID, tcsw_5_17bot },
            { TC_SW_10_ID, tcsw10 },
            { TC_SW_12_ID, tcsw12 },
            { TC_SW_14_ID, tcsw14 },
            { TC_SW_16_ID, tcsw16 },
            { TC_SW_18_ID, tcsw18 },
            { TC_SW_20_ID, tcsw20 },
            { TC_SW_22_ID, tcsw22 },
            { TC_SW_11_ID, tcsw11 },
            { TC_SW_13_ID, tcsw13 },
            { TC_SW_15_ID, tcsw15 },
            { TC_SW_19_ID, tcsw19 },
            { TC_SW_21_ID, tcsw21 }
        };


        //(Node prevMin, Node prevPlus, string track, Node nextPlus, Node nextMin, bool isSwitch)


        nodeICH = new Node(null, null, tcI_CH, tcI_CH_2, null, false, TC_I_CH_ID);
        nodeICH2 = new Node(null, tcI_CH, tcI_CH_2, tcsw_2_4top, null, false, TC_I_CH_2_ID);
        nodeIN = new Node(null, tcI_1_N, tcI_N, null, null, false, TC_I_N_ID);
        nodeI1N = new Node(null, tcsw_1_3top, tcI_1_N, tcI_N, null, false, TC_I_1_N_ID);
        nodeI1615 = new Node(null, tcsw16, tcI_16_15, tcsw15, null, false, TC_I_ID);
        node2 = new Node(null, tcsw16, tc2, tcsw15, null, false, TC_2_ID);
        node3 = new Node(null, tcsw10, tc3, tcsw11, null, false, TC_3_ID);
        node4 = new Node(null, tcsw12, tc4, tcsw13, null, false, TC_4_ID);
        node5 = new Node(null, tcsw14, tc5, tcsw13, null, false, TC_5_ID);
        node6 = new Node(null, null, tc6, tcsw_2_4bot, null, false, TC_6_ID);
        node7 = new Node(null, tcsw_1_3bot, tc7, null, null, false, TC_7_ID);
        node8 = new Node(null, null, tc8, tcsw_5_17bot, null, false, TC_8_ID);
        node9 = new Node(null, tcsw18, tc9, null, null, false, TC_9_ID);
        node10_14_18 = new Node(null, tcsw22, tc10_14_18, tcsw18, null, false, TC_10_14_18_ID);
        node10_10 = new Node(null, tcsw20, tc10_10, null, null, false, TC_10_10_ID);
        node11 = new Node(null, tcsw20, tc11, null, null, false, TC_11_ID);
        node12 = new Node(null, tcsw_5_17bot, tc12, tcsw19, null, false, TC_12_ID);
        node12_12 = new Node(null, tcsw19, tc12_12, tcsw21, null, false, TC_12_12_ID);
        node12A = new Node(null, tcsw21, tc12A, null, null, false, TC_12A_ID);
        node13 = new Node(null, tcsw19, tc13, tcsw21, null, false, TC_13_ID);
        node14 = new Node(null, null, tc14, tcsw22, null, false, TC_14_ID);


        nodeSw_1_3top = new Node(tcsw_1_3bot, tcsw_7_9top, tcsw_1_3top, tcI_1_N, tcI_1_N, true, TC_SW_1_3_TOP_ID);
        nodeSw_1_3bot = new Node(tcsw_5_17top, tcsw_5_17top, tcsw_1_3bot, tc7, tcsw_1_3top, true, TC_SW_1_3_BOT_ID);
        nodeSw_5_17top = new Node(tcsw_7_9bot, tcsw_7_9bot, tcsw_5_17top, tcsw_1_3bot, tcsw_5_17bot, true, TC_SW_5_17_TOP_ID);
        nodeSw_5_17bot = new Node(tcsw_5_17top, tc8, tcsw_5_17bot, tcsw19, tcsw19, true, TC_SW_5_17_BOT_ID);
        nodeSw_7_9top = new Node(tcsw15, tcsw15, tcsw_7_9top, tcsw_1_3top, tcsw_7_9bot, true, TC_SW_7_9_TOP_ID);
        nodeSw_7_9bot = new Node(tcsw_7_9top, tcsw11, tcsw_7_9bot, tcsw_5_17top, tcsw_5_17top, true, TC_SW_7_9_BOT_ID);
        nodeSw_2_4top = new Node(tcI_CH_2, tcI_CH_2, tcsw_2_4top, tcsw_6_8top, tcsw_2_4bot, true, TC_SW_2_4_TOP_ID);
        nodeSw_2_4bot = new Node(tcsw_2_4top, tc6, tcsw_2_4bot, tcsw_6_8bot, tcsw_6_8bot, true, TC_SW_2_4_BOT_ID);
        nodeSw_6_8top = new Node(tcsw_6_8bot, tcsw_2_4top, tcsw_6_8top, tcsw16, tcsw16, true, TC_SW_6_8_TOP_ID);
        nodeSw_6_8bot = new Node(tcsw_2_4bot, tcsw_2_4bot, tcsw_6_8bot, tcsw10, tcsw_6_8top, true, TC_SW_6_8_BOT_ID);
        nodeSw10 = new Node(tcsw_6_8bot, tcsw_6_8bot, tcsw10, tc3, tcsw12, true, TC_SW_10_ID);
        nodeSw12 = new Node(tcsw10, tcsw10, tcsw12, tcsw14, tc4, true, TC_SW_12_ID);
        nodeSw14 = new Node(tcsw12, tcsw12, tcsw14, tcsw22, tc5, true, TC_SW_14_ID);
        nodeSw22 = new Node(tc14, tcsw14, tcsw22, tc10_14_18, tc10_14_18, true, TC_SW_22_ID);
        nodeSw16 = new Node(tcsw_6_8top, tcsw_6_8top, tcsw16, tcI_16_15, tc2, true, TC_SW_16_ID);
        nodeSw18 = new Node(tc10_14_18, tc10_14_18, tcsw18, tcsw20, tc9, true, TC_SW_18_ID);
        nodeSw20 = new Node(tcsw18, tcsw18, tcsw20, tc10_10, tc11, true, TC_SW_20_ID);
        nodeSw15 = new Node(tc2, tcI_16_15, tcsw15, tcsw_7_9top, tcsw_7_9top, true, TC_SW_15_ID);
        nodeSw11 = new Node(tcsw13, tc3, tcsw11, tcsw_7_9bot, tcsw_7_9bot, true, TC_SW_11_ID);
        nodeSw19 = new Node(tcsw_5_17bot, tcsw_5_17bot, tcsw19, tc12, tc13, true, TC_SW_19_ID);
        nodeSw13 = new Node(tc4, tc5, tcsw13, tcsw11, tcsw11, true, TC_SW_13_ID);
        nodeSw21 = new Node(tc13, tc12_12, tcsw21, tc12A, tc12A, true, TC_SW_21_ID);

        nodesList = new List<Node>(new Node[] 
        {
            nodeICH,
            nodeICH2,
            nodeIN,
            nodeI1N,
            nodeI1615,
            node2,
            node3,
            node4,
            node5,
            node6,
            node7,
            node8,
            node9,
            node10_14_18,
            node10_10,
            node11,
            node12,
            node12_12,
            node12A,
            node13,
            node14,
            nodeSw_1_3top,
            nodeSw_1_3bot,
            nodeSw_5_17top,
            nodeSw_5_17bot,
            nodeSw_7_9top,
            nodeSw_7_9bot,
            nodeSw_2_4top,
            nodeSw_2_4bot,
            nodeSw_6_8top,
            nodeSw_6_8bot,
            nodeSw10,
            nodeSw12,
            nodeSw14,
            nodeSw22,
            nodeSw16,
            nodeSw18,
            nodeSw20,
            nodeSw15,
            nodeSw11,
            nodeSw19,
            nodeSw13,
            nodeSw21
        });


        nodesID_ND = new Dictionary<int, Node>
        {
            { TC_I_CH_ID,         nodeICH   },
            { TC_I_CH_2_ID,       nodeICH2  },
            { TC_I_N_ID,          nodeIN    },
            { TC_I_1_N_ID,        nodeI1N   },
            { TC_I_ID,            nodeI1615 },
            { TC_2_ID,            node2     },
            { TC_3_ID,            node3     },
            { TC_4_ID,            node4     },
            { TC_5_ID,            node5     },
            { TC_6_ID,            node6     },
            { TC_7_ID,            node7     },
            { TC_8_ID,            node8     },
            { TC_9_ID,            node9     },
            { TC_10_10_ID,        node10_10    },
            { TC_10_14_18_ID,     node10_14_18 },
            { TC_11_ID,           node11    },
            { TC_12_12_ID,        node12    },
            { TC_12_ID,           node12_12 },
            { TC_12A_ID,          node12A   },
            { TC_13_ID,           node13    },
            { TC_14_ID,           node14    },
            { TC_SW_1_3_BOT_ID,   nodeSw_1_3bot  },
            { TC_SW_1_3_TOP_ID,   nodeSw_1_3top  },
            { TC_SW_2_4_TOP_ID,   nodeSw_2_4top  },
            { TC_SW_2_4_BOT_ID,   nodeSw_2_4bot },
            { TC_SW_7_9_TOP_ID,   nodeSw_7_9top  },
            { TC_SW_7_9_BOT_ID,   nodeSw_7_9bot  },
            { TC_SW_5_17_TOP_ID,  nodeSw_5_17top  },
            { TC_SW_5_17_BOT_ID,  nodeSw_5_17bot },
            { TC_SW_6_8_TOP_ID,   nodeSw_6_8top  },
            { TC_SW_6_8_BOT_ID,   nodeSw_6_8bot  },
            { TC_SW_10_ID,        nodeSw10       },
            { TC_SW_12_ID,        nodeSw12       },
            { TC_SW_14_ID,        nodeSw14       },
            { TC_SW_22_ID,        nodeSw22       },
            { TC_SW_16_ID,        nodeSw16       },
            { TC_SW_18_ID,        nodeSw18       },
            { TC_SW_20_ID,        nodeSw20       },
            { TC_SW_15_ID,        nodeSw15       },
            { TC_SW_11_ID,        nodeSw11       },
            { TC_SW_19_ID,        nodeSw19       },
            { TC_SW_13_ID,        nodeSw13       },
            { TC_SW_21_ID,        nodeSw21       }
        };

    }

    


    private void Start()
    {
       //PrintArr();
    }

    public void PrintArr()
    {
        print("ID " + nodesID_ND[5].track.name);
        string path = " Path: ";
        foreach (Node nd in nodesList)
        {
            path += " -> " + nd.track.name;
        }
        print(path);
    }

    

}

public class Node
{
   
    public TrackCircuit prevMin;
    public TrackCircuit prevPlus;
    public TrackCircuit track;
    public TrackCircuit nextPlus;
    public TrackCircuit nextMin;
    public bool isSwitch;
    public int nodeID;
    public Node nextNode;

    public Node()
    {
        
    }

    public Node(TrackCircuit prevMin, TrackCircuit prevPlus, TrackCircuit track, TrackCircuit nextPlus, TrackCircuit nextMin, bool isSwitch, int nodeID)
    {
        this.prevMin = prevMin;
        this.prevPlus = prevPlus;
        this.track = track;
        this.nextPlus = nextPlus;
        this.nextMin = nextMin;
        this.isSwitch = isSwitch;
        this.nodeID = nodeID;        
    }    

   
}
