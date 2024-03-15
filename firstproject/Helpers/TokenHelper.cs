using System.Security.Claims;

namespace firstproject.Helpers;

public class TokenHelper
{
  public static long GetUserId(Claim? claim)
  {
    if (claim != null)
    {
      string userId = claim.Value;
      if (long.TryParse(userId, out long result)) return result;
    }

    throw new UnauthorizedAccessException("Invalid id.");
  }

}