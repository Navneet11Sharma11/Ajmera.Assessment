using Ajmera.Assessment.Shared.Common;
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
        Task<ResultDomain> GetBooksAsync();
        Task<BookMasterDto> GetBookByIdAsync(Guid id);
        Task<ResultDomain> SaveBookAsync(BookMasterDto bookMasterDto);
    }
}
