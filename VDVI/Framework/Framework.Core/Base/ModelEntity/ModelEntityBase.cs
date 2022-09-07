using Framework.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Framework.Core.Base.ModelEntity
{
    [Serializable]
    public abstract class ModelEntityCoreBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    [Serializable]
    public abstract class ModelEntityBasicBase : ModelEntityCoreBase
    {
        [JsonProperty("createDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }

        public void MarkAsDeleted()
        {
            IsActive = false;
            IsDeleted = true;
        }

        public void MarkAsActive()
        {
            IsActive = true;
            IsDeleted = false;
        }
    }

    [Serializable]
    public abstract class ModelEntityWithUserBase : ModelEntityBasicBase
    {
        /// <summary>
        /// User Id will be mapped here
        /// </summary>
        [JsonIgnore]
        public int? CreatedById { get; set; }

        [JsonProperty("createdByName")]
        public string CreatedByName { get; set; }

        /// <summary>
        /// User Id will be mapped here
        /// </summary>
        [JsonIgnore]
        public int? ModifiedById { get; set; }

        [JsonProperty("modifiedByName")]
        public string ModifiedByName { get; set; }

        public virtual void AttachCreatedInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return;

            CreatedById = httpContextAccessor.HttpContext.User.Identity.GetUserId().Value;
            MarkAsActive();
        }

        public virtual void AttachUpdateInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return;

            ModifiedById = httpContextAccessor.HttpContext.User.Identity.GetUserId().Value;
            MarkAsActive();
        }
    }

    [Serializable]
    public abstract class ModelEntityBaseWithCompanyId : ModelEntityWithUserBase
    {
        [JsonIgnore]
        public int CompanyId { get; set; }

        public override void AttachCreatedInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return;

            base.AttachCreatedInfo(httpContextAccessor);
            CompanyId = httpContextAccessor.HttpContext.User.Identity.GetCompanyId().Value;
        }

        public override void AttachUpdateInfo(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return;

            base.AttachUpdateInfo(httpContextAccessor);
        }
    }

    [Serializable]
    public abstract class ModelEntityFullBaseWithCompanyId : ModelEntityBaseWithCompanyId
    {
        private string _code;

        [JsonProperty("code")]
        public string Code
        {
            get => _code;
            set => _code = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }

    [Serializable]
    public abstract class ModelBasicDropdownEntityBase : ModelEntityBasicBase
    {
        private string _code;

        [JsonProperty("code")]
        public string Code
        {
            get => _code;
            set => _code = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        [JsonProperty("label")]
        public string Label
        {
            get { return $"{Name} { (string.IsNullOrWhiteSpace(Code) ? "" : $"({Code})")}"; }
        }
    }

    [Serializable]
    public abstract class ModelEntityFullBaseForAccount : ModelEntityFullBaseWithCompanyId
    {
        private int? _chartOfAccountId;

        [JsonProperty("currencyId")]
        public int? CurrencyId { get; set; }

        [JsonProperty("chartOfAccountId")]
        public int? ChartOfAccountId
        {
            get => _chartOfAccountId;
            set => _chartOfAccountId = value;
        }
    }
}