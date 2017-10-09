using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Long2short
{
    class Base62Class
    {
        Dictionary<long, char> Base62Dic = new Dictionary<long, char>();
        public Base62Class()
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

        }
        public List<long> GetBase62(long input)
        {
            
            List<long> Base62Value = new List<long>();
            while (input > 0)
            {
                long remain = input % 62;
                Base62Value.Add(remain);
                input = input / 62;
            }
            return Base62Value;
        }

        public string ReturnShortUrl(long UrlId)
        {
            
            string s = "";
            var base62result = GetBase62(UrlId);
            
            for (int i = 0; i < (6 - base62result.Count()); i++)
            {
                s = s + Base62Dic[0];
            }
            for (int i = base62result.Count(); i > 0; i--)
            {
                s = s + Base62Dic[base62result[i - 1]];
            }
            //Dic.Clear();
            return "http://zap.tech/" + s;
        }

        //public long GetUrlId(string OriShortUrl)
        //{
        //    char[] KeyCharList = OriShortUrl.ToCharArray();
        //    double id = 0;
        //    double counter = KeyCharList.Length - 1;
        //    //Dictionary<int, char> Dic = CreateDic();
        //    foreach (var i in KeyCharList)
        //    {
        //        var c = Base62Dic.First(r => r.Value == i).Key;
        //        id += (c) * Math.Pow(62, counter);
        //        counter--;
        //    }
        //    //Dic.Clear();

        //    return (long)id;
        //}
        //这个东西是在没有数据库的时候通过62进制换算成10进制的东西,接上了数据库的话基本上就没用了.
    }
}
