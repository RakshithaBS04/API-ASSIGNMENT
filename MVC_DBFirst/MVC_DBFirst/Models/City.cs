using System;
using System.Collections.Generic;

namespace MVC_DBFirst.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public string? Country { get; set; }
}
