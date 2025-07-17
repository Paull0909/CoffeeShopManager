using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    class ProductsRepository : RepositoryBase<Products, int>,IProductsRepository
    {
        private readonly IMapper _mapper;

        public ProductsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool FindName(string name)
        {
            var pr = _context.Products.Where(t => t.ProductName == name).ToList();
            if (pr != null) return true;
            else return false;
        }

        public async Task<List<Products>> GetAllIsAvailable()
        {
            var pr = _context.Products.Where(t => t.IsAvailable == true).ToList();
            return pr;
        }

        public async Task<List<Products>> GetByCategory(int id)
        {
            var pr = await _context.Products.Where(t => t.CategoryID == id).ToListAsync();
            return pr;
        }
    }
}
