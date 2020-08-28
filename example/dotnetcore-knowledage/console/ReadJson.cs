using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    class ReadJson
    {
        static void Main_Stop()
        {
            //JObject o1 = JObject.Parse(File.ReadAllText(@"c:\videogames.json"));

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@".\readJson.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                //o2.First.ToString
            }
        }
    }
}
