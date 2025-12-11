using System.Globalization;
using Deve.Dto;

namespace Deve.DataSource
{
    internal class DataSourceCity : DataSourceBaseAll<City, City, CriteriaCity>
    {
        #region Constructor
        public DataSourceCity(IDataSource dataSource)
            : base(dataSource)
        {
        }
        #endregion

        #region DataSourceBaseAll Implementation
        public override Task<ResultGetList<City>> Get(CriteriaCity? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.Cities
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(City.Name), Data.Cities.Count);
                }

                var qry = Data.Cities.AsQueryable();

                //Apply Filters
                if (criteria.Id.HasValue)
                {
                    qry = qry.Where(x => x.Id == criteria.Id.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));
                }

                if (criteria.StateId.HasValue)
                {
                    qry = qry.Where(x => x.StateId == criteria.StateId.Value);
                }

                if (criteria.CountryId.HasValue)
                {
                    qry = qry.Where(x => x.CountryId == criteria.CountryId.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.IsoCode))
                {
                    var countriesRes = DataSource.Countries.Get(new CriteriaCountry()
                    {
                        IsoCode = criteria.IsoCode
                    }).Result;
                    if (countriesRes.Success)
                    {
                        var countriesIds = countriesRes.Data?
                                                       .Select(x => x.Id)
                                                       .ToList();
                        if (countriesIds is not null)
                        {
                            qry = qry.Where(x => countriesIds.Contains(x.CountryId));
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(criteria.State))
                {
                    var statesRes = DataSource.States.Get(new CriteriaState()
                    {
                        Name = criteria.State
                    }).Result;
                    if (statesRes.Success)
                    {
                        var statesIds = statesRes.Data?
                                                 .Select(x => x.Id)
                                                 .ToList();
                        if (statesIds is not null)
                        {
                            qry = qry.Where(x => statesIds.Contains(x.StateId));
                        }
                    }
                }

                //OrderBy
                string orderBy = criteria.OrderBy ?? nameof(City.Name);
                qry = orderBy.ToLower(CultureInfo.InvariantCulture) switch
                {
                    "id" => qry.OrderBy(x => x.Id),
                    "country" => qry.OrderBy(x => x.Country),
                    "state" => qry.OrderBy(x => x.State),
                    _ => qry.OrderBy(x => x.Name),
                };
                return ApplyOffsetAndLimit(qry, criteria, orderBy);
            });
        }

        public override Task<ResultGet<City>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                var city = Data.Cities.FirstOrDefault(x => x.Id == id);
                if (city is null)
                {
                    return Utils.ResultGetError<City>(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultGetOk(city);
            });
        }

        public override Task<ResultGet<ModelId>> Add(City city)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                Data.Cities.Add(city);
                return Utils.ResultGetOk((ModelId)city);
            });
        }

        public override Task<Result> Update(City city)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(city.Id);
                if (found is null)
                {
                    return Utils.ResultError(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                //Update
                found.Name = city.Name;
                found.StateId = city.StateId;
                found.State = city.State;
                found.CountryId = city.CountryId;
                found.Country = city.Country;

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
                {
                    return Utils.ResultError(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                //Remove
                if (!Data.Cities.Remove(found))
                {
                    return Utils.ResultError(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private City? FindLocal(long id) => Data.Cities.FirstOrDefault(x => x.Id == id);
        #endregion
    }
}