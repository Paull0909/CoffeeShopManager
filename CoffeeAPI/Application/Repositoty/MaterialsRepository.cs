using Application.IRepositoty;
using Application.Service;
using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace Application.Repositoty
{
    class MaterialsRepository : RepositoryBase<Materials, int>,IMaterialsRepository
    {
        private readonly IMapper _mapper;

        public MaterialsRepository(Web_Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool FindName(string name)
        {
            var mate = _context.Materials.Where(t => t.MaterialName == name).ToList();
            if (mate != null) return true;
            else return false;
        }

        public async Task<List<Materials>> GetByCategory(int id)
        {
            var mate = await _context.Materials.Where(t => t.CategoryID == id).ToListAsync();
            return mate;
        }

        public async Task<List<Lot>> GetByLotByMaterialsID(int id)
        {
            var lot = await _context.Lots.Where(t => t.MaterialID == id).ToListAsync();
            return lot;
        }

        public async Task<List<Materials>> GetBySuppliers(int id)
        {
            var mate = await _context.Materials.Where(t => t.SupplierID == id).ToListAsync();
            return mate;
        }

        public async Task<Lot> GetLotByID(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            return lot;
        }

        public async Task<List<Materials>> GetMaterialByFindName(string name)
        {
            var keyword = RemoveDiacritics(name).ToLower();
            var list = await _context.Materials.ToListAsync();
            var result = list.Where(t => RemoveDiacritics(t.MaterialName).ToLower().Contains(keyword)).ToList();
            return result;
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
