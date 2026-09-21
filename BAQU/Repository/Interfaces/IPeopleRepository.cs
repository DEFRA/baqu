using BAQU.Data;
using Microsoft.Graph.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BAQU.Repository;

public interface IPeopleRepository
{
    Task<User> Get(string id);
}
