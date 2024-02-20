
namespace inspireVue.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail, string userId)
  {
    Account original = GetProfileByEmail(userEmail);
    if (original.Id != userId)
    {
      throw new Exception("Not your account to edit.");
    }
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    original.WantsCelsius = editData.WantsCelsius ?? original.WantsCelsius;
    original.WantsTwentyFourClock = editData.WantsTwentyFourClock ?? original.WantsTwentyFourClock;
    return _repo.Edit(original);
  }
}
