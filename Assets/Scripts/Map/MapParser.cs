using System;
using UnityEngine;

public class MapParser
{
    /// <summary>
    /// Read map data into a 2D char array.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static char[,] ParseMapData(string data)
    {
        // Split text file into lines
        string[] lines = data.Split("\r\n");

        char[,] mapData = new char[lines.Length,    // rows
            lines[0].Length];   // columns (no. characters in each row)
        Debug.Log(mapData.GetLength(0));
        Debug.Log(mapData.GetLength(1));

        // Read characters in each line
        for (int i = 0; i < lines.Length; ++i)
        {
            // Split line into array of characters
            char[] cells = lines[i].ToCharArray();

            // Copy cells into mapData array
            Buffer.BlockCopy(   // 1D array into 2D array
                cells, // src
                0, // srcOffset
                mapData, // dst
                i * mapData.GetLength(1) * sizeof(char), // dstOffset
                cells.Length * sizeof(char)); // count
        }
        
        return mapData;
    }
}
