using System.Collections.Generic;
using System.Configuration;

namespace LoadDing
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string key in ConfigurationManager.AppSettings)
            {
                dic[key] = ConfigurationManager.AppSettings[key];
            }
            string json = string.Format(@"{{accesstokenUrl:'{0}?corpid={1}&corpsecret={2}',cardDataUrl:'{3}',SavePath:'{4}',postdata:{{userId:'{5}',from:'{6}',to:'{7}'}}}}",
                dic["accesstokenUrl"],
                dic["corpid"],
                dic["corpsecret"],
                dic["cardDataUrl"],
                dic["SavePath"],
                dic["userId"],
                dic["from"],
                dic["to"]
                );
            WebHelper.CrreateCsvString(json);
            if (!string.IsNullOrEmpty(dic["URL"]))
            {
                WebHelper.GetMethod(dic["URL"] + @"&arguments=-A" + dic["TERMINAL_NO"] + ",-A" + dic["SavePath"]);
            }
        }
    }
}
