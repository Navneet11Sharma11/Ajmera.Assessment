using Ajmera.Assessment.DL;
using Ajmera.Assessment.DL.Entities;
using Ajmera.Assessment.DL.Repositories;
using Ajmera.Assessment.Shared.Common;
using Ajmera.Assessment.Shared.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ajmera.Assessment.BL.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BookMaster> _bookMasterRepository;

        public BookService(IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<BookService> logger,
             IRepository<BookMaster> bookMasterRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _bookMasterRepository = bookMasterRepository;
        }

        public async Task<ResultDomain> GetBooksAsync()
        {
            IEnumerable<BookMaster> bookMasterDbEntities = await _bookMasterRepository.GetDBSet().ToListAsync();

            var result = new ResultDomain();
            result.Data = _mapper.Map<List<BookMasterDto>>(bookMasterDbEntities);
            result.IsSuccess = true;
            result.TotalCount = bookMasterDbEntities.Count();

            return result;
        }

        public async Task<BookMasterDto> GetBookByIdAsync(Guid id)
        {
            var bookMasterDbEntity = await _bookMasterRepository.GetAsync(id);

            return _mapper.Map<BookMasterDto>(bookMasterDbEntity);
        }

        public async Task<ResultDomain> SaveBookAsync(BookMasterDto bookMasterDto)
        {
            var result = new ResultDomain();

            BookMaster bookMasterDbEntity = _mapper.Map<BookMaster>(bookMasterDto);

            BookMaster newBookDbEntity = await _bookMasterRepository.CreateAsync(bookMasterDbEntity);
            await _unitOfWork.SaveChangesAsync();

            result.Data = _mapper.Map<BookMasterDto>(newBookDbEntity);
            result.IsSuccess = true;
            result.Message = ConstantMessages.SaveBookMessage;

            return result;
        }
    }
}
