using Deve.Internal;

namespace Deve.DataSource
{
    internal static class CriteriaHandlerClient
    {
        public static IQueryable<Client> Apply(IQueryable<Client> qry, CriteriaClient criteria, out string orderBy)
        {
            if (criteria.Status.HasValue)
                qry = qry.Where(x => x.Status == criteria.Status.Value);

            if (criteria.Id.HasValue)
                qry = qry.Where(x => x.Id == criteria.Id.Value);

            if (!string.IsNullOrWhiteSpace(criteria.Name))
                qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));

            if (criteria.CountryId.HasValue)
                qry = qry.Where(x => x.Location.CountryId == criteria.CountryId.Value);

            //OrderBy
            orderBy = criteria.OrderBy ?? nameof(Client.Name);
            switch (orderBy.ToLower())
            {
                case "id":
                    qry = qry.OrderBy(x => x.Id);
                    break;
                case "country":
                    qry = qry.OrderBy(x => x.Location.Country);
                    break;
                case "state":
                    qry = qry.OrderBy(x => x.Location.State);
                    break;
                case "city":
                    qry = qry.OrderBy(x => x.Location.City);
                    break;
                case "postalcode":
                    qry = qry.OrderBy(x => x.Location.PostalCode);
                    break;
                case "tradename":
                    qry = qry.OrderBy(x => x.TradeName);
                    break;
                case "taxid":
                    qry = qry.OrderBy(x => x.TaxId);
                    break;
                case "taxname":
                    qry = qry.OrderBy(x => x.TaxName);
                    break;
                default:
                    qry = qry.OrderBy(x => x.Name);
                    break;
            }

            return qry;
        }
    }
}
