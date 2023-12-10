using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Options;


namespace mongodb_dotnet_example.Services
{
    public class ProductsService
    {
       private readonly IMongoCollection<Product> _products;

public ProductsService(string connectionString, string databaseName)
{
    Console.WriteLine($"ConnectionString in ProductsService: {connectionString}");
    Console.WriteLine($"DatabaseName in ProductsService: {databaseName}");

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);
    _products = database.GetCollection<Product>("products");
}


        public List<Product> Get() => _products.Find(product => true).ToList();

        public Product Get(string id) => _products.Find(product => product.Id == id).FirstOrDefault();

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(string id, Product updatedProduct) => _products.ReplaceOne(product => product.Id == id, updatedProduct);

        public void Delete(Product productForDeletion) => _products.DeleteOne(product => product.Id == productForDeletion.Id);

        public void Delete(string id) => _products.DeleteOne(product => product.Id == id);
    }
}
