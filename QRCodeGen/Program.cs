using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using QRCoder;

namespace QRCodeGen;

public class Program
{
    public static void Main()
    {
        const string filePath = "Assets/urls.csv";
        const string baseUrl = "https://gc2.me";
        const string outputDirectory = "Assets";

        var urls = LoadUrls(filePath);
        var enabledUrls = urls.Where(url => url.Enabled);

        foreach (var url in enabledUrls)
        {
            var shortUrl = $"{baseUrl}/{url.Short}";
            GenerateAndSaveQrCode(shortUrl, url.Short, outputDirectory);
        }
    }

    static IEnumerable<UrlEntry> LoadUrls(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        return lines.Skip(1).Select(line => line.Split(',')).Select(columns => new UrlEntry { Enabled = bool.Parse(columns[0]), Short = columns[1], Long = columns[2], Description = columns[3] }).ToList();
    }

    private static void GenerateAndSaveQrCode(string url, string shortName, string outputDirectory)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        using (var qrCode = new QRCode(qrCodeData))
        {
            using (var qrCodeImage = qrCode.GetGraphic(20))
            {
                var filePath = Path.Combine(outputDirectory, $"{shortName}.png");
                qrCodeImage.Save(filePath, ImageFormat.Png);
            }
        }
    }
}

internal class UrlEntry
{
    public required bool Enabled { get; set; }
    public required string Short { get; set; }
    public required string Long { get; set; }
    public required string Description { get; set; }
}
