using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program {
  static void Main(string[] args) {
    Dictionary<string, List<string>> mistakeWords = new Dictionary<string, List<string>>();
    mistakeWords.Add("привет", new List<string> { "пирвет", "превет", "привт" });
    mistakeWords.Add("пока", new List<string> { "пака", "поко" });

    Console.WriteLine(" Enter folder path : ");
    string directory = Console.ReadLine();
    string[] files = Directory.GetFiles(directory, "*.txt");

    Console.WriteLine(" Files found: {0} ", files.Length);

    foreach (string filePath in files) {
      Console.WriteLine(" \nProcessing: {0} ", filePath);

      StreamReader reader = new StreamReader(filePath);
      string content = reader.ReadToEnd();
      reader.Close();

      Console.WriteLine(" Before:\n{0} ", content);

      string originalContent = content;

      foreach (KeyValuePair<string, List<string>> pair in mistakeWords) {
        string correctWord = pair.Key;
        List<string> wrongWords = pair.Value;

        foreach (string wrongWord in wrongWords) {
          content = content.Replace(wrongWord, correctWord);
        }
      }

      content = Regex.Replace(content, @"\(0(\d{2})\) (\d{3})-(\d{2})-(\d{2})", "+380 $1 $2 $3 $4");

      if (content != originalContent) {
        StreamWriter writer = new StreamWriter(filePath);
        writer.Write(content);
        writer.Close();

        Console.WriteLine(" After:\n{0} ", content);
      }
      else {
        Console.WriteLine(" No changes needed ");
      }
    }

    Console.WriteLine(" \nDone ");
    Console.ReadKey();
  }
}