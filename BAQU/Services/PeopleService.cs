using BAQU.Entities;
using BAQU.Repository;
using Microsoft.Graph.Models;

namespace BAQU.Services;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly ILogger<PeopleService> _logger; 

    public PeopleService(IPeopleRepository peopleRepository, ILogger<PeopleService> logger)
    {
        _peopleRepository = peopleRepository;
        _logger = logger;
    }

    public virtual async Task<User> GetPerson(string Id)
    {
        _logger.LogInformation($"Getting person {Id}.");
        try
        {
            return await _peopleRepository.Get(Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while getting the person {Id}.");
            throw new ApplicationException($"An error occurred while getting the person {Id}.", ex);
        }
    }
}
