using System.Globalization;
using System.Text;
using Deve.Data;
using Deve.Auth.Login;
using Deve.Customers.Countries;
using Deve.Logging;

namespace Deve.Clients;

public class SampleClient : SampleBaseClient
{
    private readonly IData _data;
    private readonly ILog _log;

    public SampleClient(IData data, ILog log)
    {
        _data = data;
        _log = log;
    }

    public override async Task Execute(CancellationToken cancellationToken)
    {
        await ShowClients(cancellationToken);   //It will fail because it's not authenticated
        await DoLogin(cancellationToken);
        await ShowCountries(cancellationToken);
        await ShowClients(cancellationToken);
        await ShowClientStats(cancellationToken);
        var newCountryId = await AddCountry(cancellationToken);
        if (newCountryId != Guid.Empty)
        {
            await ShowCountry(newCountryId, cancellationToken);
        }

        await ShowCountries(cancellationToken);
        if (newCountryId != Guid.Empty)
        {
            await DeleteCountry(newCountryId, cancellationToken);
        }
    }

    private async Task DoLogin(CancellationToken cancellationToken)
    {
        LogTitle(_log, "Login");

        try
        {
            var loginRes = await _data.Auth.Login(new LoginRequest("teracat", "teracat"), cancellationToken);
            if (!loginRes.Success)
            {
                LogError(_log, loginRes);
            }
            else if (loginRes.Data is null)
            {
                LogResult(_log, "Unknown error");
            }
            else
            {
                LogResult(_log, $@"Name: {loginRes.Data.Name}
Username: {loginRes.Data.Username}
Joined: {loginRes.Data.Joined:d}
Token: {loginRes.Data.Token}
Created: {loginRes.Data.Token.Created}
Expires: {loginRes.Data.Token.Expires}");
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "Login => " + ex.Message);
        }
    }

    private async Task ShowCountries(CancellationToken cancellationToken)
    {
        LogTitle(_log, "Countries");

        try
        {
            var countriesRes = await _data.Customers.Countries.GetAsync(cancellationToken);
            if (!countriesRes.Success)
            {
                LogError(_log, countriesRes);
            }
            else if (countriesRes.Data.Count == 0)
            {
                LogResult(_log, "No Countries found!");
            }
            else
            {
                var result = new StringBuilder();
                foreach (var country in countriesRes.Data)
                {
                    _ = result.AppendLine(CultureInfo.InvariantCulture, $"{country.Id} - {country.IsoCode} - {country.Name}");
                }
                LogResult(_log, result.ToString());
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "ShowCountries => " + ex.Message);
        }
    }

    private async Task ShowClients(CancellationToken cancellationToken)
    {
        LogTitle(_log, "Clients");

        try
        {
            var clientsRes = await _data.Customers.Clients.GetAsync(cancellationToken);
            if (!clientsRes.Success)
            {
                LogError(_log, clientsRes);
            }
            else if (clientsRes.Data.Count == 0)
            {
                LogResult(_log, "No Clients found!");
            }
            else
            {
                var result = new StringBuilder();
                foreach (var client in clientsRes.Data)
                {
                    _ = result.AppendLine(CultureInfo.InvariantCulture, $"{client.Id} - {client.TradeName ?? client.Name} in {client.CityName}({client.StateName}, {client.CountryName}) (Balance: {client.Balance}) (Status: {client.Status})");
                }
                LogResult(_log, result.ToString());
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "ShowClients => " + ex.Message);
        }
    }

    private async Task ShowClientStats(CancellationToken cancellationToken)
    {
        LogTitle(_log, "Client Stats");

        try
        {
            var statsRes = await _data.Customers.Stats.GetClientStatsAsync(cancellationToken);
            if (!statsRes.Success)
            {
                LogError(_log, statsRes);
            }
            else if (statsRes.Data is null)
            {
                LogResult(_log, "Data not found!");
            }
            else
            {
                LogResult(_log, $"MinBalance={statsRes.Data.MinBalance}\nAvgBalance={statsRes.Data.AvgBalance}\nMaxBalance={statsRes.Data.MaxBalance} ");
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "ShowClientStats => " + ex.Message);
        }
    }

    private async Task<Guid> AddCountry(CancellationToken cancellationToken)
    {
        LogTitle(_log, "Add Country");

        try
        {
            var addCountryRes = await _data.Customers.Countries.AddAsync(new CountryAddRequest
            {
                Name = "Andorra",
                IsoCode = "AD"
            }, cancellationToken);
            if (!addCountryRes.Success)
            {
                LogError(_log, addCountryRes);
            }
            else if (addCountryRes.Data is null)
            {
                LogResult(_log, "No result!");
            }
            else
            {
                LogResult(_log, $"New country created with Id={addCountryRes.Data.Id}");
                return addCountryRes.Data.Id;
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "AddCountry => " + ex.Message);
        }
        return Guid.Empty;
    }

    private async Task ShowCountry(Guid id, CancellationToken cancellationToken)
    {
        LogTitle(_log, "Show Country: " + id);

        try
        {
            var countryRes = await _data.Customers.Countries.GetByIdAsync(id, cancellationToken);
            if (!countryRes.Success)
            {
                LogError(_log, countryRes);
            }
            else if (countryRes.Data is null)
            {
                LogResult(_log, "GetCountry: No result!");
            }
            else
            {
                LogResult(_log, $"Id={countryRes.Data.Id}\nName={countryRes.Data.Name}\nIsoCode={countryRes.Data.IsoCode}");
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "ShowCountry => " + ex.Message);
        }
    }

    private async Task DeleteCountry(Guid id, CancellationToken cancellationToken)
    {
        LogTitle(_log, "Delete Country: " + id);

        try
        {
            var delCountryRes = await _data.Customers.Countries.DeleteAsync(id, cancellationToken);
            if (!delCountryRes.Success)
            {
                LogError(_log, delCountryRes);
            }
            else
            {
                LogResult(_log, $"Country deleted");
            }
        }
        catch (Exception ex)
        {
            LogResult(_log, "DeleteCountry => " + ex.Message);
        }
    }
}
