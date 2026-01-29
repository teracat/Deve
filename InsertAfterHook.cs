// InsertAfterHook.cs
// Usage:
//   dotnet run InsertAfterHook.cs -- [--smart-comma=true|false] [--extra-eol=none|start|end|both] "<filePath>" "<hook>" "<textToInsert>"
// Exit codes:
//   0   success
//   2   hook not found
//   64  invalid arguments
//   66  file not found
//   74  I/O error
namespace InsertAfterHook;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable

internal static class Program
{
    // Exit codes
    private const int EX_OK = 0;
    private const int EX_HOOKNOTFOUND = 2;
    private const int EX_INVALIDARGS = 64;
    private const int EX_FILENOTFOUND = 66;
    private const int EX_IOERR = 74;

    // Reuse newline split array
    private static readonly string[] NewLineSplit = new[] { "\r\n", "\n" };

    private enum ExtraEolOption
    {
        None,
        Start,
        End,
        Both
    }

    public static async Task<int> Main(string[] args)
    {
        // --- parse args (allows --smart-comma anywhere) ---
        bool smartComma = false;
        ExtraEolOption extraEol = ExtraEolOption.None;
        var positional = new List<string>(3);

        foreach (var a in args)
        {
            if (a.StartsWith("--smart-comma=", StringComparison.OrdinalIgnoreCase) ||
                a.StartsWith("-c=", StringComparison.OrdinalIgnoreCase))
            {
                string[] val = a.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                if (val.Length != 2 || !bool.TryParse(val[1], out smartComma))
                {
                    await Console.Error.WriteLineAsync("Invalid value for --smart-comma. Use true|false.");
                    return EX_INVALIDARGS;
                }
                continue;
            }
            else if (a.StartsWith("--extra-eol", StringComparison.OrdinalIgnoreCase) ||
                     a.StartsWith("-e", StringComparison.OrdinalIgnoreCase))
            {
                string[] val = a.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                if (val.Length != 2 || !ValidExtraEofValue(val[1]))
                {
                    await Console.Error.WriteLineAsync("Invalid value for --extra-eol. Use none|start|end|both.");
                    return EX_INVALIDARGS;
                }

                switch (val[1].ToLowerInvariant())
                {
                    case "start":
                        extraEol = ExtraEolOption.Start;
                        break;
                    case "end":
                        extraEol = ExtraEolOption.End;
                        break;
                    case "both":
                        extraEol = ExtraEolOption.Both;
                        break;
                }

                continue;
            }
            positional.Add(a);
        }

        if (positional.Count < 3)
        {
            return await UsageAsync();
        }

        string filePath = positional[0];
        string hookArg = positional[1];
        string textToInsertArg = positional[2];

        // Normalized (trimmed) forms used only for comparisons
        string normHook = hookArg.Trim();

        // Local normalizers
        static string NormalizeForCompareSmart(string s) => s.Trim().TrimEnd(',').Trim();
        static string NormalizeForCompareBasic(string s) => s.Trim();

        string normInsertForCompare = smartComma
            ? NormalizeForCompareSmart(textToInsertArg)
            : NormalizeForCompareBasic(textToInsertArg);

        if (!File.Exists(filePath))
        {
            await Console.Error.WriteLineAsync($"Error: file not found: {filePath}");
            return EX_FILENOTFOUND;
        }

        try
        {
            // Preserve EOL style (CRLF vs LF)
            string original = await File.ReadAllTextAsync(filePath);
            string eol = original.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : "\n";

            // Split while preserving empty lines and positions
            var lines = original.Split(NewLineSplit, StringSplitOptions.None).ToList();

            // Find first occurrence of the hook, ignoring leading/trailing spaces in both sides
            int hookIndex = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                var lineNorm = lines[i].Trim();
                if (lineNorm.Contains(normHook, StringComparison.Ordinal))
                {
                    hookIndex = i;
                    break;
                }
            }

            if (hookIndex == -1)
            {
                await Console.Error.WriteLineAsync($"Hook not found: \"{hookArg}\"");
                return EX_HOOKNOTFOUND;
            }

            // Idempotency: if the (normalized) text already appears in the file, do nothing.
            bool alreadyPresentAfterHook = lines
                .Select(l => smartComma ? NormalizeForCompareSmart(l) : NormalizeForCompareBasic(l))
                .Any(t => string.Equals(t, normInsertForCompare, StringComparison.Ordinal));

            if (alreadyPresentAfterHook)
            {
                await Console.Out.WriteLineAsync(
                    smartComma
                        ? "Already present after the hook (ignoring surrounding spaces and trailing comma). No changes made."
                        : "Already present after the hook (ignoring surrounding spaces). No changes made."
                );
                return EX_OK;
            }

            string insertionLine = textToInsertArg;

            if (smartComma)
            {
                // Decide whether a trailing comma is needed for the inserted line:
                // - Look forward from the line after the hook until the next ')' character.
                // - If any non-whitespace character appears before ')', a comma is required.
                bool shouldAddComma = false;
                bool foundCloseParen = false;

                for (int li = hookIndex + 1; li < lines.Count && !foundCloseParen && !shouldAddComma; li++)
                {
                    var line = lines[li];
                    for (int ci = 0; ci < line.Length; ci++)
                    {
                        char ch = line[ci];
                        if (char.IsWhiteSpace(ch))
                            continue;

                        if (ch == ')')
                        {
                            foundCloseParen = true; // reached ')' with no text before it
                            break;
                        }

                        // Found some non-whitespace before ')': there is "more text"
                        shouldAddComma = true;
                        break;
                    }
                }

                // Trim right to avoid duplicate spaces before comma handling
                insertionLine = insertionLine.TrimEnd();

                if (shouldAddComma && !insertionLine.EndsWith(','))      // add comma if needed (char overload)
                {
                    insertionLine += ",";
                }
            }

            // Insert exactly one line right after the hook line
            lines.Insert(hookIndex + 1, insertionLine);

            // Handle extra EOL options
            switch (extraEol)
            {
                case ExtraEolOption.Start:
                    lines.Insert(hookIndex + 1, string.Empty);
                    break;
                case ExtraEolOption.End:
                    lines.Insert(hookIndex + 2, string.Empty);
                    break;
                case ExtraEolOption.Both:
                    lines.Insert(hookIndex + 1, string.Empty);
                    lines.Insert(hookIndex + 3, string.Empty);
                    break;
            }

            string updated = string.Join(eol, lines);

            // Preserve final EOL if present in original
            bool endsWithCRLF = original.EndsWith("\r\n", StringComparison.Ordinal);
            bool endsWithLF = original.EndsWith('\n');
            bool endsWithEol = endsWithCRLF || endsWithLF;

            if (endsWithEol && !updated.EndsWith(eol, StringComparison.Ordinal))
                updated += eol;

            await File.WriteAllTextAsync(filePath, updated);

            await Console.Out.WriteLineAsync($"Inserted after line {hookIndex + 1} in {Path.GetFileName(filePath)}.");
            return EX_OK;
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync("I/O error:");
            await Console.Error.WriteLineAsync(ex.Message);
            return EX_IOERR;
        }

        static async Task<int> UsageAsync()
        {
            await Console.Error.WriteLineAsync("Usage:");
            await Console.Error.WriteLineAsync("  dotnet run InsertAfterHook.cs -- [--smart-comma=true|false] [--extra-eol=none|start|end|both] <filePath> <hook> <textToInsert>");
            await Console.Error.WriteLineAsync();
            await Console.Error.WriteLineAsync(@"Examples:");
            await Console.Error.WriteLineAsync(@"  dotnet run InsertAfterHook.cs -- ./Program.cs ""// __HOOK__"" ""Console.WriteLine(\""Hello\"");""");
            await Console.Error.WriteLineAsync(@"  dotnet run InsertAfterHook.cs -- --smart-comma=true ./Program.cs ""// __HOOK__"" ""Add(Service)""");
            return EX_INVALIDARGS;
        }

        static bool ValidExtraEofValue(string val)
        {
            switch (val.ToLowerInvariant())
            {
                case "none":
                case "start":
                case "end":
                case "both":
                    return true;
                default:
                    return false;
            }
        }
    }
}
#pragma warning restore
