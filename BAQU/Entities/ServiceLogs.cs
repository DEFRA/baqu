namespace BAQU.Entities;

public record ServiceLogs(
    int Id = default!,
    DateTime LastChange = default!,
    int Changes = default!
);

