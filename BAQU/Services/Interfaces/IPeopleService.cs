using BAQU.Entities;
using BAQU.Repository;
using Microsoft.Graph.Models;

namespace BAQU.Services;

public interface IPeopleService
{
    Task<User> GetPerson(string Id);
}
