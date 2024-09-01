using GameTheoryTournament.Data;

namespace GameTheoryTournament.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[]? forecasts;

        protected override async Task OnInitializedAsync()
        {
            forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}