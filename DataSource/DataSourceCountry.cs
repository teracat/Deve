using Deve.Criteria;
using Deve.Model;

namespace Deve.DataSource
{
    internal class DataSourceCountry : DataSourceBaseAll<Country, Country, CriteriaCountry>
    {
        #region Constructor
        public DataSourceCountry(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region DataSourceBaseAll Implementation
        public override Task<ResultGetList<Country>> Get(CriteriaCountry? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.Countries
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(Country.Name), Data.Countries.Count);
                }

                var qry = Data.Countries.AsQueryable();

                //Apply Filters
                if (criteria.Id.HasValue)
                    qry = qry.Where(x => x.Id == criteria.Id.Value);

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                    qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrWhiteSpace(criteria.IsoCode))
                    qry = qry.Where(x => x.IsoCode.Equals(criteria.IsoCode, StringComparison.InvariantCultureIgnoreCase));

                //OrderBy
                string orderBy = criteria.OrderBy ?? nameof(Country.Name);
                switch (orderBy.ToLower())
                {
                    case "id":
                        qry = qry.OrderBy(x => x.Id);
                        break;
                    case "isocode":
                        qry = qry.OrderBy(x => x.IsoCode);
                        break;
                    default:
                        qry = qry.OrderBy(x => x.Name);
                        break;
                }

                //Total Count
                int totalCount = qry.Count();

                //Limit & Offset
                if (criteria.Offset.HasValue)
                    qry = qry.Skip(criteria.Offset.Value);
                if (criteria.Limit.HasValue)
                    qry = qry.Take(criteria.Limit.Value);

                //Execute Query
                var data = qry.ToList();

                //Return result
                return Utils.ResultGetListOk(data, criteria.Offset, criteria.Limit, orderBy, totalCount);
            });
        }

        public override Task<ResultGet<Country>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                var country = Data.Countries.FirstOrDefault(x => x.Id == id);
                if (country is null)
                    return Utils.ResultGetError<Country>(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                return Utils.ResultGetOk(country);
            });
        }

        public override Task<ResultGet<ModelId>> Add(Country country)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                Data.Countries.Add(country);

                return Utils.ResultGetOk((ModelId)country);
            });
        }

        public override Task<Result> Update(Country country)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(country.Id);
                if (found is null)
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Update
                found.Name = country.Name;
                found.IsoCode = country.IsoCode;

                return Utils.ResultOk();
            });
        }

        public override Task<Result> Delete(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(id);
                if (found is null)
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Remove
                if (!Data.Countries.Remove(found))
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.Unknown);

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private Country? FindLocal(long id)
        {
            return Data.Countries.FirstOrDefault(x => x.Id == id);
        }
        #endregion
    }
}