using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositoty
{
    class SuppliersRepository : RepositoryBase<Suppliers, int>,ISuppliersRepository
    {
        private readonly IMapper _mapper;

        public SuppliersRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool FindName(string name)
        {

            var cate = _context.Suppliers.Where(t => t.SupplierName == name).ToList();
            if (cate != null) return true;
            else return false;
        }

        public async Task<List<Suppliers>> GetSuppliersByFindName(string name)
        {
            var keyword = RemoveDiacritics(name).ToLower();
            var list = await _context.Suppliers.ToListAsync();
            var result = list.Where(t => RemoveDiacritics(t.SupplierName).ToLower().Contains(keyword)).ToList();
            return result;
        }

        public async Task<Suppliers> GetSuppliersByMaterialsID(int materialID)
        {
            var material= _context.Materials.FindAsync(materialID);
            var suppliers=_context.Suppliers.Where(t=>t.SupplierID==material.Result.SupplierID).ToList().FirstOrDefault();
            return suppliers;
        }
        public string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
