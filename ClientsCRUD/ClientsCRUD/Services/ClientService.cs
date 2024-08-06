using ClientsCRUD.DTOs;
using ClientsCRUD.Models;
using Microsoft.EntityFrameworkCore;
namespace ClientsCRUD.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientsCRUDDbContext _context;
        public ClientService(ClientsCRUDDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClientDto>> GetClientsAsync(ClientQueryParameters queryParameters)
        {
            IQueryable<Client> query = _context.Clients.Include(c => c.Address).Include(c => c.Accounts);

            if (!string.IsNullOrEmpty(queryParameters.Email))
            {
                query = query.Where(c => c.Email.Contains(queryParameters.Email));
            }

            if (!string.IsNullOrEmpty(queryParameters.FirstName))
            {
                query = query.Where(c => c.FirstName.Contains(queryParameters.FirstName));
            }

            if (!string.IsNullOrEmpty(queryParameters.LastName))
            {
                query = query.Where(c => c.LastName.Contains(queryParameters.LastName));
            }


            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                switch (queryParameters.SortBy.ToLower())
                {
                    case "firstname":
                        query = query.OrderBy(c => c.FirstName);
                        break;
                    case "lastname":
                        query = query.OrderBy(c => c.LastName);
                        break;

                }
            }

            // Paging
            int page = queryParameters.Page ?? 1;
            int pageSize = queryParameters.PageSize ?? 10;

            var pagedClients = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return pagedClients.Select(client => new ClientDto
            {
                Id = client.Id,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PersonalId = client.PersonalId,
                ProfilePhoto = client.ProfilePhoto,
                MobileNumber = client.MobileNumber,
                Sex = client.Sex.ToString(),
                Address = new AddressDto
                {
                    Country = client.Address.Country,
                    City = client.Address.City,
                    Street = client.Address.Street,
                    ZipCode = client.Address.ZipCode
                },
                Accounts = client.Accounts.Select(a => new AccountDto
                {
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                }).ToList()
            }).ToList();
        }

        public async Task<ClientDto> CreateClientAsync(CreateClientDto createClientDto)
        {

            var client = new Client
            {
                Email = createClientDto.Email,
                FirstName = createClientDto.FirstName,
                LastName = createClientDto.LastName,
                PersonalId = createClientDto.PersonalId,
                ProfilePhoto = createClientDto.ProfilePhoto,
                MobileNumber = createClientDto.MobileNumber,
                Sex = createClientDto.Sex,
                Address = new Address
                {
                    Country = createClientDto.Address.Country,
                    City = createClientDto.Address.City,
                    Street = createClientDto.Address.Street,
                    ZipCode = createClientDto.Address.ZipCode
                },
                Accounts = createClientDto.Accounts.Select(a => new Account
                {
                    AccountType = a.AccountType,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                }).ToList()
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new ClientDto
            {
                Id = client.Id,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PersonalId = client.PersonalId,
                ProfilePhoto = client.ProfilePhoto,
                MobileNumber = client.MobileNumber,
                Sex = client.Sex.ToString(),
                Address = new AddressDto
                {
                    ClientId = client.Id,
                    Country = client.Address.Country,
                    City = client.Address.City,
                    Street = client.Address.Street,
                    ZipCode = client.Address.ZipCode
                },
                Accounts = client.Accounts.Select(a => new AccountDto
                {
                    ClientId = client.Id,
                    AccountType = a.AccountType,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                }).ToList()
            };
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (client == null) return null;
            return MapToDto(client);

        }

        public async Task<bool> UpdateClientAsync(int id, UpdateClientDto updateClientDto)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null) return false;

            client.Email = updateClientDto.Email;
            client.FirstName = updateClientDto.FirstName;
            client.LastName = updateClientDto.LastName;
            client.PersonalId = updateClientDto.PersonalId;
            client.ProfilePhoto = updateClientDto.ProfilePhoto;
            client.MobileNumber = updateClientDto.MobileNumber;
            client.Sex = updateClientDto.Sex;
            client.Address.Country = updateClientDto.Address.Country;
            client.Address.City = updateClientDto.Address.City;
            client.Address.Street = updateClientDto.Address.Street;
            client.Address.ZipCode = updateClientDto.Address.ZipCode;
            client.Accounts = updateClientDto.Accounts.Select(a => new Account
            {
                AccountType = a.AccountType,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }


        private ClientDto MapToDto(Client client)
        {
            // Convert entity to DTO
            return new ClientDto
            {

                Id = client.Id,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PersonalId = client.PersonalId,
                ProfilePhoto = client.ProfilePhoto,
                MobileNumber = client.MobileNumber,
                Sex = client.Sex.ToString(),
                Address = new AddressDto
                {
                    ClientId = client.Id,
                    Country = client.Address.Country,
                    City = client.Address.City,
                    Street = client.Address.Street,
                    ZipCode = client.Address.ZipCode
                },
                Accounts = client.Accounts.Select(a => new AccountDto
                {
                    ClientId = client.Id,
                    AccountType = a.AccountType,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                }).ToList()
            };
        }
    }
}
