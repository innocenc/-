using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class WebHelper
    {
        public static void GetCsvString(string json)
        {
            var Ding = DingParams.GetModel(json);
            Ding.Accesstoken = AccessToken.GetModel(GetMethod(Ding.AccesstokenUrl)).Access_token;
            var postdt = Ding.Postdata;
            DateTime now = DateTime.Now;
            DateTime fdt = now.AddDays(postdt.from);
            DateTime tdt = now.AddDays(postdt.to);
            string postStrings = string.Format(@"{{workDateFrom:'{0} 00:00:00',workDateTo:'{1} 00:00:00',{2}}}", fdt.ToString("yyyy-MM-dd"), tdt.ToString("yyyy-MM-dd"), string.IsNullOrEmpty(postdt.userId) ? "" : string.Format(@"userId:'{0}'", postdt.userId));
            var data = CardDatas.GetModel(PostMethod(Ding.CardDataUrl + "?access_token=" + Ding.Accesstoken, postStrings));
            File.WriteAllText(Ding.SavePath, CSV(data.Recordresult), Encoding.ASCII);
        }
        private static string CSV(List<Recordresult> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("time,card,date");
            foreach (var dat in data)
            {
                var tm = Convert13(dat.UserCheckTime);
                sb.AppendLine(tm.ToString("HH:mm:ss") + "," + dat.UserId + "," + tm.ToString("dd/MM/yyyy"));
            }
            return sb.ToString();
        }
        private static DateTime Convert13(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        public static string GetMethod(string url)
        {
            try
            {
                WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
                System.Net.WebRequest wrq = System.Net.WebRequest.Create(url);
                wrq.Method = "GET";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                System.Net.WebResponse wrp = wrq.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(wrp.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8"));
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string PostMethod(string url, string postStrings)
        {
            try
            {
                byte[] postData = Encoding.UTF8.GetBytes(postStrings); 
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "application/json");
                byte[] responseData = webClient.UploadData(url, "POST", postData);  
                string srcString = Encoding.UTF8.GetString(responseData);
                return srcString;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }

    }
    class DingParams
    {
        private string savePath;

        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }
        private string accesstokenUrl;

        public string AccesstokenUrl
        {
            get { return accesstokenUrl; }
            set { accesstokenUrl = value; }
        }
        private string cardDataUrl;

        public string CardDataUrl
        {
            get { return cardDataUrl; }
            set { cardDataUrl = value; }
        }
        private string accesstoken;

        public string Accesstoken
        {
            get { return accesstoken; }
            set { accesstoken = value; }
        }
        private PostDatas postdata;

        public PostDatas Postdata
        {
            get { return postdata; }
            set { postdata = value; }
        }

        public static DingParams GetModel(string json)
        {
            return JsonConvert.DeserializeObject<DingParams>(json);
        }
    }
    class PostDatas
    {
        private string _userId;

        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private int _from;

        public int from
        {
            get { return _from; }
            set { _from = value; }
        }
        private int _to;

        public int to
        {
            get { return _to; }
            set { _to = value; }
        }

    }
    class AccessToken
    {

        private string errcode;

        public string Errcode
        {
            get { return errcode; }
            set { errcode = value; }
        }
        private string errmsg;

        public string Errmsg
        {
            get { return errmsg; }
            set { errmsg = value; }
        }
        private string access_token;

        public string Access_token
        {
            get { return access_token; }
            set { access_token = value; }
        }
        public static AccessToken GetModel(string json)
        {
            return JsonConvert.DeserializeObject<AccessToken>(json);
        }
    }
    class CardDatas
    {

        private string _errcode;

        public string Errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }


        private string _errmsg;

        public string Errmsg
        {
            get { return _errmsg; }
            set { _errmsg = value; }
        }


        private List<Recordresult> _recordresult;

        public List<Recordresult> Recordresult
        {
            get { return _recordresult; }
            set { _recordresult = value; }
        }


        public static CardDatas GetModel(string json)
        {
            return JsonConvert.DeserializeObject<CardDatas>(json);
        }


    }
    class Recordresult
    {
        private string _baseCheckTime;

        public string BaseCheckTime
        {
            get { return _baseCheckTime; }
            set { _baseCheckTime = value; }
        }


        private string _checkType;

        public string CheckType
        {
            get { return _checkType; }
            set { _checkType = value; }
        }


        private string _corpId;

        public string CorpId
        {
            get { return _corpId; }
            set { _corpId = value; }
        }


        private string _groupId;

        public string GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }


        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private string _locationResult;

        public string LocationResult
        {
            get { return _locationResult; }
            set { _locationResult = value; }
        }


        private string _planId;

        public string PlanId
        {
            get { return _planId; }
            set { _planId = value; }
        }


        private string _recordId;

        public string RecordId
        {
            get { return _recordId; }
            set { _recordId = value; }
        }


        private string _timeResult;

        public string TimeResult
        {
            get { return _timeResult; }
            set { _timeResult = value; }
        }


        private string _userCheckTime;

        public string UserCheckTime
        {
            get { return _userCheckTime; }
            set { _userCheckTime = value; }
        }


        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }


        private string _workDate;

        public string WorkDate
        {
            get { return _workDate; }
            set { _workDate = value; }
        }


    }






}
