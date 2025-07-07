using System.Threading.Tasks;
using System.Linq;
using Acme.StackOverflow.AppUsers;
using Acme.StackOverflow;
using AutoMapper.Internal.Mappers;
using System.Text;
using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

public class AppUserAppService : CrudAppService<
    AppUser,
    AppUserDto,
    Guid,
    ListRequestDto,
    CreateUpdateAppUserDto,
    CreateUpdateAppUserDto>,
    IAppUserAppService
{
    private readonly IRepository<AppUser, Guid> _repository;

    public AppUserAppService(IRepository<AppUser, Guid> repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<AppUserDto?> CheckUserAsync(string email, string password)
    {
        var userQueryable = await _repository.GetQueryableAsync();

        var query = from user in userQueryable
                    where user.Email == email && user.PasswordHash == password
                    select new AppUserDto
                    {
                        Id = user.Id,
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash,
                        Bio = user.Bio,
                        Location = user.Location
                    };

        return await AsyncExecuter.FirstOrDefaultAsync(query);
    }

}

