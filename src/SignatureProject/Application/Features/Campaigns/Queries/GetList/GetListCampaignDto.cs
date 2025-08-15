using Core.Application.Dtos;

namespace Application.Features.Campaigns.Queries.GetList;

public class GetListCampaignDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ExpiredDate { get; set; }
    public int Quantity { get; set; }
}
