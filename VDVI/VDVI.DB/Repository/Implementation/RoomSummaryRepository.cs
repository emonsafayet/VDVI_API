using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDVI.DB.DbModels.RoomSummary;
using VDVI.DB.Dtos.RoomSummary;
using VDVI.Repository.DbContext;
using VDVI.Repository.Repository.Interfaces;

namespace VDVI.Repository.Repository.Implementation
{
    public class RoomSummaryRepository : DapperRepository<DbRoomSummary>, IRoomSummaryRepository
    {

        private readonly VDVISchedulerDbContext _dbContext;

        public RoomSummaryRepository(VDVISchedulerDbContext dbContext) : base(dbContext.Connection)
        {
            _dbContext = dbContext;
        }


        public async Task<RoomSummaryDto> InsertAsync(RoomSummaryDto dto)
        {
            var dbEntity = TinyMapper.Map<DbRoomSummary>(dto);

            //dbEntity.CreateDate = DateTime.UtcNow;

            await _dbContext.RoomSummary.InsertAsync(dbEntity);

            return TinyMapper.Map<RoomSummaryDto>(dbEntity);
        }


        public async Task<List<RoomSummaryDto>> BulkInsertAsync(List<RoomSummaryDto> dto)
        {
            var dbEntity = TinyMapper.Map<List<DbRoomSummary>>(dto);

            //dbEntity.CreateDate = DateTime.UtcNow;

            await _dbContext.RoomSummary.BulkInsertAsync(dbEntity);

            return dto;
        }

        public async Task<RoomSummaryDto> UpdateAsync(RoomSummaryDto dto)
        {
            var dbCustomerEntity = TinyMapper.Map<DbRoomSummary>(dto);

            //dbCustomerEntity.ModifiedDate = DateTime.UtcNow;

            await _dbContext.RoomSummary.UpdateAsync(dbCustomerEntity);

            return dto;
        }

        public async Task<List<RoomSummaryDto>> FindAllByPropertyCodeAsync(string propertyCode)
        {
            IEnumerable<DbRoomSummary> dbEntities = await _dbContext
                .RoomSummary
                .SetOrderBy(OrderInfo.SortDirection.DESC, x => x.PropertyCode)
                .FindAllAsync(x => x.PropertyCode == propertyCode);


            var entities = TinyMapper.Map<List<RoomSummaryDto>>(dbEntities);

            return entities;
        }

        public async Task<RoomSummaryDto> FindByIdAsync(int id)
        {
            var dbEntity = await _dbContext.RoomSummary.FindAsync(x => x.PropertyCode == "");

            var dto = TinyMapper.Map<RoomSummaryDto>(dbEntity);
            //dto.CreatedByName = dbEntity.EmployeeCreatedBy.Name;

            //if (dbEntity.ModifiedBy.HasValue)
            //{
            // var modifiedEmployee = await _dbContext.Employee.FindAsync(x => x.Id == dbEntity.ModifiedBy);
            //dto.ModifiedByName = modifiedEmployee.Name;
            //}

            return dto;
        }



    }
}
