using System.Globalization;
using System.Text;
using Deve.Data;
using Deve.Auth.Login;
using Deve.Customers.Countries;

namespace Deve.Clients;

public class SampleClient : SampleBaseClient
{
    private readonly IData _data;

    public SampleClient(IData data)
    {
        _data = data;
    }

    public override async Task Execute()
    {
        await ShowClients();   //It will fail because it's not authenticated
        await DoLogin();
        await ShowCountries();
        await ShowClients();
        await ShowClientStats();
        var newCountryId = await AddCountry();
        if (newCountryId != Guid.Empty)
        {
            await ShowCountry(newCountryId);
        }

        await ShowCountries();
        if (newCountryId != Guid.Empty)
        {
            await DeleteCountry(newCountryId);
        }
    }

    private async Task DoLogin()
    {
        LogTitle("Login");

        try
        {
            var loginRes = await _data.Auth.Login(new LoginRequest("teracat", "teracat"));
            if (!loginRes.Success)
            {
                LogError(loginRes);
            }
            else if (loginRes.Data is null)
            {
                LogResult("Unknown error");
            }
            else
            {
                LogResult($@"Name: {loginRes.Data.Name}
Username: {loginRes.Data.Username}
Joined: {loginRes.Data.Joined:d}
Token: {loginRes.Data.Token}
Created: {loginRes.Data.Token.Created}
Expires: {loginRes.Data.Token.Expires}");
            }
        }
        catch (Exception ex)
        {
            LogResult("Login => " + ex.Message);
        }
    }

    private async Task ShowCountries()
    {
        LogTitle("Countries");

        try
        {
            var countriesRes = await _data.Customers.Countries.GetAsync();
            if (!countriesRes.Success)
            {
                LogError(countriesRes);
            }
            else if (countriesRes.Data.Count == 0)
            {
                LogResult("No Countries found!");
            }
            else
            {
                var result = new StringBuilder();
                foreach (var country in countriesRes.Data)
                {
                    _ = result.AppendLine(CultureInfo.InvariantCulture, $"{country.Id} - {country.IsoCode} - {country.Name}");
                }
                LogResult(result.ToString());
            }
        }
        catch (Exception ex)
        {
            LogResult("ShowCountries => " + ex.Message);
        }
    }

    private async Task ShowClients()
    {
        LogTitle("Clients");

        try
        {
            var clientsRes = await _data.Customers.Clients.GetAsync();
            if (!clientsRes.Success)
            {
                LogError(clientsRes);
            }
            else if (clientsRes.Data.Count == 0)
            {
                LogResult("No Clients found!");
            }
            else
            {
                var result = new StringBuilder();
                foreach (var client in clientsRes.Data)
                {
                    _ = result.AppendLine(CultureInfo.InvariantCulture, $"{client.Id} - {client.TradeName ?? client.Name} in {client.CityName}({client.StateName}, {client.CountryName}) (Balance: {client.Balance}) (Status: {client.Status})");
                }
                LogResult(result.ToString());
            }
        }
        catch (Exception ex)
        {
            LogResult("ShowClients => " + ex.Message);
        }
    }

    private async Task ShowClientStats()
    {
        LogTitle("Client Stats");

        try
        {
            var statsRes = await _data.Customers.Stats.GetClientStatsAsync();
            if (!statsRes.Success)
            {
                LogError(statsRes);
            }
            else if (statsRes.Data is null)
            {
                LogResult("Data not found!");
            }
            else
            {
                LogResult($"MinBalance={statsRes.Data.MinBalance}\nAvgBalance={statsRes.Data.AvgBalance}\nMaxBalance={statsRes.Data.MaxBalance} ");
            }
        }
        catch (Exception ex)
        {
            LogResult("ShowClientStats => " + ex.Message);
        }
    }

    private async Task<Guid> AddCountry()
    {
        LogTitle("Add Country");

        try
        {
            var addCountryRes = await _data.Customers.Countries.AddAsync(new CountryAddRequest(Name: "Andorra", IsoCode: "AD"));
            if (!addCountryRes.Success)
            {
                LogError(addCountryRes);
            }
            else if (addCountryRes.Data is null)
            {
                LogResult("No result!");
            }
            else
            {
                LogResult($"New country created with Id={addCountryRes.Data.Id}");
                return addCountryRes.Data.Id;
            }
        }
        catch (Exception ex)
        {
            LogResult("AddCountry => " + ex.Message);
        }
        return Guid.Empty;
    }

    private async Task ShowCountry(Guid id)
    {
        LogTitle("Show Country: " + id);

        try
        {
            var countryRes = await _data.Customers.Countries.GetByIdAsync(id);
            if (!countryRes.Success)
            {
                LogError(countryRes);
            }
            else if (countryRes.Data is null)
            {
                LogResult("GetCountry: No result!");
            }
            else
            {
                LogResult($"Id={countryRes.Data.Id}\nName={countryRes.Data.Name}\nIsoCode={countryRes.Data.IsoCode}");
            }
        }
        catch (Exception ex)
        {
            LogResult("ShowCountry => " + ex.Message);
        }
    }

    private async Task DeleteCountry(Guid id)
    {
        LogTitle("Delete Country: " + id);

        try
        {
            var delCountryRes = await _data.Customers.Countries.DeleteAsync(id);
            if (!delCountryRes.Success)
            {
                LogError(delCountryRes);
            }
            else
            {
                LogResult($"Country deleted");
            }
        }
        catch (Exception ex)
        {
            LogResult("DeleteCountry => " + ex.Message);
        }
    }
}
