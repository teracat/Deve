namespace Deve.DataSource
{
    internal class DataSourceCity : DataSourceBaseAll<City, City, CriteriaCity>
    {
        #region Constructor
        public DataSourceCity(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
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
                    qry = qry.Where(x => x.Id == criteria.Id.Value);

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                    qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));

                if (criteria.StateId.HasValue)
                    qry = qry.Where(x => x.StateId == criteria.StateId.Value);

                if (criteria.CountryId.HasValue)
                    qry = qry.Where(x => x.CountryId == criteria.CountryId.Value);

                if (!string.IsNullOrWhiteSpace(criteria.IsoCode))
                {
                    var countriesRes = DataSourceMain.Countries.Get(new CriteriaCountry()
                    {
                        IsoCode = criteria.IsoCode
                    }).Result;
                    if (countriesRes.Success)
                    {
                        var countriesIds = countriesRes.Data?
                                                       .Select(x => x.Id)
                                                       .ToList();
                        if (countriesIds is not null)
                            qry = qry.Where(x => countriesIds.Contains(x.CountryId));
                    }
                }

                if (!string.IsNullOrWhiteSpace(criteria.State))
                {
                    var statesRes = DataSourceMain.States.Get(new CriteriaState()
                    {
                        Name = criteria.State
                    }).Result;
                    if (statesRes.Success)
                    {
                        var statesIds = statesRes.Data?
                                                 .Select(x => x.Id)
                                                 .ToList();
                        if (statesIds is not null)
                            qry = qry.Where(x => statesIds.Contains(x.StateId));
                    }
                }

                //OrderBy
                string orderBy = criteria.OrderBy ?? nameof(City.Name);
                switch (orderBy.ToLower())
                {
                    case "id":
                        qry = qry.OrderBy(x => x.Id);
                        break;
                    case "country":
                        qry = qry.OrderBy(x => x.Country);
                        break;
                    case "state":
                        qry = qry.OrderBy(x => x.State);
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

        public override Task<ResultGet<City>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (id <= 0)
                    return Utils.ResultGetError<City>(ResultErrorType.MissingRequiredField, nameof(City.Id));

                var city = Data.Cities.FirstOrDefault(x => x.Id == id);
                if (city is null)
                    return Utils.ResultGetError<City>(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

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
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

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
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Remove
                if (!Data.Cities.Remove(found))
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private City? FindLocal(long id)
        {
            return Data.Cities.FirstOrDefault(x => x.Id == id);
        }
        #endregion
    }
}
