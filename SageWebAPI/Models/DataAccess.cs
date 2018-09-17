using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;

namespace SageWebAPI.Models
{
    public class DataAccess
    {
        MongoClient _client;
        
        MongoServer _server;
        MongoDatabase _db;
       
        public DataAccess()
        {
            _client = new MongoClient("mongodb://<sageuser>:<sageuser123>@ds253922.mlab.com:53922/sage50");
            _server = MongoClientExtensions.GetServer(this._client);
            _db = _server.GetDatabase("sage50");
        }

        public IEnumerable<Vendor> GetProducts()
        {
            return _db.GetCollection<Vendor>("Vendor").FindAll();
        }


        public Vendor GetProduct(ObjectId id)
        {
            var res = Query<Vendor>.EQ(p => p.Id, id);
            return _db.GetCollection<Vendor>("Vendor").FindOne(res);
        }

        public Vendor Create(Vendor p)
        {
            _db.GetCollection<Vendor>("Vendor").Save(p);
            return p;
        }

        public void Update(ObjectId id, Vendor p)
        {
            p.Id = id;
            var res = Query<Vendor>.EQ(pd => pd.Id, id);
            var operation = Update<Vendor>.Replace(p);
            _db.GetCollection<Vendor>("Vendor").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Vendor>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Vendor>("Products").Remove(res);
        }
    }
}


