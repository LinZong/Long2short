using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Long2short
{
    public class ProcessShortId
    {

        //Init important components
        Base62Class base62calc = new Base62Class();
        MongoClass mongotask = new MongoClass();
        public void GetShortId(long? urlid, string LongUrl)
        {
            
            var collect = mongotask.InitDatabase("mongodb://127.0.0.1:27017", "long2short", "long2shortinfo");
            //mongodb://username:password@serveraddress/databaseName  如果要鉴权的话就改改.
            long RealUrlid = urlid ?? mongotask.CountList(collect);
            bool status = mongotask.NewShortId(collect, RealUrlid, base62calc.ReturnShortUrl(RealUrlid), LongUrl,urlid);
            if(!status)
            {
                Console.WriteLine("Some errors occurred.");
            }
            
        }

        public bool GetLongId(string shortid,out string longid)
        {
            
            var collect = mongotask.InitDatabase("mongodb://127.0.0.1:27017", "long2short", "long2shortinfo");
            var filter = Builders<BsonDocument>.Filter.Eq("shortid", shortid);
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            try
            {
                var document = collect.Find(filter).Project(projection).First();
                longid = Convert.ToString(document[2]);
                return true;
            }
            catch (InvalidOperationException)
            {
                longid = "Nothing";
                return false;
            }

        }
    }

}
       

