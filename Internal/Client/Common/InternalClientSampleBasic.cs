namespace Deve.Internal.ClientApp
{
    /// <summary>
    /// It's exactly the same implementation as Deve.External.ClientApp.SdkClientSample.
    /// The differences are the referenced project and the class used as the implementation of IData.
    /// You have to take into account the differences between External.IData and Internal.IData.
    /// Here we are using the Internal.IData, so we have access to the Full Client implementation and Stats.
    /// </summary>
    public class InternalClientSampleBasic
    {
        /// <summary>
        /// IData instance to access the data.
        /// </summary>
        private IData _data;

        public InternalClientSampleBasic(IData data)
        {
            _data = data;
        }

        public void Execute()
        {
            _data.Options = new DataOptions()
            {
                LangCode = Constants.LanguageCodeSpanish
            };
            Log.Providers.AddConsole();
            //Log.Providers.AddLog4net();
            //Log.Providers.AddNLog();
#if DEBUG
            Log.Providers.AddDebug();
#endif

            ShowClients().Wait();   //It will fail because it's not authenticated
            DoLogin().Wait();
            ShowCountries().Wait();
            ShowClients().Wait();
            ShowClientStats().Wait();
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
            Log.Debug("Result:\n" + data + "\n");
        }

        private void LogError(Result result)
        {
            var msg = "Errors:\n";
            foreach (var error in result.Errors)
                msg += $"{error.Type} - {error.Description} [{error.FieldName}]\n";
            Log.Error(msg);
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
Role: {loginRes.Data.Subject.Role}
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
                    string result = string.Empty;
                    foreach (var client in clientsRes.Data)
                    {
                        result += $"{client.Id} - {client.DisplayName} (Balance: {client.Balance}) (Status: {client.Status}) [{client.Location.Latitude},{client.Location.Longitude}]\n";
                    }
                    LogResult(result);
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
    }
}
