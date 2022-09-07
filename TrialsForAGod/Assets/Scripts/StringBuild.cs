using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public sealed class StringBuild : MonoBehaviour
{
    private string pName = "Billy";
    private void Start()
    {
        // Create a StringBuilder that expects to hold 50 characters.
        // Initialize the StringBuilder with "ABC".
        StringBuilder sb = new StringBuilder();

        // Append three characters (D, E, and F) to the end of the StringBuilder.
        sb.Append("Hello my good friend /name!");

        // Append a format string to the end of the StringBuilder.
        //sb.AppendFormat("GHI{0}{1}", 'J', 'k');

        // Display the number of characters in the StringBuilder and its string.
        Debug.Log(sb.ToString());

        // Insert a string at the beginning of the StringBuilder.
        //sb.Insert(0, "Alphabet: ");

        // Replace all lowercase k's with uppercase K's.
        sb.Replace("/name", pName);

        // Display the number of characters in the StringBuilder and its string.
        Debug.Log(sb.ToString());
    }
}
