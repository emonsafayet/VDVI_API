using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Core.Base.DBEntity
{
    public abstract class DbEntityCoreBase
    {
        [Key, Identity]
        [Column(Order = 0)]
        public int Id { get; set; }
    }

    public abstract class DbEntityBasicBase : DbEntityCoreBase
    {
        public DateTime CreateDate { get; set; }

        [UpdatedAt]
        public DateTime? ModifiedDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }

    public abstract class DbEntityWithUserBase : DbEntityBasicBase
    {
        /// <summary>
        /// User Id will be mapped here
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// User Id will be mapped here
        /// </summary>
        public int? ModifiedById { get; set; }
    }

    public abstract class DbEntityBaseWithCompanyId : DbEntityWithUserBase
    {
        public int CompanyId { get; set; }
    }

    public abstract class DbEntityFullBaseWithCompanyId : DbEntityBaseWithCompanyId
    {
        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }

    public abstract class DbBasicDropdownEntityBase : DbEntityBasicBase
    {
        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }

    public abstract class DbEntityFullBaseForAccount : DbEntityFullBaseWithCompanyId
    {
        public int CurrencyId { get; set; }

        public int? ChartOfAccountId { get; set; }
    }
}
