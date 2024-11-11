using Deve.Sdk;
using Deve.External.Sdk;

namespace Deve.External.ClientApp
{
    /// <summary>
    /// It's exactly the same implementation as Deve.Internal.Client.Embedded.
    /// The differences are the referenced project and the class used as the implementation of IData.
    /// You have to take into account the differences between External.IData and Internal.IData.
    /// Here we are using the External.IData, so we have access to the Basic Client information.
    /// </summary>
    public class SdkClientSample
    {
        /// <summary>
        /// IData instance to access the data.
        /// </summary>
        private IData data = SdkFactory.Get(EnvironmentType.Production, null, new LoggingHandlerLog());

        public void Execute()
        {
            data.Options = new DataOptions()
            {
                LangCode = Constants.LanguageCodeSpanish
            };

            ShowClients().Wait();   //It will fail because it's not authenticated
            DoLogin().Wait();
            ShowCountries().Wait();
            ShowClients().Wait();
        }

        private void LogCharacters(char character, int count)
        {
            Log.Debug(new string(character, count));
        }

        private void LogTitle(string title)
        {
            LogCharacters('#', 80);
            Log.Debug("# " + title);
            LogCharacters('#', 80);
        }

        private void LogResult(string data)
        {
            LogCharacters('*', 50);
            Log.Debug("* Result:");
            LogCharacters('*', 50);
            Log.Debug("\n" + data + "\n");
            LogCharacters('*', 50);
        }

        private void LogError(Result result)
        {
            var msg = "Errors: ";
            foreach (var error in result.Errors)
                msg += $"{error.Type} - {error.Description} [{error.FieldName}]\n";
            LogResult(msg);
        }

        private async Task DoLogin()
        {
            LogTitle("Login");

            try
            {
                var loginRes = await data.Authenticate.Login(new UserCredentials("teracat", "teracat"));
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
                var countriesRes = await data.Countries.Get();
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
                    string result = string.Empty;
                    foreach (var country in countriesRes.Data)
                    {
                        result += $"{country.Id} - {country.IsoCode} - {country.Name}\n";
                    }
                    LogResult(result);
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
                var clientsRes = await data.Clients.Get();
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
                    string result = string.Empty;
                    foreach (var client in clientsRes.Data)
                    {
                        result += $"{client.Id} - {client.DisplayName} [{client.Latitude},{client.Longitude}]\n";
                    }
                    LogResult(result);
                }
            }
            catch (Exception ex)
            {
                LogResult("ShowClients => " + ex.Message);
            }
        }
    }
}
