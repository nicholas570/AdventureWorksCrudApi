using System.Text;

namespace AdventureWorksCrudApi.Common;

public static class Cursor
{
    public static string Encode(int id) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(id.ToString()));

    public static bool TryDecode(string value, out int id)
    {
        id = 0;
        try
        {
            return int.TryParse(Encoding.UTF8.GetString(Convert.FromBase64String(value)), out id);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
