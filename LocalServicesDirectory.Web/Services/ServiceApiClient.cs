using System.Net.Http.Json;

namespace LocalServicesDirectory.Web.Services
{
    public class ServiceApiClient
    {
        private readonly HttpClient _http;
        public ServiceApiClient(HttpClient http) => _http = http;

        
        private static async Task<T> Try<T>(Func<Task<T>> op, T @default)
        {
            try { return await op(); } catch { return @default; }
        }
        private static async Task<bool> Try(Func<Task<bool>> op)
        {
            try { return await op(); } catch { return false; }
        }

        

        public class CategoryDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
        }
        public class CreateOrUpdateCategoryDto
        {
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
        }

        public class CityDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
        public class CreateOrUpdateCityDto
        {
            public string Name { get; set; } = string.Empty;
        }

        
        public Task<List<CategoryDto>> GetCategoriesAsync()
            => Try(async () => await _http.GetFromJsonAsync<List<CategoryDto>>("/api/catalogs/categories") ?? new(), new List<CategoryDto>());
        public Task<List<CityDto>> GetCitiesAsync()
            => Try(async () => await _http.GetFromJsonAsync<List<CityDto>>("/api/catalogs/cities") ?? new(), new List<CityDto>());

        
        public Task<bool> CreateCategoryAsync(CreateOrUpdateCategoryDto dto)
            => Try(async () => (await _http.PostAsJsonAsync("/api/catalogs/categories", dto)).IsSuccessStatusCode);
        public Task<bool> CreateCityAsync(CreateOrUpdateCityDto dto)
            => Try(async () => (await _http.PostAsJsonAsync("/api/catalogs/cities", dto)).IsSuccessStatusCode);

        
        public Task<bool> UpdateCategoryAsync(Guid id, CreateOrUpdateCategoryDto dto)
            => Try(async () => (await _http.PutAsJsonAsync($"/api/catalogs/categories/{id}", dto)).IsSuccessStatusCode);
        public Task<bool> UpdateCityAsync(Guid id, CreateOrUpdateCityDto dto)
            => Try(async () => (await _http.PutAsJsonAsync($"/api/catalogs/cities/{id}", dto)).IsSuccessStatusCode);

        
        public Task<bool> DeleteCategoryAsync(Guid id)
            => Try(async () => (await _http.DeleteAsync($"/api/catalogs/categories/{id}")).IsSuccessStatusCode);
        public Task<bool> DeleteCityAsync(Guid id)
            => Try(async () => (await _http.DeleteAsync($"/api/catalogs/cities/{id}")).IsSuccessStatusCode);

        

        public class ServiceDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public Guid CategoryId { get; set; }
            public Guid CityId { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
            public string? WebsiteUrl { get; set; }
            public bool IsVerified { get; set; }
            public double AverageRating { get; set; }
        }
        public class CreateOrUpdateServiceDto
        {
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public Guid CategoryId { get; set; }
            public Guid CityId { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
            public string? WebsiteUrl { get; set; }
            public bool IsVerified { get; set; }
        }

        public Task<List<ServiceDto>> SearchAsync(string? q = null)
            => Try(async () =>
            {
                var url = string.IsNullOrWhiteSpace(q) ? "/api/services" : $"/api/services?q={Uri.EscapeDataString(q)}";
                return await _http.GetFromJsonAsync<List<ServiceDto>>(url) ?? new();
            }, new List<ServiceDto>());

        public Task<ServiceDto?> GetAsync(Guid id)
            => Try(async () => await _http.GetFromJsonAsync<ServiceDto>($"/api/services/{id}"), null!);

        public Task<Guid?> CreateAsync(CreateOrUpdateServiceDto dto)
            => Try<Guid?>(async () =>
            {
                var resp = await _http.PostAsJsonAsync("/api/services", dto);
                if (!resp.IsSuccessStatusCode) return null;
                var dict = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return (dict != null && dict.TryGetValue("id", out var idStr) && Guid.TryParse(idStr, out var id)) ? id : null;
            }, null);

        public Task<bool> UpdateAsync(Guid id, CreateOrUpdateServiceDto dto)
            => Try(async () => (await _http.PutAsJsonAsync($"/api/services/{id}", dto)).IsSuccessStatusCode);

        public Task<bool> DeleteAsync(Guid id)
            => Try(async () => (await _http.DeleteAsync($"/api/services/{id}")).IsSuccessStatusCode);
    }
}

