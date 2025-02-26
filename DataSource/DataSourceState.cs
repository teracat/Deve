using System.Globalization;
using Deve.Criteria;
using Deve.Model;

namespace Deve.DataSource
{
    internal class DataSourceState : DataSourceBaseAll<State, State, CriteriaState>
    {
        #region Constructor
        public DataSourceState(IDataSource dataSource)
            : base(dataSource)
        {
        }
        #endregion

        #region DataSourceBaseAll Implementation
        public override Task<ResultGetList<State>> Get(CriteriaState? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.States
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(State.Name), Data.States.Count);
                }

                //Apply Filters
                var qry = Data.States.AsQueryable();

                if (criteria.Id.HasValue)
                {
                    qry = qry.Where(x => x.Id == criteria.Id.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));
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

                //OrderBy
                string orderBy = criteria.OrderBy ?? nameof(State.Name);
                switch (orderBy.ToLower(CultureInfo.InvariantCulture))
                {
                    case "id":
                        qry = qry.OrderBy(x => x.Id);
                        break;
                    case "country":
                        qry = qry.OrderBy(x => x.Country);
                        break;
                    default:
                        qry = qry.OrderBy(x => x.Name);
                        break;
                }

                return ApplyOffsetAndLimit(qry, criteria, orderBy);
            });
        }

        public override Task<ResultGet<State>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                var state = Data.States.FirstOrDefault(x => x.Id == id);
                if (state is null)
                {
                    return Utils.ResultGetError<State>(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultGetOk(state);
            });
        }

        public override Task<ResultGet<ModelId>> Add(State state)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                Data.States.Add(state);

                return Utils.ResultGetOk((ModelId)state);
            });
        }

        public override Task<Result> Update(State state)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(state.Id);
                if (found is null)
                {
                    return Utils.ResultError(DataSource.Options.LangCode, ResultErrorType.NotFound);
                }

                //Update
                found.Name = state.Name;
                found.CountryId = state.CountryId;
                found.Country = state.Country;

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
                if (!Data.States.Remove(found))
                {
                    return Utils.ResultError(DataSource.Options.LangCode, ResultErrorType.Unknown);
                }

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private State? FindLocal(long id)
        {
            return Data.States.FirstOrDefault(x => x.Id == id);
        }
        #endregion
    }
}