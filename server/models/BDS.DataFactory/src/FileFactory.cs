using System;
using System.Collections.Generic;
using System.IO;
namespace BDS.DataFactory
{
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Threading.Tasks;
    public class FileFactory
    {
        public static async Task SerializationJsonAsync(string filePath, object value)
        {
            string json = string.Empty;
            try
            {
                using (var stream = new MemoryStream())
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.WriteIndented = true;
                    options.Encoder = JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);

                    if (!File.Exists(filePath))
                    {
                        using var file = File.Create(filePath);
                    }
                    await JsonSerializer.SerializeAsync(stream, value, value.GetType(), options);
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        await File.WriteAllTextAsync(filePath, await reader.ReadToEndAsync());
                    }

                }
            }
            catch(Exception ex)
            {

            }
        }
        public static void SerializationJson(string filePath, object value)
        {
            FileFactory.SerializationJsonAsync(filePath, value).Wait();
        }
    }
}
