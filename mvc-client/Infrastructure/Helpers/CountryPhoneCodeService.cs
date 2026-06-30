using System.Text.Json;

public class CountryPhoneCodeService
{
    private readonly IWebHostEnvironment _env;

    public CountryPhoneCodeService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public IList<CountryPhoneCodeItem> GetAll()
    {
        var jsonPath = Path.Combine(_env.ContentRootPath, "Infrastructure", "Helpers", "CountryPhoneCode.json");

        if (!File.Exists(jsonPath))
            return new List<CountryPhoneCodeItem>();

        var json = File.ReadAllText(jsonPath);

        var countries = JsonSerializer.Deserialize<List<CountryPhoneCodeItem>>(json);

        return countries ?? new List<CountryPhoneCodeItem>();
    }
}

public class CountryPhoneCodeItem
{
    public string name { get; set; } = string.Empty;
    public string dial_code { get; set; } = string.Empty;
    public string code { get; set; } = string.Empty;
}