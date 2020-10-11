using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace League_Bot.Services
{
    public static class DataDragon
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<string> GetLatestVersion()
        {
            const string url = "https://ddragon.leagueoflegends.com/api/versions.json";
            var json = await (await Client.GetAsync(url)).Content.ReadAsStringAsync();
            var versionList = JsonConvert.DeserializeObject<List<string>>(json);

            return versionList.FirstOrDefault();
        }
    }
}
