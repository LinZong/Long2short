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
            //mongodb://username:password@serveraddress/databaseName  如果要鉴权的话就改改.
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
       

