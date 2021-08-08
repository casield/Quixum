
using System;
using System.Text;
using UnityEngine;
public class QuixConsole
{
    public static void Log(params object[] array)
    {
        StringBuilder builder = new StringBuilder();

        foreach (var item in array)
        {
            builder.Append("<color=#"+RandomColor()+">");
            builder.Append(item.ToString());
            builder.Append("</color>");
            builder.Append(", ");
        }

        Debug.Log(builder.ToString());
    }

    public static string RandomColor()
    {
        System.Random r = new System.Random();
       var  BackColor = new Color(r.Next(0, 256),r.Next(0, 256),r.Next(0, 256),1);

       return ColorUtility.ToHtmlStringRGB( BackColor );
    }
}