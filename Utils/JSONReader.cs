using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumC_.Utils
{
    public class JSONReader
    {
        public void JsonReader()
        {

        }


        public String ExtractData(String tokenName)
        {
            var data = File.ReadAllText("Utils\\TestData.json");
            var parsedData = JToken.Parse(data);
            return parsedData.SelectToken(tokenName).Value<string>();
        }
    }
}
