namespace Client_FAU.Business.Interfaces
{
    public interface Jwt_Int
    {
        Task SetAuthorizationHeaderAsync(string localSName);
    }
}
