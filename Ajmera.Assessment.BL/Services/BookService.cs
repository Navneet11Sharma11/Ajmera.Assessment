using Ajmera.Assessment.DL;
using Ajmera.Assessment.DL.Entities;
using Ajmera.Assessment.DL.Repositories;
using Ajmera.Assessment.Shared.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<BookMasterDto>> GetBooksAsync()
        {
            IEnumerable<BookMaster> bookMasterDbEntities = await _bookMasterRepository.GetDBSet().ToListAsync();

            return _mapper.Map<List<BookMasterDto>>(bookMasterDbEntities);
        }

        public async Task<BookMasterDto> GetBookByIdAsync(Guid id)
        {
            var bookMasterDbEntity = await _bookMasterRepository.GetAsync(id);

            return _mapper.Map<BookMasterDto>(bookMasterDbEntity);
        }

        public async Task<BookMasterDto> SaveBookAsync(BookMasterDto bookMasterDto)
        {
            BookMaster bookMasterDbEntity = _mapper.Map<BookMaster>(bookMasterDto);

            BookMaster newBookDbEntity = await _bookMasterRepository.CreateAsync(bookMasterDbEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookMasterDto>(newBookDbEntity);
        }
    }
}
