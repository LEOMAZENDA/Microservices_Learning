using AutoMapper;
using GreekShoping.ProductApi.Data.ValueObjects;
using GreekShoping.ProductApi.Models;
using GreekShoping.ProductApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GreekShoping.ProductApi.Repository._Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Create(ProductVO vO)
        {
            throw new NotImplementedException();
        }
        public async Task<ProductVO> Update(ProductVO vO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
