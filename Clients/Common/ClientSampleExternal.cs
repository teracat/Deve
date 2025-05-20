using System.Text;
using Deve.Authenticate;
using Deve.External.Data;

namespace Deve.Clients
{
    /// <summary>
    /// It's exactly the same implementation as ClientSampleInternal.
    /// The differences are the referenced project and the class used as the implementation of IData.
    /// You have to take into account the differences between External.IData and Internal.IData.
    /// Here we are using the External.IData, so we have access to the Basic Client information.
    /// </summary>
    public class ClientSampleExternal : ClientSampleBase
    {
        /// <summary>
        /// IData instance to access the data.
        /// </summary>
        private readonly IData _data;

        public ClientSampleExternal(IData data)
        {
            _data = data;
        }

        public override void Execute()
        {
            ShowClients().Wait();   //It will fail because it's not authenticated
            DoLogin().Wait();
            ShowCountries().Wait();
            ShowClients().Wait();
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
Joined: {loginRes.Data.Subject.Joined.ToShortDateString()}
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
                        result.AppendLine($"{country.Id} - {country.IsoCode} - {country.Name}");
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
                        result.AppendLine($"{client.Id} - {client.DisplayName} [{client.Latitude},{client.Longitude}]");
                    }
                    LogResult(result.ToString());
                }
            }
            catch (Exception ex)
            {
                LogResult("ShowClients => " + ex.Message);
            }
        }
    }
}
