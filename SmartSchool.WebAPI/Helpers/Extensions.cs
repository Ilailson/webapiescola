using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.WebAPI.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int pageItems, int itemsPerPage, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, pageItems, itemsPerPage, totalPages);

           
            var camelCaseFormatter = new JsonSerializerSettings(); //transformar... Caixa baixa
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver(); //transformar... Caixa baixa

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }
    }
}