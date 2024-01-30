using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyOldButNewApp.GoogleBooksClient
{
    public class BooksManager
    {
        public async Task<IEnumerable<Volumeinfo>> GetVolumeInfos(string suchText)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={suchText}&maxResults=40";

            var http = new HttpClient();

            var json = await http.GetStringAsync(url);

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<BooksResult>(json);

            return result.items.Select(x => x.volumeInfo);
        }
    }
}
