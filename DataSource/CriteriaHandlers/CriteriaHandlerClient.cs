using System.Globalization;
using Deve.Internal.Criteria;
using Deve.Internal.Model;

namespace Deve.DataSource.CriteriaHandlers
{
    internal static class CriteriaHandlerClient
    {
        public static IQueryable<Client> Apply(IQueryable<Client> qry, CriteriaClient criteria, out string orderBy)
        {
            if (criteria.Status.HasValue)
            {
                qry = qry.Where(x => x.Status == criteria.Status.Value);
            }

            if (criteria.Id.HasValue)
            {
                qry = qry.Where(x => x.Id == criteria.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase) ||
                                     (!string.IsNullOrEmpty(x.TradeName) && x.TradeName.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase)) ||
                                     (!string.IsNullOrEmpty(x.TaxName) && x.TaxName.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase)));
            }

            if (criteria.CountryId.HasValue)
            {
                qry = qry.Where(x => x.Location.CountryId == criteria.CountryId.Value);
            }

            //OrderBy
            orderBy = criteria.OrderBy ?? nameof(Client.Name);
            qry = orderBy.ToLower(CultureInfo.InvariantCulture) switch
            {
                "id" => qry.OrderBy(x => x.Id),
                "country" => qry.OrderBy(x => x.Location.Country),
                "state" => qry.OrderBy(x => x.Location.State),
                "city" => qry.OrderBy(x => x.Location.City),
                "postalcode" => qry.OrderBy(x => x.Location.PostalCode),
                "tradename" => qry.OrderBy(x => x.TradeName),
                "taxid" => qry.OrderBy(x => x.TaxId),
                "taxname" => qry.OrderBy(x => x.TaxName),
                _ => qry.OrderBy(x => x.Name),
            };
            return qry;
        }
    }
}