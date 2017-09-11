using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Long2short
{
    class MongoClass
    {
        public IMongoCollection<BsonDocument> InitDatabase(string ClientAddr, string DatabaseName, string CollectionName)
        {
            var client = new MongoClient(ClientAddr);
            var database = client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);
            return collection;
        }

        public bool NewShortId(IMongoCollection<BsonDocument> CollectionHandler, int insertUrlId, string insertShortUrlId, string insertLongUrlId,long? UserInputId)
        {
            
            var filter = Builders<BsonDocument>.Filter.Eq("id",insertUrlId);
            var ExistChecker = CollectionHandler.Find(filter);
            Base62Class b = new Base62Class();
            if (ExistChecker.Count() != 0)
            {
                if (UserInputId == null)
                {
                    insertUrlId++;
                    try
                    {
                        var document = new BsonDocument
                    {
                    {"id",insertUrlId },
                    {"shortid",b.ReturnShortUrl(Convert.ToInt32(insertUrlId)) },
                    {"longid",insertLongUrlId }
                    };
                        CollectionHandler.InsertOne(document);
                        Console.WriteLine(b.ReturnShortUrl(Convert.ToInt32(insertUrlId)));
                        return true;
                    }
                    catch
                    {

                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("你钦定的ID已经被使用,获取短链接失败.");
                    return false;
                }

            }
            else
            {
                try
                {
                    var document = new BsonDocument
                {
                    {"id",insertUrlId },
                    {"shortid",insertShortUrlId },
                    {"longid",insertLongUrlId }
                };
                    CollectionHandler.InsertOne(document);
                    Console.WriteLine(insertShortUrlId);
                    return true;
                }
                catch
                {

                    return false;
                }
            }
            
            
            

        }
        public long CountList(IMongoCollection<BsonDocument> CollectionHandler)
        {
            return CollectionHandler.Count(new BsonDocument());
        }
    }
}
