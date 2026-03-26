#pragma warning disable CA1303 // Do not pass literals as localized parameters
using Deve;
using Deve.Clients;
using Deve.Data;
using Deve.Logging;

var log = new MultiLog();
log.AddConsole();
//log.AddLog4Net();
//log.AddNLog();
//log.AddSerilog();
//log.AddSentry();

//-:cnd
#if DEBUG
log.AddDebug();
#endif
//+:cnd

var options = new DataOptions()
{
    LangCode = Constants.LanguageCodeSpanish
};

//////////////////////////////////////////////////////////////////////////////////////////////////////
// Sdk (Api must be running)
//////////////////////////////////////////////////////////////////////////////////////////////////////
SampleBaseClient.LogTitle(log, "Sdk...");

await SampleExecutorsClient.Sdk(options, log, CancellationToken.None);

Console.WriteLine("Sdk done. Press a key to continue...");
Console.ReadKey();


//////////////////////////////////////////////////////////////////////////////////////////////////////
// Embedded (uses Core, no other projects must be running)
//////////////////////////////////////////////////////////////////////////////////////////////////////
SampleBaseClient.LogTitle(log, "Embedded...");

await SampleExecutorsClient.Embedded(options, log, CancellationToken.None);

//////////////////////////////////////////////////////////////////////////////////////////////////////
// Finish
//////////////////////////////////////////////////////////////////////////////////////////////////////
Console.WriteLine("Embedded done. Press a key to finish...");
Console.ReadKey();

#pragma warning restore CA1303 // Do not pass literals as localized parameters
