using System.Globalization;

// Imported from Libray project.

namespace Arbeidskrav1_Sem2.Helpers;

public static class InputHelpers
{
    /// <summary>
    /// Reads user input from the console and attempts to parse it as a non-negative integer.
    /// Continues prompting until a valid value is entered.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <returns>A non-negative integer entered by the user.</returns>
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();

            if (int.TryParse(s, out var value) && value >= 0)
                return value;

            Console.WriteLine("Invalid input.");
        }
    }
    
    /// <summary>
    /// Reads a required string value from the console.
    /// Rejects empty or whitespace-only input and continues prompting until valid text is entered.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <returns>A trimmed, non-empty string.</returns>
    public static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Required field - Cannot be empty.");
        }
    }

    // /// <summary>
    // /// Reads and validates an email address from the console.
    // /// Uses <see cref="ValidationHelpers.IsValidEmail"/> to verify format.
    // /// Continues prompting until a valid email is entered.
    // /// </summary>
    // /// <param name="prompt">The message displayed to the user.</param>
    // /// <returns>A validated email address.</returns>
    // public static string ReadEmailAddress(string prompt)
    // {
    //     while (true)
    //     {
    //         string emailInput = ReadRequiredString(prompt).Trim();
    //
    //         if (ValidationHelpers.IsValidEmail(emailInput))
    //             return emailInput;
    //
    //         Console.WriteLine("Invalid email address. Example: user@example.com.\n" +
    //                           "Must include a domain and Top Level Domain (TLD).");
    //     }
    // }

    /// <summary>
    /// Reads a menu choice from the console and ensures the value falls within the specified range.
    /// Continues prompting until a valid choice is entered.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <param name="min">The minimum allowed menu value.</param>
    /// <param name="max">The maximum allowed menu value.</param>
    /// <returns>An integer within the specified range.</returns>
    public static int ReadMenuChoice(string prompt, int min, int max)
    {
        while (true)
        {
            int value = ReadInt(prompt);

            if (value >= min && value <= max)
                return value;

            Console.WriteLine($"Invalid choice. Enter a number between {min} and {max}.");
        }

    }
    
    /// <summary>
    /// Reads a date from the console using one of several accepted formats.
    /// Normalizes the result to midnight with <see cref="DateTimeKind.Unspecified"/>.
    /// Continues prompting until a valid date is entered.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <returns>A normalized <see cref="DateTime"/> representing the entered date.</returns>
    public static DateTime ReadDate(string prompt)
    {
        string[] acceptedFormats =
        {
            "dd.MM.yyyy",
            "ddmmyyyy",
            "dd-MM-yyyy",
            "dd/MM/yyyy",
        };
        
        while (true)
        {
            string input = ReadRequiredString(prompt).Trim();

            if (DateTime.TryParseExact(
                    input,
                    acceptedFormats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsed))
            {
                return new DateTime(parsed.Year, parsed.Month, parsed.Day, 0, 0, 0, 
                    DateTimeKind.Unspecified);
            }
            
            Console.WriteLine("Invalid date. Use format dd.MM.yyyy / dd-MM-yyyy / ddMMyyyy.");
        }
    }

    // /// <summary>
    // /// Reads and validates a phone number from the console.
    // /// Accepts only 8-digit numeric values (Norwegian format).
    // /// Continues prompting until a valid number is entered.
    // /// </summary>
    // /// <param name="prompt">The message displayed to the user.</param>
    // /// <param name="defaultRegionIso2">
    // /// Optional ISO-2 region code. Currently unused but reserved for future validation rules.
    // /// </param>
    // /// <returns>A validated phone number string.</returns>
    // public static string ReadPhoneNumber(string prompt, string defaultRegionIso2 = "NO")
    // {
    //     while (true)
    //     {
    //         string input = ReadRequiredString(prompt);
    //
    //         if (input.Length == 8 && input.All(char.IsDigit))
    //             return input;
    //
    //         Console.WriteLine("Invalid phone number. Must be 8 digits.");
    //     }
    // }

    // /// <summary>
    // /// Reads and normalizes an ID value such as booking, room, guest, or payment ID.
    // /// Accepts both dashed and non-dashed formats (e.g., B-001 or B001),
    // /// and automatically adjusts casing and dash placement based on the specified output format.
    // /// </summary>
    // /// <param name="prompt">The message displayed to the user.</param>
    // /// <param name="format">
    // /// The desired output format, controlling casing and dash usage.
    // /// Defaults to <see cref="IDFormat.UpperWithDash"/>.
    // /// </param>
    // /// <returns>
    // /// A normalized ID string matching the requested format.
    // /// </returns>
    // public static string ReadId(string prompt, IDFormat format = IDFormat.UpperWithDash)
    // {
    //     while (true)
    //     {
    //         string input = ReadRequiredString(prompt).Trim();
    //         if (string.IsNullOrWhiteSpace(input))
    //         {
    //             Console.WriteLine("ID cannot be empty.");
    //             continue;
    //         }
    //
    //         if (input.Length < 2)
    //         {
    //             Console.WriteLine("Invalid id. Use B-001, B001, R-001, R001, (with or without dash).");
    //             continue;
    //         }
    //         
    //         char prefix = char.ToUpperInvariant(input[0]);
    //         bool prefixOk = prefix is 'B' or 'P' or 'G' or 'R';
    //         if (!prefixOk)
    //         {
    //             Console.WriteLine("Invalid ID prefix. Use B, P, G or R.");
    //             continue;
    //         }
    //
    //         // Accept both dash and no dash:
    //         bool hasDash =
    //             input.Length == 5 &&
    //             input[1] == '-' &&
    //             char.IsDigit(input[2]) &&
    //             char.IsDigit(input[3]) &&
    //             char.IsDigit(input[4]);
    //         
    //         bool noDash =
    //             input.Length == 4 &&
    //             char.IsDigit(input[1]) &&
    //             char.IsDigit(input[2]) &&
    //             char.IsDigit(input[3]);
    //
    //         if (!hasDash && !noDash)
    //         {
    //             Console.WriteLine($"Invalid {prefix} ID. Use {prefix}-001 or {prefix}001 (3 digits).");
    //             continue;
    //         }
    //
    //         // Reject '000' as that is an invalid booking ID
    //         int digitStart = hasDash ? 2 : 1;
    //         if (input[digitStart] == '0' &&
    //             input[digitStart + 1] == '0' &&
    //             input[digitStart + 2] == '0')
    //         {
    //             Console.WriteLine("Invalid booking id. Digits cannot be '000'.");
    //             continue;
    //         }
    //
    //         // Extracts final 3 digits
    //         string digits = input.Substring(input.Length - 3);
    //
    //         bool lower = format is IDFormat.LowerNoDash or IDFormat.LowerWithDash;
    //         bool withDash = format is IDFormat.UpperWithDash or IDFormat.LowerWithDash;
    //         
    //         char letter = lower ? char.ToLowerInvariant(prefix) :
    //             char.ToUpperInvariant(prefix);
    //         return withDash ? $"{letter}-{digits}" : $"{letter}{digits}";
    //     }
    // }
}
