using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface IChildService
{
    Task<ChildResponseDto> RegisterChildAsync(ChildRequestDto dto);
    Task<ChildResponseDto> UpdateChildAsync(string childId, ChildRequestDto dto);
    Task<bool> DeleteChildAsync(string childId);
    Task<ChildResponseDto?> GetChildByIdAsync(string childId);
    Task<IEnumerable<ChildResponseDto>> GetChildrenByMotherIdAsync(string motherId);
} 