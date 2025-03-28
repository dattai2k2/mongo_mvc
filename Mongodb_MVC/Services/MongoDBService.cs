using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Mongodb_MVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Mongodb_MVC.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Product> _productCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoSettings)
        {
            var settings = mongoSettings.Value;

            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException("MongoDB ConnectionString is missing!");
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(settings.CategoryCollection);
            _productCollection = database.GetCollection<Product>(settings.ProductCollection);
        }

        // Category CRUD
        public async Task<List<Category>> GetCategoriesAsync() => await _categoryCollection.Find(_ => true).ToListAsync();
        public async Task<List<Category>> SearchCategoriesAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await _categoryCollection.Find(_ => true).ToListAsync(); 
            }

            var filter = Builders<Category>.Filter.Regex("Name", new BsonRegularExpression(searchTerm, "i"));

            return await _categoryCollection.Find(filter).ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(string id) => await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        public async Task AddCategoryAsync(Category category) => await _categoryCollection.InsertOneAsync(category);
        public async Task UpdateCategoryAsync(string id, Category category) => await _categoryCollection.ReplaceOneAsync(c => c.Id == id, category);
        public async Task DeleteCategoryAsync(string id) => await _categoryCollection.DeleteOneAsync(c => c.Id == id);

        // Product CRUD
        public async Task<List<Category>> GetCategoriesByStatusAsync() => await _categoryCollection.Find(x=>x.Status == true).ToListAsync();
        public async Task<List<Product>> GetProductsAsync() => await _productCollection.Find(_ => true).ToListAsync();
        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await _productCollection.Find(_ => true).ToListAsync(); // Nếu không có từ khóa tìm kiếm, trả về danh sách rỗng.
            }

            // Tạo filter tìm kiếm theo Name, sử dụng biểu thức chính quy không phân biệt chữ hoa/thường
            var filter = Builders<Product>.Filter.Regex("Name", new BsonRegularExpression(searchTerm, "i"));

            // Thực hiện tìm kiếm và trả về danh sách các danh mục phù hợp
            return await _productCollection.Find(filter).ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(string id) => await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        public async Task AddProductAsync(Product product) => await _productCollection.InsertOneAsync(product);
        public async Task UpdateProductAsync(string id, Product product) => await _productCollection.ReplaceOneAsync(p => p.Id == id, product);
        public async Task DeleteProductAsync(string id) => await _productCollection.DeleteOneAsync(p => p.Id == id);
    }

}
