using System.ComponentModel.DataAnnotations.Schema;

namespace BAQU.Entities;

[Table("PeopleInfo")]
public record Person(
    [property: Column("uid")] string PersonId = default!,
    [property: Column("name")] string PersonName = default!,
    [property: Column("supervisor")] string ManagerId = default!,
    [property: Column("supervisorName")] string ManagerName = default!
);
