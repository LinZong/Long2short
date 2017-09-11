using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long2short
{
    class Base62Class
    {
        Dictionary<int, char> Base62Dic = new Dictionary<int, char>();
        public Dictionary<int, char> CreateDic()
        {
            int a = 0;
            for (int i = 97; i <= 122; i++)
            {
                Base62Dic.Add(a, Convert.ToChar(i));
                a++;
            } // 0-25 is a-z
            for (int i = 65; i <= 90; i++)
            {
                Base62Dic.Add(a, Convert.ToChar(i));
                a++;
            }//26-51 is A-Z
            for (int i = 48; i <= 57; i++)
            {
                Base62Dic.Add(a, Convert.ToChar(i));
                a++;
            } //52-61 is 0-9

            return Base62Dic;
        }

        public List<int> GetBase62(int input)
        {
            List<int> Base62Value = new List<int>();
            while (input > 0)
            {
                int remain = input % 62;
                Base62Value.Add(remain);
                input = input / 62;
            }
            return Base62Value;
        }

        public string ReturnShortUrl(int UrlId)
        {
            string s = "";
            var base62result = GetBase62(UrlId);
            Dictionary<int, char> Dic = CreateDic();
            for (int i = 0; i < (6 - base62result.Count()); i++)
            {
                s = s + Dic[0];
            }
            for (int i = base62result.Count(); i > 0; i--)
            {
                s = s + Dic[base62result[i - 1]];
            }
            Dic.Clear();
            return "http://zap.tech/" + s;
        }

        public int GetUrlId(string OriShortUrl)
        {
            char[] KeyCharList = OriShortUrl.ToCharArray();
            double id = 0;
            double counter = KeyCharList.Length - 1;
            Dictionary<int, char> Dic = CreateDic();
            foreach (var i in KeyCharList)
            {
                var c = Dic.First(r => r.Value == i).Key;
                id += (c) * Math.Pow(62, counter);
                counter--;
            }
            Dic.Clear();
            int intid = (Int32)id;//To prevent too large operating number,So use a convert.
            return intid;
        }
    }
}
