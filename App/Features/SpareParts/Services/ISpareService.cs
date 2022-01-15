using App.Features.SpareParts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Features.SpareParts.Services
{
    public interface ISpareService
    {
        Task<List<SparePart>> GetAllSpares();
        Task<bool> AddSpare(SparePart spare);
        Task<SparePart> GetSpare(int id);
        Task<bool> UpdateSpare(SparePart spare);
        Task<bool> DeleteSpare(int id);
    }
}
