using Ajmera.Assessment.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.BL.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookMasterDto>> GetBooksAsync();
        Task<BookMasterDto> GetBookByIdAsync(Guid id);
        Task<BookMasterDto> SaveBookAsync(BookMasterDto bookMasterDto);
    }
}
