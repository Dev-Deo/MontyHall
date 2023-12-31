﻿using Domain.Common;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : BaseIdentityUserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactNo { get; set; }
        List<GameRequest> GameRequests { get; set; }
    }
}
