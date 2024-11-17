using System.Text;

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
            SaveToFile(Path.ChangeExtension(imagePath, ".js"), jsContent);
            Console.WriteLine("Base64 string saved to: " + Path.ChangeExtension(imagePath, ".js"));
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
}
