using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetList;
public class GetListOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetListOperationClaimListItemDto()
    {
        Name = string.Empty;
    }

    public GetListOperationClaimListItemDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}