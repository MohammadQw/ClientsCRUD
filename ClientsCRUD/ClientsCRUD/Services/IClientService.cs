using ClientsCRUD.DTOs;

namespace ClientsCRUD.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetClientsAsync(ClientQueryParameters queryParameters);
        Task<ClientDto> CreateClientAsync(CreateClientDto createClientDto);
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<bool> UpdateClientAsync(int id, UpdateClientDto updateClientDto);
        Task<bool> DeleteClientAsync(int id);
    }
}