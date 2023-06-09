﻿using BlazorShop.Domain.Entities;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BlazorShopContext _ctx;

        public ProductRepository(BlazorShopContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Products.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Product> List()
        {
            return _ctx.Products
                .AsNoTracking()
                .ToList();
        }

        public Product SearchByName(string name)
        {
            return _ctx.Products.FirstOrDefault(x => x.Name == name);
        }

        public Product SearchById(Guid? id)
        {
            return _ctx.Products.FirstOrDefault(x => x.Id == id);
        }
    }
}
