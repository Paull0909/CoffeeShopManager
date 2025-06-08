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
    class Categories_MaterialRepository : RepositoryBase<Categories_Material, int>, ICategories_MaterialRepository
    {
        private readonly IMapper _mapper;

        public Categories_MaterialRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<Categories_Material>> GetCategoryMaterialByFindName(string name)
        {
            var keyword = RemoveDiacritics(name).ToLower();
            var list = await _context.Categories_Materials.ToListAsync();
            var result= list.Where(t=> RemoveDiacritics(t.CategoryName).ToLower().Contains(keyword)).ToList();
            return result;
        }
        // Hàm bỏ dấu tiếng Việt
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
