using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Core.Service;

public class ChildService : IChildService
{
    private readonly IChildRepository _childRepository;
    private readonly IUserRepository _userRepository;

    public ChildService(IChildRepository childRepository, IUserRepository userRepository)
    {
        _childRepository = childRepository;
        _userRepository = userRepository;
    }

    public async Task<ChildResponseDto> RegisterChildAsync(ChildRequestDto dto)
    {
        if (!Guid.TryParse(dto.MotherId, out var motherId))
            throw Except.BadRequest("Invalid mother ID");
        var mother = await _userRepository.GetByIdAsync(motherId);
        if (mother == null || mother.Role != UserRole.CommunityMother)
            throw Except.NotFound("Mother not found");
        var child = new Child
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Gender = Enum.TryParse<Gender>(dto.Gender, out var g) ? g : Gender.Other,
            BloodGroup = dto.BloodGroup,
            MedicalHistory = dto.MedicalHistory,
            Allergies = dto.Allergies,
            Weight = dto.Weight,
            Height = dto.Height,
            MotherId = motherId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        await _childRepository.AddAsync(child);
        await _childRepository.SaveChangesAsync();
        return ToResponseDto(child);
    }

    public async Task<ChildResponseDto> UpdateChildAsync(string childId, ChildRequestDto dto)
    {
        if (!Guid.TryParse(childId, out var id))
            throw Except.BadRequest("Invalid child ID");
        var child = await _childRepository.GetByIdAsync(id);
        if (child == null)
            throw Except.NotFound("Child not found");
        child.FirstName = dto.FirstName;
        child.LastName = dto.LastName;
        child.DateOfBirth = dto.DateOfBirth;
        child.Gender = Enum.TryParse<Gender>(dto.Gender, out var g) ? g : Gender.Other;
        child.BloodGroup = dto.BloodGroup;
        child.MedicalHistory = dto.MedicalHistory;
        child.Allergies = dto.Allergies;
        child.Weight = dto.Weight;
        child.Height = dto.Height;
        child.UpdatedAt = DateTime.UtcNow;
        _childRepository.Update(child);
        await _childRepository.SaveChangesAsync();
        return ToResponseDto(child);
    }

    public async Task<bool> DeleteChildAsync(string childId)
    {
        if (!Guid.TryParse(childId, out var id))
            throw Except.BadRequest("Invalid child ID");
        var child = await _childRepository.GetByIdAsync(id);
        if (child == null)
            throw Except.NotFound("Child not found");
        _childRepository.Remove(child);
        await _childRepository.SaveChangesAsync();
        return true;
    }

    public async Task<ChildResponseDto?> GetChildByIdAsync(string childId)
    {
        if (!Guid.TryParse(childId, out var id))
            throw Except.BadRequest("Invalid child ID");
        var child = await _childRepository.GetByIdAsync(id);
        if (child == null)
            throw Except.NotFound("Child not found");
        return ToResponseDto(child);
    }

    // Pagination support
    public async Task<IEnumerable<ChildResponseDto>> GetChildrenByMotherIdAsync(string motherId)
    {
        if (!Guid.TryParse(motherId, out var id))
            throw Except.BadRequest("Invalid mother ID");
        var children = await _childRepository.GetByMotherIdAsync(id);
        return children.Select(ToResponseDto);
    }

    public async Task<Paginate<ChildResponseDto>> GetChildrenByMotherIdPaginatedAsync(string motherId, int page, int pageSize)
    {
        if (!Guid.TryParse(motherId, out var id))
            throw Except.BadRequest("Invalid mother ID");
        var children = (await _childRepository.GetByMotherIdAsync(id)).ToList();
        var totalCount = children.Count;
        var items = children.Skip((page - 1) * pageSize).Take(pageSize).Select(ToResponseDto);
        return new Paginate<ChildResponseDto>(items, page, pageSize, totalCount);
    }

    private ChildResponseDto ToResponseDto(Child child)
    {
        return new ChildResponseDto
        {
            Id = child.Id.ToString(),
            FirstName = child.FirstName,
            LastName = child.LastName,
            DateOfBirth = child.DateOfBirth,
            Gender = child.Gender.ToString(),
            BloodGroup = child.BloodGroup,
            MedicalHistory = child.MedicalHistory,
            Allergies = child.Allergies,
            Weight = child.Weight,
            Height = child.Height,
            MotherId = child.MotherId.ToString(),
            IsActive = child.IsActive
        };
    }
} 