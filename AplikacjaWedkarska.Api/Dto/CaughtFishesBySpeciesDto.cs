using AplikacjaWedkarska.Api.Entities;

namespace AplikacjaWedkarska.Api.Dto
{
    public class CaughtFishesBySpeciesDto
    {
        public string? Species { get; set; }
        public List<CaughtFishInfoDto> FishList { get; set; } = new List<CaughtFishInfoDto>();
        public bool IsExpanded { get; set; } = false;

    }
}
