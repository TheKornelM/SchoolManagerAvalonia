using System.Globalization;

namespace SchoolManagerModel.Utils;

public static class CultureUtils
{
    /// <summary>
    /// Returns the full name based on the current culture.
    /// </summary>
    /// <param name="firstName">The first name of the individual.</param>
    /// <param name="lastName">The last name of the individual.</param>
    /// <returns>A string representing the full name formatted for the current culture.</returns>
    public static string GetFullName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            throw new ArgumentException("Both first name and last name must be provided.");

        var currentCulture = CultureInfo.CurrentCulture.Name;

        // Hungarian culture formats as "LastName FirstName"
        if (currentCulture == "hu-HU")
        {
            return $"{lastName} {firstName}";
        }

        // Default to "FirstName LastName" for other cultures, including English
        return $"{firstName} {lastName}";
    }

    public static string GetTimeString(DateTime time)
    {
        var currentCulture = CultureInfo.CurrentCulture;
        var dateTimeFormat = currentCulture.DateTimeFormat;

        // Construct a custom format string using culture-specific patterns
        string customFormat = $"{dateTimeFormat.ShortDatePattern} {dateTimeFormat.ShortTimePattern}";

        // Format the DateTime based on the custom format string
        string formattedTime = time.ToString(customFormat, currentCulture);

        return formattedTime;
    }
}
