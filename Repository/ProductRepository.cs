using System.Text;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApp_vSem3.Abstraction;
using WebApp_vSem3.Data;
using WebApp_vSem3.DTO;
using WebApp_vSem3.Models;

namespace WebApp_vSem3.Repository
{
    public class ProductRepository(WebAppContext _webAppCtx, IMapper _mapper, IMemoryCache _memoryCache) : IProductRepository
    {
        public int AddProduct(ProductModel productModel)
        {
            using (_webAppCtx)
            {
                if (_webAppCtx.Products.Any(p => p.Name == productModel.Name))
                    throw new Exception("Уже есть продукт с таким именем");

                var entity = _mapper.Map<Product>(productModel);
                _webAppCtx.Products.Add(entity);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("products");
                return entity.Id;
            }
        }


        public void DeleteProduct(int id)
        {
            using (_webAppCtx)
            {
                var product = _webAppCtx.Products.Find(id);
                if (product == null)
                    throw new Exception($"Нет продукта с id = {id}");

                _webAppCtx.Products.Remove(product);
                _webAppCtx.SaveChanges();
                _memoryCache.Remove("products");
            }
        }


        public IEnumerable<ProductModel> GetAllProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductModel>? listModel))
            {
                return listModel!;
            }
            else
            {
                using (_webAppCtx)
                {
                    listModel = _webAppCtx.Products.Select(_mapper.Map<ProductModel>).ToList();
                    _memoryCache.Set("products", listModel, TimeSpan.FromMinutes(30));
                    return listModel;
                }
            }
        }

        public string GetStringCsv(IEnumerable<ProductModel> products)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("﻿\ufeff");
            foreach (var product in products)
            {
                sb.AppendLine($"{product.Name};{product.Price};{product.Description};{product.ProductGroupId}");
            }
            return sb.ToString();
        }

        public (byte[] Content, string FileName) GetProductsCsv()
        {
            string content;
            using (_webAppCtx)
            {
                var products = _webAppCtx.Products.Select(_mapper.Map<ProductModel>).ToList();
                content = GetStringCsv(products);
            }
            
            string fileName = "products.csv";
            string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName);
            System.IO.File.WriteAllText(filePath, content, Encoding.UTF8);
            return (File.ReadAllBytes(filePath), fileName);
        }
    }
}
