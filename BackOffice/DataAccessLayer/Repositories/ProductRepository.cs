using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MatrixIncDbContext _context;

        public ProductRepository(MatrixIncDbContext context) 
        {
            _context = context;
        }
        // add products
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        // Delete products
        public void DeleteProductsById(List<int> productIds)
        {
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            if (products.Any())
            {
                _context.Products.RemoveRange(products);
                _context.SaveChanges();
            }
        }
        public void DeleteProductById(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.Parts);
        }

        public Product? GetProductById(int id)
        {
            return _context.Products.Include(p => p.Parts).FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void UpdateProductsById(List<int> productIds)
        {
            var products = _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
            foreach (var product in products)
            {
                _context.Products.Update(product);
            }
            _context.SaveChanges();
        }
    }
}
