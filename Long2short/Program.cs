using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;



namespace Long2short
{
    class Program
    {
        static void Main(string[] args)
        {
            //中国有句话，叫闷声发大财，这是最好的.
            //但是我想想觉得一句话不说也不好.
            //ProcessShortId p = new ProcessShortId();
            //p下有两个方法，GetLongId和GetShortId，前者用于服务器请求原始链接，后者用于用户或者批量生成短链接.
            
            
        }


    }

    class ProcessShortId
    {

        //Init important components
        Base62Class base62calc = new Base62Class();
        MongoClass mongotask = new MongoClass();

        public void GetShortId(long? urlid, string LongUrl)
        {
            var collect = mongotask.InitDatabase("mongodb://127.0.0.1:27017", "long2short", "long2shortinfo");
            long RealUrlid = urlid ?? mongotask.CountList(collect);
            bool status = mongotask.NewShortId(collect, Convert.ToInt32(RealUrlid), base62calc.ReturnShortUrl(Convert.ToInt32(RealUrlid)), LongUrl,urlid);
            if(!status)
            {
                Console.WriteLine("Some errors occurred.");
            }
            
        }

        public BsonValue GetLongId(string shortid)
        {
            var collect = mongotask.InitDatabase("mongodb://127.0.0.1:27017", "long2short", "long2shortinfo");
            var filter = Builders<BsonDocument>.Filter.Eq("shortid", shortid);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            var document = collect.Find(filter).Project(projection).First();
            return document[2];
        }
    }

}
       

