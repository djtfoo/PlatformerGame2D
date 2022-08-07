using System;
using System.Text;
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

    public static string SerializeMapData(char[,] data)
    {
        StringBuilder sb = new StringBuilder(data.GetLength(0) * data.GetLength(1));
        for (int i = 0; i < data.GetLength(0); i++)  // rows, vertical axis
        {
            for (int j = 0; j < data.GetLength(1); j++)  // columns, horizontal axis
                sb.Append(data[i, j]);

            if (i < data.GetLength(0) - 1)  // not the last row, add a newline
                sb.Append("\r\n");
        }

        return sb.ToString();
    }
}
