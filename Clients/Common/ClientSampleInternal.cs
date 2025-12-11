using System.Text;
using Deve.Authenticate;
using Deve.Internal.Data;
using Deve.Dto;

namespace Deve.Clients
{
    /// <summary>
    /// It's the same implementation as ClientSampleExternal.
    /// The differences are the referenced project and the class used as the implementation of IData.
    /// You have to take into account the differences between External.IData and Internal.IData.
    /// Here we are using the Internal.IData, so we have access to the Full Client implementation and Stats.
    /// In the Internal implementation we have full access to manage the data, so there are samples to Add a Country and to Delete it.
    /// </summary>
    public class ClientSampleInternal : ClientSampleBase
    {
        /// <summary>
        /// IData instance to access the data.
        /// </summary>
        private readonly IData _data;

        public ClientSampleInternal(IData data)
        {
            _data = data;
        }

        public override void Execute()
        {
            ShowClients().Wait();   //It will fail because it's not authenticated
            DoLogin().Wait();
            ShowCountries().Wait();
            ShowClients().Wait();
            ShowClientStats().Wait();
            var newCountryId = AddCountry().Result;
            if (newCountryId > 0)
            {
                ShowCountry(newCountryId).Wait();
            }

            ShowCountries().Wait();
            if (newCountryId > 0)
            {
                DeleteCountry(newCountryId).Wait();
            }
        }

        private async Task DoLogin()
        {
            LogTitle("Login");

            try
            {
                var loginRes = await _data.Authenticate.Login(new UserCredentials("teracat", "teracat"));
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
                    LogResult($@"Name: {loginRes.Data.Subject.Name}
Username: {loginRes.Data.Subject.Username}
Joined: {loginRes.Data.Subject.Joined:d}
Token: {loginRes.Data.Token}
Created: {loginRes.Data.Created}
Expires: {loginRes.Data.Expires}");
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
                var countriesRes = await _data.Countries.Get();
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
                        _ = result.AppendLine($"{country.Id} - {country.IsoCode} - {country.Name}");
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
                var clientsRes = await _data.Clients.Get();
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
                        _ = result.AppendLine($"{client.Id} - {client.DisplayName} (Balance: {client.Balance}) (Status: {client.Status}) [{client.Location.Latitude},{client.Location.Longitude}]");
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
                var statsRes = await _data.Stats.GetClientStats();
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

        private async Task<long> AddCountry()
        {
            LogTitle("Add Country");

            try
            {
                var addCountryRes = await _data.Countries.Add(new Country()
                {
                    IsoCode = "AD",
                    Name = "Andorra",
                });
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
            return 0;
        }

        private async Task ShowCountry(long id)
        {
            LogTitle("Show Country: " + id);

            try
            {
                var countryRes = await _data.Countries.Get(id);
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

        private async Task DeleteCountry(long id)
        {
            LogTitle("Delete Country: " + id);

            try
            {
                var delCountryRes = await _data.Countries.Delete(id);
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
}
