﻿using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: Program <image-file-path>");
            return;
        }

        string imagePath = args[0];
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("File not found: " + imagePath);
            return;
        }

        try
        {
            string base64String = ConvertImageToBase64(imagePath);

            SaveToFile(Path.ChangeExtension(imagePath, ".txt"), base64String);
            Console.WriteLine("Base64 string saved to: " + Path.ChangeExtension(imagePath, ".txt"));

            string jsContent = FormatAsJavaScript(base64String);
            string jsFilePath = Path.ChangeExtension(imagePath, ".js");
            SaveToFile(jsFilePath, jsContent);
            Console.WriteLine("Base64 string saved to: " + jsFilePath);

            string htmlContent = GenerateHtml(imagePath, jsFilePath);
            SaveToFile(Path.ChangeExtension(imagePath, ".html"), htmlContent);
            Console.WriteLine("HTML file saved to: " + Path.ChangeExtension(imagePath, ".html"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static string ConvertImageToBase64(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        return Convert.ToBase64String(imageBytes);
    }

    static void SaveToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    static string FormatAsJavaScript(string base64String)
    {
        StringBuilder jsContent = new StringBuilder();
        jsContent.Append("const image = '");
        jsContent.Append("'+\n'");
        for (int i = 0; i < base64String.Length; i += 80)
        {
            if (i + 80 < base64String.Length)
            {
                jsContent.Append(base64String.Substring(i, 80));
                jsContent.Append("'+\n'");
            }
            else
            {
                jsContent.Append(base64String.Substring(i));
            }
        }

        jsContent.Append("';");
        return jsContent.ToString();
    }

    static string GenerateHtml(string imagePath, string jsFilePath)
    {
        string imageExtension = Path.GetExtension(imagePath).TrimStart('.').ToLower();
        string jsFileName = Path.GetFileName(jsFilePath);

        StringBuilder htmlContent = new StringBuilder();
        htmlContent.Append("<!DOCTYPE html>\n<html>\n<head>\n<title>Image and Base64</title>\n");
        htmlContent.Append($"<link id=\"favicon\" rel=\"icon\" type=\"image/{imageExtension}\" href=\"\" />\n");
        htmlContent.Append($"<script src=\"{jsFileName}\"></script>\n");
        htmlContent.Append("</head>\n<body>\n");
        htmlContent.Append("<h1>Original Image</h1>\n");
        htmlContent.Append("<img id=\"originalImage\" alt=\"Image\" />\n");
        htmlContent.Append("<h1>Base64 Encoded String</h1>\n");
        htmlContent.Append("<textarea id=\"base64TextArea\" rows=\"20\" cols=\"80\" readonly></textarea>\n");
        htmlContent.Append("<script>\n");
        htmlContent.Append("document.getElementById('originalImage').src = 'data:image/" + imageExtension + ";base64,' + image;\n");
        htmlContent.Append("document.getElementById('base64TextArea').value = image;\n");
        htmlContent.Append("document.getElementById('favicon').href = 'data:image/" + imageExtension + ";base64,' + image;\n");
        htmlContent.Append("</script>\n");
        htmlContent.Append("</body>\n</html>");

        return htmlContent.ToString();
    }
}