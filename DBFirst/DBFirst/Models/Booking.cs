using System;
using System.Collections.Generic;

namespace DBFirst.Models;

public partial class Booking
{
    public int Bookingid { get; set; }

    public int? Memberid { get; set; }

    public int? Trainerid { get; set; }

    public int? Facilityid { get; set; }

    public DateOnly? Bookingdate { get; set; }

    public string? Slottime { get; set; }
}
