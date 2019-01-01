using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHolder : Singleton<PathHolder>
{

	//working cells
    // cohdition of stop
    public int EMPTY = 0;
    // skiping cell
    public int SKIP = 1000;
    // tracks 1 - 20
    public int TC_I_CH_ID = 16;
    public int TC_I_CH_2_ID = 18;
    public int TC_I_N_ID = 15;
    public int TC_I_N_I_ID = 17;
    public int TC_I_ID = 1;
    public int TC_2_ID = 2;
    public int TC_3_ID = 3;
    public int TC_4_ID = 4;
    public int TC_5_ID = 5;
    public int TC_6_ID = 6;
    public int TC_7_ID = 7;
    public int TC_8_ID = 8;
    public int TC_9_ID = 9;
    public int TC_10_ID = 19;
    public int TC_12A_ID = 20;
    // 10 END
    public int TC_10_10_ID = 10;
    public int TC_11_ID = 11;
    public int TC_12_ID = 12;
    public int TC_13_ID = 13;
    public int TC_14_ID = 14;
    // switches left to right 21 - 32
           
    public int TC_SW_2_4_TOP_ID = 22;
    public int TC_SW_2_4_BOT_ID = 23;            
    public int TC_SW_10_ID = 24;
    public int TC_SW_12_ID = 25;
    public int TC_SW_14_ID = 26;
    public int TC_SW_22_ID = 27;                       
    public int TC_SW_20_ID = 28;
    public int TC_SW_15_ID = 29;
    public int TC_SW_7_9_BOT_ID = 30;
    public int TC_SW_7_9_TOP_ID = 31;
    public int TC_SW_5_17_BOT_ID = 32;
    public int TC_SW_5_17_TOP_ID = 33;
    public int TC_SW_19_ID = 34;
    public int TC_SW_21_ID = 32;



    // switches right to left 50 - 
    public int TC_SW_6_8_BOT_ID = 50;
    public int TC_SW_6_8_TOP_ID = 51;
    public int TC_SW_16_ID = 52;
    public int TC_SW_18_ID = 53;
    public int TC_SW_13_ID = 54;
    public int TC_SW_11_ID = 55;            
    public int TC_SW_1_3_BOT_ID = 56;
    public int TC_SW_1_3_TOP_ID = 57;               

            
    public int[,] map =
        new int[8, 20]
    {
        { 0,  0,  0,  0,   0,  52, 2,  1000, 1000, 1000, 1000, 29, 0,    0,  0,    0,  0,  0,  0,  0 },
        { 0,  18, 16, 23,  51, 52, 1,  1000, 1000, 1000, 1000, 29, 1000, 31, 1000, 57, 15, 17, 0,  0 },
        { 0,  0,  6,  22,  50, 24, 3,  1000, 1000, 1000, 1000, 55, 1000, 30, 33,   56, 7,  0,  0,  0 },
        { 0,  0,  0,  0,   0,  24, 25, 4,    1000, 1000, 54,   55, 0,    8,  32,   34, 12, 58, 20, 0 },
        { 0,  0,  0,  0,   0,  0,  25, 26,   5,    1000, 54,   0,  0,    0,  0,    34, 13, 58, 0,  0 },
        { 0,  0,  0,  0,   0,  0,  0,  26,   27,   0,    53,   9,  0,    0,  0,    0,  0,  0,  0,  0 },
        { 0,  0,  0,  0,   0,  0,  0,  14,   27,   19,   53,   28, 10,   0,  0,    0,  0,  0,  0,  0 },
        { 0,  0,  0,  0,   0,  0,  0,  0,    0,    0,    0,    28, 11,   0,  0,    0,  0,  0,  0,  0 }
    };

    public void PrintArr()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Debug.Log(map[i, j] + "    ");
            }  
        }
    }

    public void ChangeSwitch(int numID)
    {               

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (map[i, j] == numID)
                {
                    map[i, j] *= -1;
                }
            }
        }  
    }

    public int[] FindStart(int _start)
    {
        int[] point = new int [2] { 0, 0 };

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if(map[i,j] == _start)
                {
                    point[0] = i;
                    point[1] = j;
                    break;
                }
            }
        }
        Debug.Log(  point[0] + "  " + point[1]);
        return point;

    }

    public void PrintPath(int start)
    {
        int[] point = FindStart(start);
                
        bool isGoing = true;
        for (int i = point[0]; i < 8; i++)
        {
            if (!isGoing)
                break;
            for (int j = point[1]; j < 20; j++)
            {
                if (map[i, j] == 1000)
                    continue;                        
                if (map[i, j] < 0)
                {
                    int temp = map[i, j];
                    if (map[i, j] > -40)
                        i++;
                    else if (map[i, j] < -40)
                        i--;
                    if(Math.Abs(map[i, j] - temp) > 2)
                    {
                        isGoing = false;
                        Console.WriteLine();
                        break;
                    }
                }
                        
                if(map[i, j] == 0)
                {
                    isGoing = false;
                    Console.WriteLine();
                    break;
                }
                Console.Write(" -> {0}", map[i, j]);
            }                    

        }
    }

}
